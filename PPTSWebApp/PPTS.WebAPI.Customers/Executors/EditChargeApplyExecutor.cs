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

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑缴费单")]
    public class EditChargeApplyExecutor : PPTSEditAccountExecutorBase<ChargeApplyModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public EditChargeApplyExecutor(ChargeApplyModel model)
            : base(model, null)
        {
            model.NullCheck("model");            
        }

        private void Init(ChargeApplyModel apply)
        {
            apply.FillCreator();
            apply.FillModifier();
            apply.InitApplier();
            apply.InitApproverFromApplier();
            apply.InitSubmitterFromApplier();

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
            this.Init(this.Model);
            AccountChargeApplyAdapter.Instance.UpdateInContext(this.Model);
            AccountChargeAllotAdapter.Instance.DeleteInContext(builder => builder.AppendItem("ApplyID", this.Model.ApplyID));
            foreach (ChargeAllotItemModel itemModel in this.Model.Allot.Items)
            {
                if (!itemModel.TeacherID.IsNullOrEmpty())
                {
                    itemModel.AllotID = Guid.NewGuid().ToString().ToUpper();
                    AccountChargeAllotAdapter.Instance.UpdateInContext(itemModel);
                }
            }
            base.PrepareData(context);            
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