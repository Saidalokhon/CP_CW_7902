using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_CW_7902_DAL.DBO
{
    public interface ISwipe
    {
        string SwipeId { get; set; }
        DateTime Time { get; set; }
        string Direction { get; set; }
    }
}
