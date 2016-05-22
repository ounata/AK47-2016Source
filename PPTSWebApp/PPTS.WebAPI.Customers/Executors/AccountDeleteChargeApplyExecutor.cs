using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.WebAPI.Customers.ViewModels.Accounts;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("删除缴费单")]
    public class AccountDeleteChargeApplyExecutor : PPTSCustomerExecutorBase
    {
        /// <summary>
        /// 缴费申请单ID
        /// </summary>
        private string _applyID;
        public AccountDeleteChargeApplyExecutor(string applyID)
            : base("Delete")
        {
            _applyID = applyID;
        }

        protected override string GetOperationDescription()
        {
            string description = base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(description))
            {
                return this.OperationType;
            }
            return description;
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            AccountChargeApply apply = AccountChargeApplyAdapter.Instance.LoadByApplyID(_applyID);
            if (apply != null && apply.PayStatus == PayStatusDefine.Unpay)
            {
                AccountChargeAllotAdapter.Instance.Delete(x => x.AppendItem("ApplyID", apply.ApplyID));
                AccountChargeApplyAdapter.Instance.Delete(x => x.AppendItem("ApplyID", apply.ApplyID));                
                return true;
            }
            return false;
        }
    }
}