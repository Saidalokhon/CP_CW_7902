using CP_CW_7902_UI.Models;
using CP_CW_7902_UI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CP_CW_7902_UI
{
    public partial class DataForm : Form
    {
        TerminalService.ITerminalService service;
        List<Terminal> terminals;
        static bool flag;
        string clientToken;

        bool MainBGWorkerIsWorking = false;
        bool DatabaseBGWorkerIsWorking = false;

        public DataForm()
        {
            InitializeComponent();
            service = new TerminalService.TerminalServiceClient();
            clientToken = ClientToken.GenerateToken();
        }

        #region Buttons
        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (!MainBGWorkerIsWorking && service.StartCollectingSwipes(clientToken)) bgw_MainBGWorker.RunWorkerAsync();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            dgv_Swipes.Rows.Clear();
            if (!DatabaseBGWorkerIsWorking) bgw_UpdateDatabaseBGWorker.RunWorkerAsync("update");
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            if (!DatabaseBGWorkerIsWorking)
            {
                bgw_UpdateDatabaseBGWorker.RunWorkerAsync("clear");
                dgv_Swipes.Rows.Clear();
            }
        }
        #endregion
        #region Main BackgroundWorker
        private void bgw_MainBGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MainBGWorkerIsWorking = true;

            terminals = new List<Terminal>();

            bgw_MainBGWorker.ReportProgress(0);
            InitializeTerminals();
            flag = false;

            while (!flag)
            {
                flag = IsProcessFinished();
                UpdateTable();
                bgw_MainBGWorker.ReportProgress(GetProgress());
                Thread.Sleep(100);
            }

            bgw_MainBGWorker.ReportProgress(100);
            terminals.Clear();

            MainBGWorkerIsWorking = false;
        }

        private bool IsProcessFinished() => terminals.Where(t => t.Status == "Waiting" || t.Status == "InProcess").Count() == 0;

        private int GetProgress() => terminals.Where(t => t.Status == "Finished").Count() * 100 / terminals.Count;

        private void InitializeTerminals()
        {
            foreach (KeyValuePair<string, string> terminal in service.GetStatus(clientToken))
                terminals.Add(new Terminal(terminal.Key, dgv_Terminals));
        }

        private void UpdateTable()
        {
            foreach (KeyValuePair<string, string> status in service.GetStatus(clientToken))
                terminals.Where(t => t.Ip == status.Key).FirstOrDefault().UpdateStatus(status.Value, dgv_Terminals);
        }

        private void bgw_MainBGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgb_Progress.Value = e.ProgressPercentage;
            lbl_ProgressText.Text = "Task is " + e.ProgressPercentage + "% complete";
        }

        private void bgw_MainBGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbl_ProgressText.Text = "Done!";
            clientToken = ClientToken.GenerateToken();
        }
        #endregion
        #region Database BackgroundWorker
        private void bgw_DatabaseBGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DatabaseBGWorkerIsWorking = true;

            string action = (string)e.Argument;

            switch (action)
            {
                case "update":
                    List<Swipe> swipes = new List<Swipe>();

                    foreach (string[] parameters in service.GetDatabase())
                    {
                        Swipe swipe = new Swipe(parameters);
                        swipes.Add(swipe);
                        swipe.AddSwipeToDataGridView(dgv_Swipes);
                    }
                    break;
                case "clear":
                    service.TruncateDatabase();
                    break;
            }

            DatabaseBGWorkerIsWorking = false;
        }
        #endregion
    }
}
