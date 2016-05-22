using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Executors
{
    public class AccountApproveRefundModel : ApproveModelBase
    {
        public AccountRefundApply Apply
        {
            set;
            get;
        }

        public void PrepareApprove()
        {
            AccountRefundApply apply = AccountRefundApplyAdapter.Instance.LoadByApplyID(this.BillID);
            if (apply == null)
                throw new Exception(string.Format("退费单ID为[{0}]的记录不存在", this.BillID));
            this.Apply = apply;
        }
    }
}
