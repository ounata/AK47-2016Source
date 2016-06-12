using PPTS.Data.Customers.Entities;
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


        /// <summary>
        /// 提交订单后同步服务费
        /// </summary>
        /// <param name="assets"></param>
        [OperationContract]
        void SyncExpense(string processID, CustomerExpenseRelationCollection collection);

    }
}
