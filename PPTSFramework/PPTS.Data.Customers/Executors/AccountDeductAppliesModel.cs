using MCS.Library.Net.SNTP;
using PPTS.Contracts.Proxies;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Contracts.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Executors
{
    public class AccountDeductAppliesModel
    {
        /// <summary>
        /// 综合服务费
        /// </summary>
        public List<CustomerExpenseRelation> Expenses
        {
            get;
            private set;
        }

        /// <summary>
        /// 扣减单
        /// </summary>
        public List<AccountDeductApply> Applies
        {
            get;
            private set;
        }

        /// <summary>
        /// 账户
        /// </summary>
        public List<Account> Accounts
        {
            get;
            private set;
        }

        public AccountDeductAppliesModel(List<CustomerExpenseRelation> expenses)
        {
            this.Expenses = expenses;
        }

        public void Prepare()
        {
            Dictionary<string, Customer> customerDict = new Dictionary<string, Customer>();
            Dictionary<string, Account> accountDict = new Dictionary<string, Account>();
            List<AccountDeductApply> applies = new List<AccountDeductApply>();
            foreach (CustomerExpenseRelation expense in this.Expenses)
            {
                Customer customer = null;
                if (!customerDict.ContainsKey(expense.CustomerID))
                {
                    customer = CustomerAdapter.Instance.Load(expense.CustomerID);
                    if (customer == null)
                        throw new Exception(string.Format("学员ID为 {0} 的信息不存在", expense.CustomerID));
                    customerDict.Add(expense.CustomerID, customer);
                }
                customer = customerDict[expense.CustomerID];

                Account account = null;
                if (!accountDict.ContainsKey(expense.AccountID))
                {
                    account = AccountAdapter.Instance.Load(expense.AccountID);
                    if (account == null)
                        throw new Exception(string.Format("账户ID为 {0} 的信息不存在", expense.AccountID));

                    AssetStatisticQueryResult asset = PPTSAssetQueryServiceProxy.Instance.QueryAssetStatisticByAccountID(expense.AccountID);
                    if (asset != null)
                        account.AssetMoney = asset.AssetMoney;
                    accountDict.Add(expense.AccountID, account);
                }
                account = accountDict[expense.AccountID];

                AccountDeductApply apply = new AccountDeductApply();
                apply.AccountID = expense.AccountID;
                apply.CampusID = customer.CampusID;
                apply.CampusName = customer.CampusName;
                apply.CustomerID = customer.CustomerID;
                apply.CustomerCode = customer.CustomerCode;
                apply.CustomerName = customer.CustomerName;
                apply.ApplyID = Guid.NewGuid().ToString().ToUpper();

                apply.ApplyTime = SNTPClient.AdjustedTime;
                apply.ApplyStatus = ApplyStatusDefine.Approved;

                apply.SubmitTime = apply.ApplyTime;
                apply.ApproveTime = apply.ApplyTime;

                apply.ExpenseID = expense.ExpenseID;
                apply.ExpenseType = expense.ExpenseType;
                apply.ExpenseMoney = expense.ExpenseMoney;

                apply.ThatDiscountID = account.DiscountID;
                apply.ThatDiscountCode = account.DiscountCode;
                apply.ThatDiscountBase = account.DiscountBase;
                apply.ThatDiscountRate = account.DiscountRate;
                apply.ThatAccountMoney = account.AccountMoney;
                apply.ThatAccountValue = account.AccountValue;

                account.AccountMoney -= apply.ExpenseMoney;

                apply.ThisDiscountID = account.DiscountID;
                apply.ThisDiscountCode = account.DiscountCode;
                apply.ThisDiscountBase = account.DiscountBase;
                apply.ThisDiscountRate = account.DiscountRate;
                apply.ThisAccountMoney = account.AccountMoney;
                apply.ThisAccountValue = account.AccountValue;

                applies.Add(apply);
            }

            this.Applies = applies;
            this.Accounts = accountDict.Values.ToList();
        }
    }
}
