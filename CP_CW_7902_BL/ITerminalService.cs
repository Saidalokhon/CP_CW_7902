using System.Collections.Generic;
using System.ServiceModel;

namespace CP_CW_7902_BL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITerminalService" in both code and config file together.
    [ServiceContract]
    public interface ITerminalService
    {
        [OperationContract]
        bool StartCollectingSwipes(string clientToken);

        [OperationContract]
        Dictionary<string, string> GetStatus(string clientToken);

        [OperationContract]
        void TruncateDatabase();

        [OperationContract]
        List<List<string>> GetDatabase();
    }
}
