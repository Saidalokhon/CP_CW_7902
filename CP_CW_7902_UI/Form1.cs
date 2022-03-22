using CP_CW_7902_UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CP_CW_7902_UI
{
    public partial class Form1 : Form
    {
        TerminalService.ITerminalService service;
        List<Terminal> terminals;
        static bool flag;

        public Form1()
        {
            InitializeComponent();
            service = new TerminalService.TerminalServiceClient();
        }

        private void EnableButton(bool isEnabled)
        {
            button1.Invoke((MethodInvoker)delegate
            {
                button1.Enabled = isEnabled;
            });
        }

        private void InitializeTerminals()
        {
            foreach (KeyValuePair<string, string> terminal in service.GetStatus())
                terminals.Add(new Terminal(terminal.Key, dataGridView1));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            terminals = new List<Terminal>();
            EnableButton(false);
            service.StartCollectingSwipes();
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        private bool IsProcessFinished() => terminals.Where(t => t.Status == Terminal.Statuses.Waiting || t.Status == Terminal.Statuses.InProcess).Count() == 0;

        private int GetProgress() => terminals.Where(t => t.Status == Terminal.Statuses.Finished).Count() * 100 / terminals.Count;

        private void UpdateTable()
        {
            foreach (KeyValuePair<string, string> status in service.GetStatus())
                terminals.Where(t => t.Ip == status.Key).FirstOrDefault().UpdateTerminalStatus(status.Value, dataGridView1);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(0);
            InitializeTerminals();
            flag = false;

            while (!flag)
            {
                flag = IsProcessFinished();
                UpdateTable();
                backgroundWorker1.ReportProgress(GetProgress());
                Thread.Sleep(100);
            }

            terminals.Clear();
            EnableButton(true);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Task is " + e.ProgressPercentage + "% complete";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = "Done!";
        }
    }
}
