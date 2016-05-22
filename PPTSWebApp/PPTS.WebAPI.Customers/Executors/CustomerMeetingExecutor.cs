﻿using MCS.Library.Data.Executors;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.ViewModels.CustomerMeetings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects;
using PPTS.WebAPI.Customers.ViewModels.Students;
using PPTS.Data.Customers.Entities;
using MCS.Library.Core;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Executors;
using PPTS.Data.Common;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("学情服务会Executor")]
    public class CustomerMeetingExecutor : PPTSEditCustomerExecutorBase<EditCustomerMeetingModel>
    {
        public CustomerMeetingExecutor(EditCustomerMeetingModel model) : base(model, null)
        {
            model.CustomerMeeting.NullCheck("model");

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
            if (string.IsNullOrEmpty(Model.CustomerMeeting.MeetingID))
            {
                Model.CustomerMeeting.MeetingID = UuidHelper.NewUuidString();
            }
            Model.CustomerMeeting.CampusID = customer.CampusID;
            Model.CustomerMeeting.CampusName = customer.CampusName;
            //修改当前操作人信息
            Model.CustomerMeeting.FillCreator();

            CustomerMeetingAdapter.Instance.UpdateInContext(this.Model.CustomerMeeting);
            CustomerMeetingItemAdapter.Instance.Delete(x => x.AppendItem("MeetingID", Model.CustomerMeeting.MeetingID));
            foreach (var item in this.Model.Items)
            {
                item.MeetingID = Model.CustomerMeeting.MeetingID;
                CustomerMeetingItemAdapter.Instance.UpdateInContext(item);
            }
        }
    }
}