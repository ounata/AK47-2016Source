using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.Service;
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
    [DataExecutorDescription("分配教师")]
    public class AssignTeacherExecutor : PPTSEditCustomerExecutorBase<AssignTeacherModel>
    {
        public AssignTeacherExecutor(AssignTeacherModel Model)
            : base(Model,null)
        {

        }
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            CustomerTeacherRelationAdapter.Instance.InsertCollection(Model.ctrc);
            foreach (var item in Model.ctrc)
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
                      NewTeacherJobOrgName = item.TeacherJobOrgName,
                      ApplyType = "0"
                };
                ctaa.FillCreator();
                CustomerTeacherAssignApplyAdapter.Instance.UpdateInContext(ctaa);

                IUser user = PPTS.Data.Common.OGUExtensions.GetUserByID(item.TeacherID);//人员信息
                //Customer c = CustomerAdapter.Instance.Load(item.CustomerID);
                user.IsNotNull(action =>
                {
                    foreach (var sendItem in Model.sendEmailSMS)
                    {
                        switch (sendItem)
                        {
                            case 1:
                                //发邮件 
                                EmailMessage message = new EmailMessage(user.Email, "分配教师", string.Format(
                                    @"尊敬的{0}老师: 
                                            
                                            您名下有新分配的学员，请关注下课表的安排！",item.TeacherName));                                
                                EmailMessageAdapter.Instance.Insert(message);
                                break;
                            case 2:
                                //发短信
                                string msg = string.Format("您名下有新分配的学员，请关注下课表的安排！");
                                SMSTask t = new SMSTask();
                                t.SendSMSWithTask(user.GetUserMobile(), msg, "PPTS.WebAPI.Customers");
                                break;
                        }
                    }
                });                
            }            
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            #region 生成数据权限范围数据
            //ScopeAuthorization<Customer>.GetInstance(PPTS.Data.Customers.ConnectionDefine.PPTSCustomerConnectionName)
            //        .UpdateAuthByJobCollection(jobs.Select(job => job.ID).ToList(), ri.RecordID, RecordType.Customer, RelationType.Teacher);
            #endregion 生成数据权限范围数据

            return base.DoOperation(context);
        }
    }
}