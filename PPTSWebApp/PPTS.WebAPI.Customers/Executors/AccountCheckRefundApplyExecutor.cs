using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using PPTS.Data.Common.Security;
using PPTS.Data.Common;
using MCS.Library.Principal;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("对账退费单")]
    public class AccountCheckRefundApplyExecutor : PPTSCustomerExecutorBase
    {
        string[] _applyIDs;
        public AccountCheckRefundApplyExecutor(string[] applyIDs)
            : base("Check")
        {
            _applyIDs = applyIDs;
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
            List<RefundApplyModel> list = new List<RefundApplyModel>();
            foreach (string applyID in _applyIDs)
            {
                AccountRefundApply apply = AccountRefundApplyAdapter.Instance.LoadByApplyID(applyID);
                if (apply != null && apply.VerifyStatus == RefundVerifyStatus.Refunded && apply.CheckStatus == CheckStatusDefine.UnCheck)
                {
                    RefundApplyModel model = AutoMapper.Mapper.DynamicMap<RefundApplyModel>(apply);
                    model.FillModifier();
                    model.InitChecker(DeluxeIdentity.CurrentUser);
                    AccountRefundApplyAdapter.Instance.Update(model);
                    list.Add(model);
                }
            }
            return list;
        }
    }
}