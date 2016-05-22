using MCS.Library.Test.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Services.Test.Contracts
{
    [ServiceContract]
    public interface IOrderTransactionService
    {
        [OperationContract]
        void SetOrderStatus(string processID, string orderID, OrderStatus status);

        [OperationContract]
        void ResetOrderStatus(string orderID, OrderStatus status);

        [OperationContract]
        void RollbackOrderStatus(string processID, string orderID);
    }
}
