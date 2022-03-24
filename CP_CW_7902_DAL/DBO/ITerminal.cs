using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_CW_7902_DAL.DBO
{
    public interface ITerminal
    {
        string Ip { get; set; }
        Statuses Status { get; set; }
    }
}
