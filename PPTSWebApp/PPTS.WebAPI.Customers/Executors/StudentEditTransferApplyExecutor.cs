using System;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.WebAPI.Customers.ViewModels.Students;
using PPTS.Data.Common.Security;
using System.Linq;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Customers.Executors;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑转学申请单")]
    public class StudentEditTransferApplyExecutor : PPTSEditCustomerExecutorBase<StudentTransferApplyModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public StudentEditTransferApplyExecutor(StudentTransferApplyModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        private void Init(StudentTransferApplyModel apply)
        {
            if (apply.ApplyID.IsNullOrEmpty())
                apply.ApplyID = Guid.NewGuid().ToString().ToUpper();

            apply.FillCreator();
            apply.FillModifier();
            apply.InitApplier(DeluxeIdentity.CurrentUser);
            apply.InitSubmitter(DeluxeIdentity.CurrentUser);
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Init(this.Model);
            CustomerTransferApplyAdapter.Instance.UpdateInContext(this.Model);
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