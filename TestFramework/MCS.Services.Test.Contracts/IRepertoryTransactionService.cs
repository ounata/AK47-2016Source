using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Services.Test.Contracts
{
    [ServiceContract]
    public interface IRepertoryTransactionService
    {
        [OperationContract]
        void ChangeUsedQuantity(string processID, string repertoryID, int changeQuantity);

        [OperationContract]
        void RollbackUsedQuantity(string processID, string repertoryID);
    }
}
