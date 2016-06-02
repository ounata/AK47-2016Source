using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Executors
{
    public class AccountApproveTransferModel : ApproveModelBase
    {
        public AccountTransferApply Apply
        {
            get;
            private set;
        }

        /// <summary>
        /// 转出方账户
        /// </summary>
        public Account Account
        {
            get;
            private set;
        }

        /// <summary>
        /// 转入方账户
        /// </summary>
        public Account BizAccount
        {
            get;
            set;
        }

        public void PrepareApprove()
        {
            AccountTransferApply apply = AccountTransferApplyAdapter.Instance.LoadByApplyID(this.BillID);
            if (apply == null)
                throw new Exception(string.Format("转让申请单ID为[{0}]的记录不存在", this.BillID));

            this.Apply = apply;
            Account account = AccountAdapter.Instance.LoadByAccountID(this.Apply.AccountID);
            if (account == null)
                throw new Exception(string.Format("账户ID为[{0}]的记录不存在", apply.AccountID));
            this.Account = account;
            Account bizAccount = AccountAdapter.Instance.LoadByAccountID(this.Apply.BizAccountID);
            this.BizAccount = bizAccount;
        }
    }
}
