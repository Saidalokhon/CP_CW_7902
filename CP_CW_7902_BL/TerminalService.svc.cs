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
        Semaphore semaphore = new Semaphore(3, 3);
        Dictionary<string, List<Terminal>> terminals = new Dictionary<string, List<Terminal>>();
        SynConnectDLL.SynConnection connection = SynConnectDLL.SynConnection.GetInstance();
        string clientToken = "";
        List<AutoResetEvent> events = new List<AutoResetEvent>();
        static object _lock = new object();
        #endregion
        #region StartCollectingSwipes
        public bool StartCollectingSwipes(string clientToken)
        {
            if (clientToken.Equals(this.clientToken)) return false;

            lock (_lock)
            {
                if (events.Count > 0) WaitHandle.WaitAll(events.ToArray());

                UpdateService(clientToken);

                for (int i = 0; i < terminals[clientToken].Count; i++)
                    new Thread((object obj) =>
                    {
                        List<string> parameters = (List<string>)obj;

                        int count = Convert.ToInt32(parameters[1]);
                        string token = parameters[0];

                        FillTerminalSwipes(terminals[token][count]);
                        terminals[token][count].Status = Statuses.Finished;

                        events[count].Set();
                    }).Start(new List<string> { clientToken, i.ToString() });

                this.clientToken = "";

                return true;
            }
        }

        private void UpdateService(string clientToken)
        {
            Random random = new Random();

            events.Clear();

            List<Terminal> terminals = new List<Terminal>();
            for (int i = 0; i < 10; i++)
            {
                terminals.Add(new Terminal(random));
                events.Add(new AutoResetEvent(false));
            }
            this.terminals.Add(clientToken, terminals);
        }

        private void FillTerminalSwipes(Terminal terminal)
        {
            semaphore.WaitOne();
            terminal.Status = Statuses.InProcess;
            terminal.ParseSwipes(connection.RetrieveSwipes(terminal.Ip));
            AddSwipesToDatabase(terminal);
            semaphore.Release();
        }

        private void AddSwipesToDatabase(Terminal terminal)
        {
            lock (this)
            {
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
        public Dictionary<string, string> GetStatus(string clientToken) => terminals.ContainsKey(clientToken) ? terminals[clientToken].ToDictionary(t => t.Ip, t => t.Status.ToString()) : null;
        #endregion
        #region TruncateDatabase
        public void TruncateDatabase()
        {
            new SwipesRepository(ConfigurationManager.ConnectionStrings["SwipeDatabase"].ConnectionString).Truncate();
        }
        #endregion
        #region GetDatabase
        public List<List<string>> GetDatabase()
        {
            List<List<string>> list = new List<List<string>>();
            new SwipesRepository(ConfigurationManager.ConnectionStrings["SwipeDatabase"].ConnectionString).GetAll()
                .ForEach(swipe => list.Add(new List<string> { swipe.SwipeId, swipe.Time.ToString(), swipe.Direction, swipe.TerminalIp }));
            return list;
        }
        #endregion
    }
}
