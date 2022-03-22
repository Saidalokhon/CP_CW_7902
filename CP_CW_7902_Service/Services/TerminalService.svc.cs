using CP_CW_7902_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace CP_CW_7902_Service.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TerminalService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TerminalService.svc or TerminalService.svc.cs at the Solution Explorer and start debugging.
    public class TerminalService : ITerminalService
    {
        Semaphore semaphore = new Semaphore(3, 3);
        static List<Terminal> terminals = new List<Terminal>();
        SynConnectDLL.SynConnection connection = SynConnectDLL.SynConnection.GetInstance();

        private void UpdateTerminals()
        {
            terminals.Clear();
            Random random = new Random();
            for (int i = 0; i < 10; i++) terminals.Add(new Terminal(random));
        }

        private void FillTerminalSwipes(Terminal terminal)
        {
            semaphore.WaitOne();
            terminal.Status = Terminal.Statuses.InProcess;
            terminal.ParseSwipes(connection.RetrieveSwipes(terminal.Ip));
            semaphore.Release();
        }

        public Dictionary<string, string> GetStatus() => terminals.ToDictionary(t => t.Ip, t => t.Status.ToString());

        public void StartCollectingSwipes()
        {
            UpdateTerminals();
            List<AutoResetEvent> events = new List<AutoResetEvent>();

            for(int i = 0; i < terminals.Count; i++)
                new Thread((object obj) =>
                {
                    int count = (int)obj;
                    events.Add(new AutoResetEvent(false));
                    FillTerminalSwipes(terminals[count]);
                    terminals[count].Status = Terminal.Statuses.Finished;
                    events[count].Set();
                }).Start(i);

            new Thread(() =>
            {
                WaitHandle.WaitAll(events.ToArray());
            }).Start();
        }
    }
}
