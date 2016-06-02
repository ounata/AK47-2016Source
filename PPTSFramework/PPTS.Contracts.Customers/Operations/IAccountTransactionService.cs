using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Operations
{
    [ServiceContract]
    public interface IAccountTransactionService
    {
        /// <summary>
        /// 帐户扣钱
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="accountID"></param>
        /// <param name="money"></param>
        [OperationContract]
        void DebitAccount(string processID, string accountID, decimal money);

        /// <summary>
        /// 回滚 帐户扣钱
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="accountID"></param>
        [OperationContract]
        void RollbackDebitAccount(string processID, string accountID, decimal money);

    }
}
