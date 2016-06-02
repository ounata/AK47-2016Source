using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.Data.Common.Security;
using PPTS.Data.Common;
using MCS.Library.Principal;
using System.Collections.Generic;
using System;
using MCS.Library.Net.SNTP;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("转学审批")]
    public class StudentApproveTransferApplyExecutor : PPTSEditCustomerExecutorBase<StudentApproveTransferModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public StudentApproveTransferApplyExecutor(StudentApproveTransferModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.DoOperation(context);
            return this.Model.Apply;
        }

        private void Init(StudentApproveTransferModel model, CustomerTransferApply apply, List<Account> accounts)
        {
            apply.ModifierID = model.ApproverID;
            apply.ModifierName = model.ApproverName;
            apply.ModifyTime = SNTPClient.AdjustedTime;

            apply.ApproverID = model.ApproverID;
            apply.ApproverName = model.ApproverName;
            apply.ApproverJobID = model.ApproverJobID;
            apply.ApproverJobName = model.ApproverJobName;
            apply.ApproveTime = model.ApproveTime;

            if (model.IsRefused)
                apply.ApplyStatus = ApplyStatusDefine.Refused;
            else if (model.IsFinalApprove)
                apply.ApplyStatus = ApplyStatusDefine.Approved;

            foreach (Account account in accounts)
            {
                account.ModifierID = model.ApproverID;
                account.ModifierName = model.ApproverName;
                account.ModifyTime = SNTPClient.AdjustedTime;
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            if (this.Model.Apply != null && this.Model.Apply.CanApprove)
            {
                this.Init(this.Model, this.Model.Apply, this.Model.Accounts);
                CustomerTransferApplyAdapter.Instance.UpdateInContext(this.Model.Apply);
                CustomerAdapter.Instance.UpdateInContext(this.Model.Customer);
                if (!this.Model.IsRefused)
                {
                    foreach (Account account in this.Model.Accounts)
                    {
                        AccountAdapter.Instance.UpdateInContext(account);
                    }
                }
            }
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