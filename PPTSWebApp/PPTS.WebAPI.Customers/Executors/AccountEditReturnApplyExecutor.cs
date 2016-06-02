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
    [DataExecutorDescription("综合服务费返还")]
    public class AccountEditReturnApplyExecutor : PPTSEditCustomerExecutorBase<ReturnApplyModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public AccountEditReturnApplyExecutor(ReturnApplyModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        private void Init(ReturnApplyModel apply, Account account)
        {
            if (apply.ApplyID.IsNullOrEmpty())
                apply.ApplyID = Guid.NewGuid().ToString().ToUpper();

            apply.FillCreator();
            apply.FillModifier();
            apply.InitApplier(DeluxeIdentity.CurrentUser);
            apply.InitSubmitter(DeluxeIdentity.CurrentUser);
            apply.InitApprover(DeluxeIdentity.CurrentUser, ApplyStatusDefine.Approved);

            account.FillModifier();

            CustomerStaffRelationCollection c = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(apply.CustomerID);
            var consultant = c.Where(x => x.RelationType == CustomerRelationType.Consultant).SingleOrDefault();
            if (consultant != null)
            {
                apply.ConsultantID = consultant.StaffID;
                apply.ConsultantName = consultant.StaffName;
                apply.ConsultantJobID = consultant.StaffJobID;
            }
            var educator = c.Where(x => x.RelationType == CustomerRelationType.Educator).SingleOrDefault();
            if (educator != null)
            {
                apply.EducatorID = educator.StaffID;
                apply.EducatorName = educator.StaffName;
                apply.EducatorJobID = educator.StaffJobID;
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Init(this.Model, this.Model.PreparedAccount);
            AccountReturnApplyAdapter.Instance.UpdateInContext(this.Model);
            AccountAdapter.Instance.UpdateInContext(this.Model.PreparedAccount);
            CustomerExpenseRelationAdapter.Instance.DeleteInContext(this.Model.CustomerID, this.Model.ExpenseID);
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
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