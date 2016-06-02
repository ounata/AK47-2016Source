using PPTS.Data.Customers.Entities;
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
            set;
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

        }
    }
}
