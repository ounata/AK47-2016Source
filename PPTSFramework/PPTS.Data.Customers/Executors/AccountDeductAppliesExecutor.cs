using System;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.Data.Common.Security;
using System.Linq;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Customers.Executors;
using System.Collections.Generic;
using MCS.Library.Net.SNTP;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("综合服务费扣除")]
    public class AccountDeductAppliesExecutor : PPTSEditCustomerExecutorBase<AccountDeductAppliesModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public AccountDeductAppliesExecutor(AccountDeductAppliesModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        private void Init(List<CustomerExpenseRelation> expenses, List<AccountDeductApply> applies, List<Account> accounts)
        {
            Dictionary<string, CustomerStaffRelationCollection> dict = new Dictionary<string, CustomerStaffRelationCollection>();
            foreach (AccountDeductApply apply in applies)
            {
                if (apply.ApplyID.IsNullOrEmpty())
                    apply.ApplyID = Guid.NewGuid().ToString().ToUpper();

                apply.ApplyTime = SNTPClient.AdjustedTime;
                apply.SubmitTime = SNTPClient.AdjustedTime;
                apply.ApproveTime = SNTPClient.AdjustedTime;

                if (!dict.ContainsKey(apply.CustomerID))
                    dict.Add(apply.CustomerID, CustomerStaffRelationAdapter.Instance.LoadByCustomerID(apply.CustomerID));
                CustomerStaffRelationCollection c = dict[apply.CustomerID];
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
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Init(this.Model.Expenses, this.Model.Applies, this.Model.Accounts);
            foreach (CustomerExpenseRelation expense in this.Model.Expenses)
                CustomerExpenseRelationAdapter.Instance.UpdateInContext(expense);
            foreach (AccountDeductApply apply in this.Model.Applies)
                AccountDeductApplyAdapter.Instance.UpdateInContext(apply);
            foreach (Account account in this.Model.Accounts)
                AccountAdapter.Instance.UpdateInContext(account);
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