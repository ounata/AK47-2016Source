using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.Net.SNTP;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.Service;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using PPTS.WebAPI.Customers.ViewModels.Students;
using System;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("批量增加回访")]
    public class AddCustomerVisitBatchExecutor : PPTSEditCustomerExecutorBase<CreatableVisitBatchModel>
    {
        /// <summary>
        /// 批量添加回访
        /// </summary>
        /// <param name="model"></param>
        public AddCustomerVisitBatchExecutor(CreatableVisitBatchModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.CustomerVisits.NullCheck("CustomerVisits");
        }

        private void InitData(CreatableVisitBatchModel model)
        {
            foreach (CustomerVisitModel item in model.CustomerVisits)
            {
                item.VisitID = UuidHelper.NewUuidString();
                item.CampusID = this.Model.CampusID;
                item.CampusName = this.Model.CampusName;

                ///回访人相关
                item.VisitorID = this.Model.CurrID;
                item.VisitorJobID = this.Model.CurrJobID;
                item.VisitorName = this.Model.CurrName;
                item.VisitorJobName = this.Model.CurrJobName;

                ///创建人相关
                item.FillCreator();

                ///编辑人相关
                item.FillModifier();

            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            InitData(Model);
            bool currFlg = true;
            string errorStr = "学员：";
            foreach (CustomerVisitModel item in Model.CustomerVisits)
            {
                DateTime now = SNTPClient.AdjustedTime;
                DateTime tempStart = now.Date.AddDays(-3);
                DateTime tempEnd = now.Date.AddDays(1);                
                if (tempStart > item.VisitTime || item.VisitTime > tempEnd)
                {
                    StudentModel Customer = new StudentModel();
                    Customer = CreatableVisitBatchModel.GetStudentByID(item.CustomerID);
                    errorStr = errorStr + Customer.CustomerName + "、";

                    currFlg = false;
                    continue;
                }
                CustomerVisitAdapter.Instance.UpdateInContext(item);

                ///得到学管师相关信息
                EducatorModel EducatorModel_ = new EducatorModel(item.CustomerID);

                StudentModel StudentModel_ = CreatableVisitBatchModel.GetStudentByID(item.CustomerID);

                ///发邮件
                if (item.SelectType == (int)RemainType.Email)
                {
                    EmailMessage message = new EmailMessage("lihui_10@xueda.com", "预约回访提醒", EducatorModel.GenericContextMessage(EducatorModel_, StudentModel_.CustomerName, item.VisitTime, RemainType.Email));

                    EmailMessageAdapter.Instance.Insert(message);
                }
                ///发短信
                if (item.SelectType == (int)RemainType.Message)
                {
                    SMSTask task = new SMSTask();

                    task.SendScheduleSMSTask("15910793195", EducatorModel.GenericContextMessage(EducatorModel_, StudentModel_.CustomerName, item.VisitTime, RemainType.Message), this.Model.CustomerVisit.RemindTime, "CustomerVisit");

                }

            }
            if (currFlg == false)
            {
                errorStr = errorStr.Substring(0, errorStr.Length - 1);
                errorStr = errorStr + "回访时间有误，只允许选择当前时间及三天前";
                throw new Exception(errorStr);
            }
        }
    }
}