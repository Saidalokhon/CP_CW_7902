using System.Windows.Forms;

namespace CP_CW_7902_UI.Models
{
    public class Swipe
    {
        /// <summary>
        /// The constructor creates the swipe from the passed array of string.
        /// </summary>
        /// <param name="parameters">Array of string containing parameters in specific order</param>
        public Swipe(string[] parameters)
        {
            SwipeId = parameters[0];
            Time = parameters[1];
            Direction = parameters[2];
            TerminalIp = parameters[3];
        }

        /// <summary>
        /// The method adds the swipe to the UI table.
        /// </summary>
        /// <param name="dataGridView">Table the swipe to be added to.</param>
        public void AddSwipeToDataGridView(DataGridView dataGridView)
        {
            // Pass the thread to the UI thread.
            dataGridView.Invoke((MethodInvoker)delegate
            {
                // Add the row to the table.
                dataGridView.Rows.Add(new[] { SwipeId, Time, Direction, TerminalIp });
            });
        }

        public string SwipeId { get; set; }
        public string Time { get; set; }
        public string Direction { get; set; }
        public string TerminalIp { get; set; }
    }
}
