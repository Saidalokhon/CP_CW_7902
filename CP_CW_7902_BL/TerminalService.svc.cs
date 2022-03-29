using AutoMapper;
using CP_CW_7902_BL.Models;
using CP_CW_7902_DAL.DBO;
using CP_CW_7902_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace CP_CW_7902_BL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TerminalService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TerminalService.svc or TerminalService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class TerminalService : ITerminalService
    {
        #region Local Variables
        // Create needed local variables.

        // Create Semaphore object with min and max values of 3 and 3.
        Semaphore semaphore = new Semaphore(3, 3);

        // Create dictionary with client token as a key and list of terminals as value.
        Dictionary<string, List<Terminal>> terminals = new Dictionary<string, List<Terminal>>();

        // Get instance from DLL file.
        SynConnectDLL.SynConnection connection = SynConnectDLL.SynConnection.GetInstance();

        // Initialize client token as empty string
        string clientToken = "";

        // Create new list of AutoResetEvents.
        List<AutoResetEvent> events = new List<AutoResetEvent>();

        // Create object to lock on.
        static object _lock = new object();
        #endregion
        #region StartCollectingSwipes
        /// <summary>
        /// The method starts to collect the swipes.
        /// </summary>
        /// <param name="clientToken">Token passed from client</param>
        /// <returns>
        /// The boolean variable. True if the method ran successfuly,
        /// otherwise false.
        /// </returns>
        public bool StartCollectingSwipes(string clientToken)
        {
            // Return false if the duplicate request from the client is comming
            // while being collecting swipes.
            if (clientToken.Equals(this.clientToken)) return false;

            // Let only one client at a time to enter the critical section.
            lock (_lock)
            {
                // If previous collection of swipes is not finished, wait.
                if (events.Count > 0) WaitHandle.WaitAll(events.ToArray());

                // Update the variables.
                UpdateService(clientToken);

                // Create threads for each terminal in the list of terminals.
                for (int i = 0; i < terminals[clientToken].Count; i++)
                    new Thread((object obj) =>
                    {
                        // Get parameters from passed object.
                        List<string> parameters = (List<string>)obj;

                        // Get count (i) from parameters.
                        int count = Convert.ToInt32(parameters[1]);
                        // Get client token from parameters.
                        string token = parameters[0];

                        // Fill the list of swipes for each terminal.
                        FillTerminalSwipes(terminals[token][count]);
                        // Set the status of the terminal to Finished.
                        terminals[token][count].Status = Statuses.Finished;

                        // Signal about finishing the work.
                        events[count].Set();
                    }).Start(new List<string> { clientToken, i.ToString() });

                // Set the client token as empty string.
                this.clientToken = "";

                return true;
            }
        }

        /// <summary>
        /// The method updates local variables, refreshes and creates lists needed.
        /// </summary>
        /// <param name="clientToken">Token passed from client</param>
        private void UpdateService(string clientToken)
        {
            Random random = new Random();

            // Clear list of AutoResetEvents.
            events.Clear();

            List<Terminal> terminals = new List<Terminal>();
            // Add 10 terminals and 10 events to the corresponding lists.
            for (int i = 0; i < 10; i++)
            {
                terminals.Add(new Terminal(random));
                events.Add(new AutoResetEvent(false));
            }

            // Add list of terminals to the dictionary with client token as a key.
            this.terminals.Add(clientToken, terminals);
        }


        /// <summary>
        /// The method fills the list of passed terminal object with swipes.
        /// </summary>
        /// <param name="terminal">The terminal to be filled.</param>
        private void FillTerminalSwipes(Terminal terminal)
        {
            // Control the number of incoming threads.
            // Enter the semaphore.
            semaphore.WaitOne();
            // Set terminal status to InProcess.
            terminal.Status = Statuses.InProcess;
            // Get swipes data from DLL function, parse them and 
            // add to the list of swipes in terminal object.
            terminal.ParseSwipes(connection.RetrieveSwipes(terminal.Ip));
            // Add swipes to the database.
            AddSwipesToDatabase(terminal);
            // Leave semaphore.
            semaphore.Release();
        }

        /// <summary>
        /// The method gets the list of swipes from terminal and writes them to the database.
        /// </summary>
        /// <param name="terminal">The terminal from which swipes are taken.</param>
        private void AddSwipesToDatabase(Terminal terminal)
        {
            // Let only one thread to be able to proceed.
            lock (this)
            {
                // 1) Create new instance of SwipesRepository. The connection string is taken from 
                // the config file.
                // 2) Use Insert function of the SwipesRepository class.
                // 3) Map the DBO Swipe to the model Swipe using automapper.
                // Take the IP of the terminal passed and map it to TerminalIp variable
                // of DBO Swipe.
                // 4) Pass the mapped DBO object to Insert method.
                new SwipesRepository(ConfigurationManager.ConnectionStrings["SwipeDatabase"].ConnectionString)
                    .Insert(new Mapper(new MapperConfiguration(cfg =>
                        cfg.CreateMap<Models.Swipe, CP_CW_7902_DAL.DBO.Swipe>()
                        .ForMember(dest => dest.SwipeId, opt => opt.MapFrom(src => src.SwipeId))
                        .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time))
                        .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
                        .ForMember(dest => dest.TerminalIp, opt => opt.MapFrom(src => terminal.Ip))))
                    .Map<Models.Swipe[], IList<CP_CW_7902_DAL.DBO.Swipe>>(terminal.Swipes.ToArray()).ToList());
            }
        }
        #endregion
        #region GetStatus
        /// <summary>
        /// The method returns the status of all terminals for the given client token.
        /// </summary>
        /// <param name="clientToken">Token passed from client</param>
        /// <returns>
        /// If the client token is found, the dictionary with statuses.
        /// Else null.
        /// </returns>
        public Dictionary<string, string> GetStatus(string clientToken) => terminals.ContainsKey(clientToken) ? terminals[clientToken].ToDictionary(t => t.Ip, t => t.Status.ToString()) : null;
        #endregion
        #region TruncateDatabase
        /// <summary>
        /// The method truncates the database (cleans the Swipes table).
        /// </summary>
        public void TruncateDatabase()
        {
            // 1) Create new SwipeRepository object
            // 2) Take connection string from config file
            // 3) Pass as a parameter and call Truncate method.
            new SwipesRepository(ConfigurationManager.ConnectionStrings["SwipeDatabase"].ConnectionString).Truncate();
        }
        #endregion
        #region GetDatabase
        /// <summary>
        /// The method returns the records from Swipes table.
        /// </summary>
        public List<List<string>> GetDatabase()
        {
            // 1) Create the list containing list of strings.
            // 2) Create new SwipeRepository object
            // 3) Take connection string from config file
            // 4) Pass as a parameter and call GetAll method.
            // 5) For each returned swipe get the swipe data and add it to the list of strings.
            // 6) Add the list of strings to the list containing list of strings.
            List<List<string>> list = new List<List<string>>();
            new SwipesRepository(ConfigurationManager.ConnectionStrings["SwipeDatabase"].ConnectionString).GetAll()
                .ForEach(swipe => list.Add(new List<string> { swipe.SwipeId, swipe.Time.ToString(), swipe.Direction, swipe.TerminalIp }));
            return list;
        }
        #endregion
    }
}
