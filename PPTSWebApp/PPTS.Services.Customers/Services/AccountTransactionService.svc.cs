using PPTS.Contracts.Customers.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Data.Customers.Entities;
using MCS.Library.WcfExtensions;
using System.ServiceModel.Web;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Core;

namespace PPTS.Services.Customers.Services
{

    public class AccountTransactionService : IAccountTransactionService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void DebitAccount(string processID, string accountId, decimal money)
        {
            AccountAdapter.Instance.Load(accountId).IsNotNull(account =>
            {
                if (account.AccountMoney >= money)
                {
                    TxProcessExecutor.GetExecutor(processID).PrepareData(tp =>
                    {
                        tp.CurrentActivity.Context["money"] = money;
                    }).ExecuteMoveTo(tp =>
                    {
                        account.AccountMoney -= money;
                        AccountAdapter.Instance.Update(account);
                    });
                }

            });

        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void RollbackDebitAccount(string processID, string accountId, decimal money)
        {
            AccountAdapter.Instance.Load(accountId).IsNotNull(account =>
            {
                TxProcessExecutor.GetExecutor(processID).ExecuteRollback(tp =>
                {
                    money = tp.CurrentActivity.Context.GetValue("money", money);
                    account.AccountMoney += money;
                    AccountAdapter.Instance.Update(account);
                });
            });
        }
    }
}
