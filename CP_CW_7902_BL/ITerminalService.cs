using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CP_CW_7902_BL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITerminalService" in both code and config file together.
    [ServiceContract]
    public interface ITerminalService
    {
        [OperationContract]
        void StartCollectingSwipes();

        [OperationContract]
        Dictionary<string, string> GetStatus();

        [OperationContract]
        void TruncateDatabase();

        [OperationContract]
        List<List<string>> GetDatabase();
    }
}
