using System;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using PPTS.Data.Common.Security;
using System.Linq;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Customers.Executors;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑转让单")]
    public class AccountEditTransferApplyExecutor : PPTSEditCustomerExecutorBase<TransferApplyModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public AccountEditTransferApplyExecutor(TransferApplyModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        private void Init(TransferApplyModel apply)
        {
            if (apply.ApplyID.IsNullOrEmpty())
                apply.ApplyID = UuidHelper.NewUuidString();

            apply.FillCreator();
            apply.FillModifier();
            apply.InitApplier(DeluxeIdentity.CurrentUser);
            apply.InitSubmitter(DeluxeIdentity.CurrentUser);
            apply.TransferType = AccountTransferType.TransferOut;
            if (apply.CustomerID == apply.BizCustomerID)
                throw new Exception("转出学员和转入学员不能是同一个人");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Init(this.Model);
            AccountTransferApplyAdapter.Instance.UpdateInContext(this.Model);
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
        }
    }
}