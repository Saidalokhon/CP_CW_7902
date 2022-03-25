using System.Windows.Forms;

namespace CP_CW_7902_UI.Models
{
    public class Swipe
    {
        public Swipe(string[] parameters)
        {
            SwipeId = parameters[0];
            Time = parameters[1];
            Direction = parameters[2];
            TerminalIp = parameters[3];
        }

        public void AddSwipeToDataGridView(DataGridView dataGridView)
        {
            dataGridView.Invoke((MethodInvoker)delegate
            {
                dataGridView.Rows.Add(new[] { SwipeId, Time, Direction, TerminalIp });
            });
        }

        public string SwipeId { get; set; }
        public string Time { get; set; }
        public string Direction { get; set; }
        public string TerminalIp { get; set; }
    }
}
