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
        Semaphore semaphore = new Semaphore(3, 3);
        List<Terminal> terminals = new List<Terminal>();
        SynConnectDLL.SynConnection connection = SynConnectDLL.SynConnection.GetInstance();

        public Dictionary<string, string> GetStatus() => terminals.ToDictionary(t => t.Ip, t => t.Status.ToString());

        public void StartCollectingSwipes()
        {
            UpdateTerminals();

            for (int i = 0; i < terminals.Count; i++)
                new Thread((object obj) =>
                {
                    int count = (int)obj;
                    FillTerminalSwipes(terminals[count]);
                    terminals[count].Status = Statuses.Finished;
                }).Start(i);
        }

        private void UpdateTerminals()
        {
            terminals.Clear();
            Random random = new Random();
            for (int i = 0; i < 10; i++) terminals.Add(new Terminal(random));
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
            object _lock = new object();
            lock (_lock)
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

        public void TruncateDatabase()
        {
            new SwipesRepository(ConfigurationManager.ConnectionStrings["SwipeDatabase"].ConnectionString).Truncate();
        }

        public List<List<string>> GetDatabase()
        {
            List<List<string>> list = new List<List<string>>();
            new SwipesRepository(ConfigurationManager.ConnectionStrings["SwipeDatabase"].ConnectionString).GetAll()
                .ForEach(swipe => list.Add(new List<string> { swipe.SwipeId, swipe.Time.ToString(), swipe.Direction, swipe.TerminalIp }));
            return list;
        }
    }
}
