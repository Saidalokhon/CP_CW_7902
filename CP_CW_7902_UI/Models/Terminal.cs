using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CP_CW_7902_UI.Models
{
    public class Terminal
    {
        public Terminal(string ip, DataGridView dataGridView)
        {
            Ip = ip;

            dataGridView.Invoke((MethodInvoker)delegate 
            { 
                dataGridView.Rows.Add(new[] { Ip, Status.ToString() }); 
            });

            RowCount = dataGridView.Rows.Count - 1;

            UpdateTerminalStatus("waiting", dataGridView);
        }

        public void UpdateTerminalStatus(string status, DataGridView dataGridView)
        {
            Status = 
                status.ToLower().Equals("waiting") ? Statuses.Waiting : 
                status.ToLower().Equals("inprocess") ? Statuses.InProcess :
                status.ToLower().Equals("finished") ? Statuses.Finished : throw new Exception($"Unknown status { status }");
            dataGridView.Invoke((MethodInvoker)delegate
            {
                switch(Status)
                {
                    case Terminal.Statuses.Waiting:
                        dataGridView.Rows[RowCount].DefaultCellStyle.BackColor = Color.Red;
                        break;
                    case Terminal.Statuses.InProcess:
                        dataGridView.Rows[RowCount].DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    case Terminal.Statuses.Finished:
                        dataGridView.Rows[RowCount].DefaultCellStyle.BackColor = Color.Green;
                        break;
                }
                dataGridView.Rows[RowCount].Cells["Status"].Value = Status.ToString();
            });
        }

        public int RowCount { get; set; }
        public string Ip { get; set; }
        public Statuses Status { get; set; }

        public enum Statuses
        {
            Waiting,
            InProcess,
            Finished
        }
    }
}
