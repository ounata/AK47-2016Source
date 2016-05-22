using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using System;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑反馈")]
    public class EditableStudentVisitExecutor : PPTSEditCustomerExecutorBase<EditableCustomerVisitModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public EditableStudentVisitExecutor(EditableCustomerVisitModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.CustomerVisit.NullCheck("CustomerVisit");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            ///编辑人信息
            this.Model.CustomerVisit.FillModifier();

            CustomerVisitAdapter.Instance.UpdateInContext(this.Model.CustomerVisit);
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);

            context.Logs.ForEach(log => log.ResourceID = this.Model.CustomerVisit.VisitID);
        }
    }
}