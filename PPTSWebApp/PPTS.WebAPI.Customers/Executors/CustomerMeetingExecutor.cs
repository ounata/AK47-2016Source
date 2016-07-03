using MCS.Library.Data.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.ViewModels.CustomerMeetings;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Core;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Executors;
using PPTS.Data.Common;
using System;
using MCS.Web.MVC.Library.Models;
using MCS.Library.Net.SNTP;
using PPTS.Data.Customers.Entities;
using MCS.Library.Principal;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("学情服务会Executor")]
    public class CustomerMeetingExecutor : PPTSEditCustomerExecutorBase<EditCustomerMeetingModel>
    {
        public CustomerMeetingExecutor(EditCustomerMeetingModel model) : base(model, null)
        {
            model.CustomerMeeting.NullCheck("model");
            DateTime now = Convert.ToDateTime(SNTPClient.AdjustedLocalTime.ToShortDateString()+" 23:59");
            //编辑
            if (string.IsNullOrEmpty(Model.CustomerMeeting.ResourceId))
            {
                //会议起始时间点范围
                now =CustomerMeetingAdapter.Instance.Load(Model.CustomerMeeting.MeetingID).CreateTime;
                now= Convert.ToDateTime(now.ToShortDateString() + " 23:59");
            }
            DateTime beforeThreeDay = now.AddDays(-3);
            if (model.CustomerMeeting.MeetingTime < beforeThreeDay ||
                model.CustomerMeeting.MeetingTime > now ||
                model.CustomerMeeting.MeetingEndTime > now ||
                model.CustomerMeeting.MeetingEndTime < beforeThreeDay)
            {
                throw new ApplicationException("会议时间只能选择当前时间及前三天的任意一天!");
            }

            //会议起始时间限制在同一天
            bool isEquDay = false;
            if (model.CustomerMeeting.MeetingTime.ToShortDateString() == model.CustomerMeeting.MeetingEndTime.ToShortDateString())
            {
                isEquDay = true;
            }
            if (!isEquDay)
            {
                throw new ApplicationException("会议时间只能选择当前时间及前三天的任意一天!");
            }
            if (model.CustomerMeeting.NextMeetingTime < model.CustomerMeeting.MeetingTime || model.CustomerMeeting.NextMeetingTime < model.CustomerMeeting.MeetingEndTime)
            {
                throw new ApplicationException("下次开会时间不能小于本次会议时间!");
            }
            ////获取结账日期
            //ConfigArgs args = ConfigsCache.GetArgs(model.CustomerMeeting.CampusID);

            //DateTime dtAccount = args.GetCurrentClosingAccountDate();
            //if (model.CustomerMeeting.MeetingTime > dtAccount || model.CustomerMeeting.MeetingEndTime > dtAccount)
            //{
            //    throw new ApplicationException("会议日期不能超过结账日期!");
            //}
        }

        /// <summary>
        /// 新增会议准备数据
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            var customer = CustomerAdapter.Instance.Load(Model.CustomerMeeting.CustomerID);

            //新增
            if (!string.IsNullOrEmpty(Model.CustomerMeeting.ResourceId))
            {
                Model.CustomerMeeting.MeetingID = Model.CustomerMeeting.ResourceId;//UuidHelper.NewUuidString();
            }
            Model.CustomerMeeting.CampusID = customer.CampusID;
            Model.CustomerMeeting.CampusName = customer.CampusName;
            //修改当前操作人信息
            Model.CustomerMeeting.FillCreator();
            Model.CustomerMeeting.CreateTime = SNTPClient.AdjustedUtcTime;

            CustomerMeetingAdapter.Instance.UpdateInContext(this.Model.CustomerMeeting);
            CustomerMeetingItemAdapter.Instance.Delete(x => x.AppendItem("MeetingID", Model.CustomerMeeting.MeetingID));
            foreach (var item in this.Model.Items)
            {
                item.MeetingID = Model.CustomerMeeting.MeetingID;
                CustomerMeetingItemAdapter.Instance.UpdateInContext(item);
            }
        }
        /// <summary>
        /// 上传文件附件信息写入到DB
        /// </summary>
        /// <param name="context"></param>
        protected override void BeforeTransactionComplete(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.BeforeTransactionComplete(context);

            MaterialModelHelper helper = MaterialModelHelper.GetInstance(CustomerMeetingAdapter.Instance.ConnectionName);

            helper.Update(Model.CustomerMeeting.Materials);

        }
        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            #region 生成数据权限范围数据
            PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerMeeting>
               .GetInstance(PPTS.Data.Customers.ConnectionDefine.PPTSCustomerConnectionName)
               .UpdateAuthInContext(DeluxeIdentity.CurrentUser.GetCurrentJob()
               , DeluxeIdentity.CurrentUser.GetCurrentJob().Organization()
               , this.Model.CustomerMeeting.MeetingID
               , PPTS.Data.Common.Authorization.RelationType.Owner);
            #endregion 生成数据权限范围数据

            return base.DoOperation(context);
        }
    }
}