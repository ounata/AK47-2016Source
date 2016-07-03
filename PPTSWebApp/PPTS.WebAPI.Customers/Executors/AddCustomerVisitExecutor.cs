using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.Service;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using PPTS.WebAPI.Customers.ViewModels.Students;
using System;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("增加回访")]
    public class AddCustomerVisitExecutor : PPTSEditCustomerExecutorBase<CreatableCustomerVisitModel>
    {
        /// <summary>
        /// 添加回访
        /// </summary>
        /// <param name="model"></param>
        public AddCustomerVisitExecutor(CreatableCustomerVisitModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.CustomerVisit.NullCheck("CustomerVisit");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            DateTime now = SNTPClient.AdjustedTime;
            DateTime tempStart = now.Date.AddDays(-3);
            DateTime tempEnd = now.Date.AddDays(1);

            if (tempStart > this.Model.CustomerVisit.VisitTime  || this.Model.CustomerVisit.VisitTime > tempEnd)
            {
                throw new Exception("回访时间允许选择当前日期及当前日期的前三天");
            }

            //this.Model.Customer.FillCreator();
            this.Model.CustomerVisit.VisitID = UuidHelper.NewUuidString();

            this.Model.CustomerVisit.CampusID = this.Model.CampusID;

            this.Model.CustomerVisit.CampusName = this.Model.CampusName;

            ///写入人信息
            this.Model.CustomerVisit.FillCreator();
            ///编辑人信息
            this.Model.CustomerVisit.FillModifier();
            ///
            this.Model.CustomerVisit.FillAccepter(DeluxeIdentity.CurrentUser);
            ///受理人信息
            CustomerVisitAdapter.Instance.UpdateInContext(this.Model.CustomerVisit);

            ///得到学管师相关信息
            EducatorModel EducatorModel_ = new EducatorModel(this.Model.CustomerVisit.CustomerID);

            StudentModel StudentModel_ = CreatableVisitBatchModel.GetStudentByID(this.Model.CustomerVisit.CustomerID);

            ///发邮件
            if (this.Model.CustomerVisit.SelectType == (int)RemainType.Email)
            {
                EmailMessage message = new EmailMessage("lihui_10@xueda.com", "预约回访提醒", EducatorModel.GenericContextMessage(EducatorModel_, StudentModel_.CustomerName, this.Model.CustomerVisit.VisitTime, RemainType.Email));

                EmailMessageAdapter.Instance.Insert(message);
            }
            ///发短信
            if (this.Model.CustomerVisit.SelectType == (int)RemainType.Message)
            {
                SMSTask task = new SMSTask();

                task.SendScheduleSMSTask("15910793195", EducatorModel.GenericContextMessage(EducatorModel_, StudentModel_.CustomerName, this.Model.CustomerVisit.VisitTime, RemainType.Message), this.Model.CustomerVisit.RemindTime, "CustomerVisit");

            }

            

        }
    }
}