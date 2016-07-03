using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("调出教师")]
    public class CalloutTeacherExecutor : PPTSEditCustomerExecutorBase<CustomerTeacherAssignApply>
    {
        public CalloutTeacherExecutor(CustomerTeacherAssignApply Model)
            : base(Model,null)
        {
           
        }
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            CustomerTeacherRelation ctr = CustomerTeacherRelationAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", Model.CustomerID).AppendItem("TeacherJobID", Model.OldTeacherJobID), DateTime.MinValue).FirstOrDefault();
            CustomerTeacherRelationAdapter.Instance.Delete(ctr);

            Model.ID = UuidHelper.NewUuidString();
            Model.CustomerTeacherRelationID = ctr.ID;
            Model.ApplyType = "2";
            Model.FillCreator();
            CustomerTeacherAssignApplyAdapter.Instance.UpdateInContext(Model);
        }
    }
}