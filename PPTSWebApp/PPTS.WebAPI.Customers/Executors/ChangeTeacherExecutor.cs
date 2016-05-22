using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("调换教师")]
    public class ChangeTeacherExecutor : PPTSEditCustomerExecutorBase<CustomerTeacherAssignApplie>
    {
        public ChangeTeacherExecutor(CustomerTeacherAssignApplie Model)
            : base(Model,null)
        {

        }
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            CustomerTeacherRelation ctr = CustomerTeacherRelationAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", Model.CustomerID).AppendItem("TeacherJobID", Model.OldTeacherJobID), DateTime.MinValue).FirstOrDefault();//查询
            ctr.TeacherID = Model.NewTeacherID;
            ctr.TeacherJobID = Model.NewTeacherJobID;
            ctr.TeacherJobOrgID = Model.NewTeacherOrgID;
            ctr.TeacherJobOrgName = Model.NewTeacherOrgName;
            ctr.TeacherName = Model.NewTeacherName;
            CustomerTeacherRelationAdapter.Instance.Update(ctr);

            Model.ID = UuidHelper.NewUuidString();
            Model.CustomerTeacherRelationID = ctr.ID;
            Model.FillCreator();
            CustomerTeacherAssignApplieAdapter.Instance.UpdateInContext(Model);
        }
    }
}