using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_CW_7902_DAL.DBO
{
    public class Swipe : ISwipe
    {
        public int Id { get; set; }
        public string SwipeId { get; set; }
        public DateTime Time { get; set; }
        public string Direction { get; set; }
        public string TerminalIp { get; set; }
    }
}
