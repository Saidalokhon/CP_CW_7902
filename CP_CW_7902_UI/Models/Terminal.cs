using System.Drawing;
using System.Windows.Forms;

namespace CP_CW_7902_UI.Models
{
    public class Terminal
    {
        /// <summary>
        /// The constructor creates new terminal from passed IP and adds
        /// the data to the table.
        /// </summary>
        /// <param name="dataGridView">The table the terminal to be added to.</param>
        /// <param name="ip">The IP of the terminal.</param>
        public Terminal(string ip, DataGridView dataGridView)
        {
            Ip = ip;

            dataGridView.Invoke((MethodInvoker)delegate
            {
                dataGridView.Rows.Add(new[] { Ip, "Waiting" });
            });

            UpdateStatus("waiting", dataGridView);
        }

        /// <summary>
        /// The method updates the status of the terminal in the table.
        /// </summary>
        /// <param name="dataGridView">The table the terminal is in.</param>
        /// <param name="status">The updated status.</param>
        public void UpdateStatus(string status, DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value.ToString() == Ip)
                {
                    switch (status.ToLower().Trim())
                    {
                        case "inprocess":
                            row.DefaultCellStyle.BackColor = Color.Red;
                            Status = "InProcess";
                            row.Cells[1].Value = Status;
                            break;
                        case "waiting":
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells[1].Value = Status;
                            Status = "Waiting";
                            break;
                        case "finished":
                            row.DefaultCellStyle.BackColor = Color.Green;
                            row.Cells[1].Value = Status;
                            Status = "Finished";
                            break;
                    }
                }
            }
        }

        public string Ip { get; set; }
        public string Status { get; set; }
    }
}
