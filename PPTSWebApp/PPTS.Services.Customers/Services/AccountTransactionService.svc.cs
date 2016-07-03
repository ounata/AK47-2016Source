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
using PPTS.Data.Customers.Executors;

namespace PPTS.Services.Customers.Services
{

    public class AccountTransactionService : IAccountTransactionService
    {


        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void DebitAccount(string processID, string accountId, decimal money, AccountRecord record)
        {
            AccountAdapter.Instance.Load(accountId).IsNotNull(m =>
            {
                (m.AccountMoney < money).TrueThrow("帐户余额不足！");

                (m.AccountMoney >= money).TrueAction(() =>
                {

                    TxProcessExecutor.GetExecutor(processID).PrepareData(tp =>
                    {
                        tp.CurrentActivity.Context["money"] = money;
                        tp.CurrentActivity.Context["record"] = record;
                        tp.CurrentActivity.Context["ModifierID"] = m.ModifierID;
                        tp.CurrentActivity.Context["ModifierName"] = m.ModifierName;


                    }).ExecuteMoveTo(tp =>
                    {
                        m.ModifierID = record.BillerID;
                        m.ModifierName = record.BillerName;
                        m.AccountMoney -= money;
                        new AccountDebitExecutor("Debit") { Account = m, Record = record }.Execute();
                    });

                });


            });
        }


        

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void RollbackDebitAccount(string processID, string accountId, decimal money, AccountRecord record)
        {
            AccountAdapter.Instance.Load(accountId).IsNotNull(account =>
            {
                TxProcessExecutor.GetExecutor(processID).ExecuteRollback(tp =>
                {
                    money = tp.CurrentActivity.Context.GetValue("money", money);
                    record = tp.CurrentActivity.Context.GetValue("record", record);

                    string modifierID = null, modifierName = null;
                    modifierID = tp.CurrentActivity.Context.GetValue("ModifierID", modifierID);
                    modifierName = tp.CurrentActivity.Context.GetValue("ModifierName", modifierName);

                    (!modifierID.IsNullOrWhiteSpace()).TrueAction(() => { account.ModifierID = modifierID; });
                    (!modifierName.IsNullOrWhiteSpace()).TrueAction(() => { account.ModifierName = modifierName; });

                    account.AccountMoney += money;
                    new AccountDebitExecutor("RollbackDebit") { Account = account, Record = record }.Execute();
                });
            });
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void DebookAccount(string processID, string accountID, decimal money, AccountRecord record)
        {
            AccountAdapter.Instance.Load(accountID).IsNotNull(m =>
            {
                (m.AccountMoney < money).TrueThrow("帐户余额不足！");

                (m.AccountMoney >= money).TrueAction(() =>
                {

                    TxProcessExecutor.GetExecutor(processID).PrepareData(tp =>
                    {
                        tp.CurrentActivity.Context["money"] = money;
                        tp.CurrentActivity.Context["record"] = record;
                        tp.CurrentActivity.Context["ModifierID"] = m.ModifierID;
                        tp.CurrentActivity.Context["ModifierName"] = m.ModifierName;


                    }).ExecuteMoveTo(tp =>
                    {
                        m.ModifierID = record.BillerID;
                        m.ModifierName = record.BillerName;
                        m.AccountMoney += money;
                        new AccountDebookExecutor("Debook") { Account = m, Record = record }.Execute();
                    });

                });


            });
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void RollbackDebookAccount(string processID, string accountID, decimal money, AccountRecord record)
        {
            AccountAdapter.Instance.Load(accountID).IsNotNull(account =>
            {
                TxProcessExecutor.GetExecutor(processID).ExecuteRollback(tp =>
                {
                    money = tp.CurrentActivity.Context.GetValue("money", money);
                    record = tp.CurrentActivity.Context.GetValue("record", record);

                    string modifierID = null, modifierName = null;
                    modifierID = tp.CurrentActivity.Context.GetValue("ModifierID", modifierID);
                    modifierName = tp.CurrentActivity.Context.GetValue("ModifierName", modifierName);

                    (!modifierID.IsNullOrWhiteSpace()).TrueAction(() => { account.ModifierID = modifierID; });
                    (!modifierName.IsNullOrWhiteSpace()).TrueAction(() => { account.ModifierName = modifierName; });

                    account.AccountMoney -= money;
                    new AccountDebitExecutor("RollbackDebook") { Account = account, Record = record }.Execute();
                });
            });
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SyncExpense(string processID, List<CustomerExpenseRelation> collection)
        {


            TxProcessExecutor.GetExecutor(processID).PrepareData(tp =>
            {
                (collection == null).TrueThrow("collection");

                tp.CurrentActivity.Context["CustomerExpenseRelationCollection"] = collection;
            }).ExecuteMoveTo(tp =>
            {
                var relations = new CustomerExpenseRelationCollection();
                collection.ForEach(m => relations.Add(m));

                CustomerExpenseRelationAdapter.Instance.SyncOrderExpenseRelation(relations);
            });

        }



    }
}
