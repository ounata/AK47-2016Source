using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.Service;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using PPTS.WebAPI.Customers.ViewModels.Students;
using System;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑反馈")]
    public class EditableStudentVisitExecutor : PPTSEditCustomerExecutorBase<EditableCustomerVisitModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public EditableStudentVisitExecutor(EditableCustomerVisitModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.CustomerVisit.NullCheck("CustomerVisit");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            //EditableCustomerVisitModel tempModel = new EditableCustomerVisitModel();
            //tempModel = EditableCustomerVisitModel.Load(this.Model.CustomerVisit.VisitID);

            CheckTimeResult CheckTimeResult_= Model.CheckTime();
            if (!CheckTimeResult_.Sucess)
            {
                throw new Exception(CheckTimeResult_.Message);
            }

            ///编辑人信息
            this.Model.CustomerVisit.FillModifier();

            CustomerVisitAdapter.Instance.UpdateInContext(this.Model.CustomerVisit);

            ///得到学管师相关信息
            EducatorModel EducatorModel_ = new EducatorModel(this.Model.CustomerVisit.CustomerID);

            StudentModel StudentModel_ = CreatableVisitBatchModel.GetStudentByID(this.Model.CustomerVisit.CustomerID);

            ///发邮件
            if (this.Model.SelectType == (int)RemainType.Email)
            {
                EmailMessage message = new EmailMessage("lihui_10@xueda.com", "预约回访提醒", EducatorModel.GenericContextMessage(EducatorModel_, StudentModel_.CustomerName, this.Model.CustomerVisit.VisitTime, RemainType.Email));

                EmailMessageAdapter.Instance.Insert(message);
            }
            ///发短信
            if (this.Model.SelectType == (int)RemainType.Message)
            {
                SMSTask task = new SMSTask();

                task.SendScheduleSMSTask("15910793195", EducatorModel.GenericContextMessage(EducatorModel_, StudentModel_.CustomerName, this.Model.CustomerVisit.VisitTime, RemainType.Message), this.Model.CustomerVisit.RemindTime, "CustomerVisit");

            }
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);

            context.Logs.ForEach(log => log.ResourceID = this.Model.CustomerVisit.VisitID);
        }
    }
}