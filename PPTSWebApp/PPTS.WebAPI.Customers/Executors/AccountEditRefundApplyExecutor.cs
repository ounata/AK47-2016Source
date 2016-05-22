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
    [DataExecutorDescription("编辑退费单")]
    public class AccountEditRefundApplyExecutor : PPTSEditCustomerExecutorBase<RefundApplyModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public AccountEditRefundApplyExecutor(RefundApplyModel model)
            : base(model, null)
        {
            model.NullCheck("model");            
        }

        private void Init(RefundApplyModel apply)
        {
            if (apply.ApplyID.IsNullOrEmpty())
                apply.ApplyID = Guid.NewGuid().ToString().ToUpper();

            apply.FillCreator();
            apply.FillModifier();
            apply.InitApplier(DeluxeIdentity.CurrentUser);
            apply.InitSubmitter(DeluxeIdentity.CurrentUser);

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

            for (int i = apply.Allot.Items.Count - 1; i >= 0; i++)
            {
                RefundAllotItemModel item = apply.Allot.Items[i];
                if (item.TeacherID.IsNullOrEmpty())
                    apply.Allot.Items.RemoveAt(i);
                else
                {
                    item.ApplyID = apply.ApplyID;
                    item.AllotID = Guid.NewGuid().ToString().ToUpper();
                }
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Init(this.Model);
            AccountRefundApplyAdapter.Instance.UpdateInContext(this.Model);
            AccountRefundAllotAdapter.Instance.DeleteInContext(builder => builder.AppendItem("ApplyID", this.Model.ApplyID));
            foreach (RefundAllotItemModel itemModel in this.Model.Allot.Items)
                AccountRefundAllotAdapter.Instance.UpdateInContext(itemModel);
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