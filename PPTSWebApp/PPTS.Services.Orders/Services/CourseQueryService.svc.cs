using PPTS.Contracts.Orders.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Contracts.Orders.Models;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.WcfExtensions;
using System.ServiceModel.Web;

namespace PPTS.Services.Orders.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“CourseQueryService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 CourseQueryService.svc 或 CourseQueryService.svc.cs，然后开始调试。
    public class CourseQueryService : ICourseQueryService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public HasPeriodCourseQueryResult QueryPeriodCourseByCustomerID(string customerID, DateTime dateTime)
        {

            HasPeriodCourseQueryResult result = new HasPeriodCourseQueryResult();
            result.HasPeriodInside = AssignsAdapter.Instance.Exists(builder => builder.AppendItem("CustomerID", customerID)
             .AppendItem("AssignStatus", AssignStatusDefine.Finished.GetHashCode()).AppendItem("StartTime", TimeZoneContext.Current.ConvertTimeToUtc(dateTime), "<="));
            result.HasPeriodOutside = AssignsAdapter.Instance.Exists(builder => builder.AppendItem("CustomerID", customerID)
             .AppendItem("AssignStatus", AssignStatusDefine.Finished.GetHashCode()).AppendItem("StartTime", TimeZoneContext.Current.ConvertTimeToUtc(dateTime), ">"));
            return result;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public OrderInfoForRefundQueryResult QueryOrderInfoForRefundByAccountID(OrderInfoForRefundQueryModel model)
        {
            model.NullCheck("model");
            model.AccountID.NullCheck("model.AccountID");
            (model.LastChargePayDate > DateTime.MinValue).FalseThrow("model.LastChargePayDate");
            OrderInfoForRefundQueryResult result = new OrderInfoForRefundQueryResult();
            decimal consumptionValue = 0;
            decimal reallowanceMoney = 0;
            DateTime startTime= model.LastChargePayDate;
            if (model.LastChargePayDate < model.LastestRefundVerifyDate)//上次充值时间早于退费时间则使用退费时间
                startTime = model.LastestRefundVerifyDate;
            else//上次充值时间晚于退费时间则检查是否有消耗的课时，如果没有则使用上上次的充值时间
            {
                if (!AssignsAdapter.Instance.ExistAccountConfirmAssignByDateTime(model.AccountID, startTime))
                {
                    startTime = model.LastestChargePayDate;
                }
            }
            result.AccountID = model.AccountID;
            result.StartTime = startTime;
            result.AssetMoney = AssetAdapter.Instance.LoadAssetsValueByAccountID(model.AccountID);
            result.ConsumptionValue = 0;
            result.ReallowanceMoney = 0;
            AssignsAdapter.Instance.LoadAccountRefundInfoByDateTime(model.AccountID, model.NewDiscountRate, startTime,
                ref consumptionValue, ref reallowanceMoney);
            result.ConsumptionValue = consumptionValue;
            result.ReallowanceMoney = reallowanceMoney;
            
            return result;
        }
    }
}
