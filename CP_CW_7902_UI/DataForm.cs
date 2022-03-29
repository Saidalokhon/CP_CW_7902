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
        #region Local Variables
        // Create local variables.

        // Create instance of the service.
        TerminalService.ITerminalService service;
        // Create list of terminals.
        List<Terminal> terminals;
        // Create flag variable (boolean).
        static bool flag;
        // Create client name string.
        string clientToken;

        // Create variable to control the work of 
        // background workers.
        bool MainBGWorkerIsWorking = false;
        bool DatabaseBGWorkerIsWorking = false;
        #endregion

        public DataForm()
        {
            InitializeComponent();
            // Initialize service.
            service = new TerminalService.TerminalServiceClient();
            // Generate client token.
            clientToken = ClientToken.GenerateToken();
        }

        #region Buttons
        private void btn_Start_Click(object sender, EventArgs e)
        {
            // Check if background worker is working
            // Check if can start collecting swipes
            if (!MainBGWorkerIsWorking && service.StartCollectingSwipes(clientToken)) bgw_MainBGWorker.RunWorkerAsync();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            // Clears the database table.
            dgv_Swipes.Rows.Clear();
            // Check if background worker is working
            if (!DatabaseBGWorkerIsWorking) bgw_UpdateDatabaseBGWorker.RunWorkerAsync("update");
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            // Check if background worker is working
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
            // Set the status of the worker.
            MainBGWorkerIsWorking = true;

            // Create new empty list of terminals.
            terminals = new List<Terminal>();

            // Set progress of the worker to 0.
            bgw_MainBGWorker.ReportProgress(0);
            InitializeTerminals();
            // Set flag to false.
            flag = false;

            while (!flag)
            {
                // Check if process is finished.
                flag = IsProcessFinished();
                // Refresh the table.
                UpdateTable();
                // Get the process percentage.
                bgw_MainBGWorker.ReportProgress(GetProgress());
                // Sleep for 100 miliseconds.
                Thread.Sleep(100);
            }

            // Report that the work is done.
            bgw_MainBGWorker.ReportProgress(100);
            // Clear the terminals.
            terminals.Clear();

            // Set the status of the worker.
            MainBGWorkerIsWorking = false;
        }

        /// <summary>
        /// The method checks if there are unfinished terminals in the list.
        /// </summary>
        private bool IsProcessFinished() => terminals.Where(t => t.Status == "Waiting" || t.Status == "InProcess").Count() == 0;

        /// <summary>
        /// The method gets the percentage of finished terminals.
        /// </summary>
        private int GetProgress() => terminals.Where(t => t.Status == "Finished").Count() * 100 / terminals.Count;

        /// <summary>
        /// The method creates the list of terminals.
        /// </summary>
        private void InitializeTerminals()
        {
            foreach (KeyValuePair<string, string> terminal in service.GetStatus(clientToken))
                terminals.Add(new Terminal(terminal.Key, dgv_Terminals));
        }

        /// <summary>
        /// The method gets statuses of all terminals and updates the table.
        /// </summary>
        private void UpdateTable()
        {
            foreach (KeyValuePair<string, string> status in service.GetStatus(clientToken))
                terminals.Where(t => t.Ip == status.Key).FirstOrDefault().UpdateStatus(status.Value, dgv_Terminals);
        }

        private void bgw_MainBGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Show the progress in progress bar.
            pgb_Progress.Value = e.ProgressPercentage;
            // Show the percentage of the progress.
            lbl_ProgressText.Text = "Task is " + e.ProgressPercentage + "% complete";
        }

        private void bgw_MainBGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Print that the work is done.
            lbl_ProgressText.Text = "Done!";
            // Refresh the client token.
            clientToken = ClientToken.GenerateToken();
        }
        #endregion
        #region Database BackgroundWorker
        private void bgw_DatabaseBGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Set the status of the worker.
            DatabaseBGWorkerIsWorking = true;

            // Action that should be taken (update or clear).
            string action = (string)e.Argument;

            switch (action)
            {
                case "update":
                    // Clreate new list of swipes.
                    List<Swipe> swipes = new List<Swipe>();

                    // Get the swipes from database and show them in table.
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

            // Set the status of the worker.
            DatabaseBGWorkerIsWorking = false;
        }
        #endregion
    }
}
