using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
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
    [DataExecutorDescription("调换教师")]
    public class ChangeTeacherExecutor : PPTSEditCustomerExecutorBase<ChangeTeacherModel>
    {
        public ChangeTeacherExecutor(ChangeTeacherModel Model)
            : base(Model,null)
        {

        }
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            CustomerTeacherRelation ctr = CustomerTeacherRelationAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", Model.cta.CustomerID).AppendItem("TeacherJobID", Model.cta.OldTeacherJobID), DateTime.MinValue).FirstOrDefault();//查询
            ctr.TeacherID = Model.cta.NewTeacherID;
            ctr.TeacherJobID = Model.cta.NewTeacherJobID;
            ctr.TeacherJobOrgID = Model.cta.NewTeacherJobOrgID;
            ctr.TeacherJobOrgName = Model.cta.NewTeacherJobOrgName;
            ctr.TeacherName = Model.cta.NewTeacherName;
            CustomerTeacherRelationAdapter.Instance.Update(ctr);

            Model.cta.ID = UuidHelper.NewUuidString();
            Model.cta.CustomerTeacherRelationID = ctr.ID;
            Model.cta.FillCreator();
            Model.cta.ApplyType = "1";
            CustomerTeacherAssignApplyAdapter.Instance.UpdateInContext(Model.cta);

            IUser user = PPTS.Data.Common.OGUExtensions.GetUserByID(Model.cta.NewTeacherID);//人员信息
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
                                            
                                            您名下有新分配的学员，请关注下课表的安排！", Model.cta.NewTeacherName));
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
}