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
    [DataExecutorDescription("分配教师")]
    public class AssignTeacherExecutor : PPTSEditCustomerExecutorBase<CustomerTeacherRelationCollection>
    {
        public AssignTeacherExecutor(CustomerTeacherRelationCollection Model)
            : base(Model,null)
        {

        }
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            CustomerTeacherRelationAdapter.Instance.InsertCollection(Model);
            foreach (var item in Model)
            {
                CustomerTeacherAssignApply ctaa = new CustomerTeacherAssignApply() {
                      ID= UuidHelper.NewUuidString(),
                      CustomerTeacherRelationID = item.ID,
                      CustomerID = item.CustomerID,
                      NewTeacherID = item.TeacherID,
                      NewTeacherJobID = item.TeacherJobID,
                      NewTeacherName = item.TeacherName,
                      NewTeacherOACode = item.TeacherOACode,
                      NewTeacherJobOrgID = item.TeacherJobOrgID,
                      NewTeacherJobOrgName = item.TeacherJobOrgName
                };
                ctaa.FillCreator();
                CustomerTeacherAssignApplyAdapter.Instance.UpdateInContext(ctaa);
            }
        }
    }
}