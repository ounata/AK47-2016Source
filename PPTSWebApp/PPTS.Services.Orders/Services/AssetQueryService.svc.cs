using MCS.Library.WcfExtensions;
using PPTS.Contracts.Orders.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PPTS.Contracts.Orders.Models;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Adapters;
using System.Data;
using MCS.Library.Data.Mapping;

namespace PPTS.Services.Orders.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class AssetQueryService : IAssetQueryService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public AssetStatisticQueryResult QueryAssetStatisticByAccountID(string accountID)
        {
            string sqlText = "select  sum(Amount*Price) AS AssetMoney,sum(AssignedAmount) as AssignedAmount,sum(ConfirmedAmount) as ConfirmedAmount from Assets_Current where AccountID='{0}' group by AccountID";
            sqlText = string.Format(sqlText, accountID);
            DataSet ds = DbHelper.RunSqlReturnDS(sqlText, AssetAdapter.Instance.GetDbContext().Name);
            
            AssetStatisticQueryResult result = new AssetStatisticQueryResult();
            if (ds.Tables[0].Rows.Count > 0)
                result = ORMapping.DataRowToObject<AssetStatisticQueryResult>(ds.Tables[0].Rows[0], new AssetStatisticQueryResult());
            return result;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public AssetStatisticQueryResult QueryAssetStatisticByCustomerID(string customerID)
        {
            string sqlText = "select  sum(Amount*Price) AS AssetMoney,sum(AssignedAmount) as AssignedAmount,sum(ConfirmedAmount) as ConfirmedAmount from Assets_Current where CustomerID='{0}' group by customerID";
            sqlText = string.Format(sqlText, customerID);
            DataSet ds = DbHelper.RunSqlReturnDS(sqlText, AssetAdapter.Instance.GetDbContext().Name);

            AssetStatisticQueryResult result = new AssetStatisticQueryResult();
            if (ds.Tables[0].Rows.Count > 0)
                result = ORMapping.DataRowToObject<AssetStatisticQueryResult>(ds.Tables[0].Rows[0], new AssetStatisticQueryResult());
            return result;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public RefundConsumptionValueQueryResult QueryConsumptionValue(RefundConsumptionValueQueryCriteriaModel criteria)
        {
            //model.NullCheck("model");
            //model.AccountID.NullCheck("model.AccountID");
            //(model.LastChargePayDate > DateTime.MinValue).FalseThrow("model.LastChargePayDate");
            //OrderInfoForRefundQueryResult result = new OrderInfoForRefundQueryResult();
            //decimal consumptionValue = 0;
            //decimal reallowanceMoney = 0;
            //DateTime startTime= model.LastChargePayDate;
            //if (model.LastChargePayDate < model.LastestRefundVerifyDate)//上次充值时间早于退费时间则使用退费时间
            //    startTime = model.LastestRefundVerifyDate;
            //else//上次充值时间晚于退费时间则检查是否有消耗的课时，如果没有则使用上上次的充值时间
            //{
            //    if (!AssignsAdapter.Instance.ExistAccountConfirmAssignByDateTime(model.AccountID, startTime))
            //    {
            //        startTime = model.LastestChargePayDate;
            //    }
            //}
            //result.AccountID = model.AccountID;
            //result.StartTime = startTime;
            //result.AssetMoney = AssetAdapter.Instance.LoadAssetsValueByAccountID(model.AccountID);
            //result.ConsumptionValue = 0;
            //result.ReallowanceMoney = 0;
            //AssignsAdapter.Instance.LoadAccountRefundInfoByDateTime(model.AccountID, model.NewDiscountRate, startTime,
            //    ref consumptionValue, ref reallowanceMoney);
            //result.ConsumptionValue = consumptionValue;
            //result.ReallowanceMoney = reallowanceMoney;
            //return result;

            return null;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public RefundReallowanceMoneyQueryResult QueryReallowanceMoney(RefundReallowanceMoneyQueryCriteriaModel criteria)
        {
            //model.NullCheck("model");
            //model.AccountID.NullCheck("model.AccountID");
            //(model.LastChargePayDate > DateTime.MinValue).FalseThrow("model.LastChargePayDate");
            //OrderInfoForRefundQueryResult result = new OrderInfoForRefundQueryResult();
            //decimal consumptionValue = 0;
            //decimal reallowanceMoney = 0;
            //DateTime startTime= model.LastChargePayDate;
            //if (model.LastChargePayDate < model.LastestRefundVerifyDate)//上次充值时间早于退费时间则使用退费时间
            //    startTime = model.LastestRefundVerifyDate;
            //else//上次充值时间晚于退费时间则检查是否有消耗的课时，如果没有则使用上上次的充值时间
            //{
            //    if (!AssignsAdapter.Instance.ExistAccountConfirmAssignByDateTime(model.AccountID, startTime))
            //    {
            //        startTime = model.LastestChargePayDate;
            //    }
            //}
            //result.AccountID = model.AccountID;
            //result.StartTime = startTime;
            //result.AssetMoney = AssetAdapter.Instance.LoadAssetsValueByAccountID(model.AccountID);
            //result.ConsumptionValue = 0;
            //result.ReallowanceMoney = 0;
            //AssignsAdapter.Instance.LoadAccountRefundInfoByDateTime(model.AccountID, model.NewDiscountRate, startTime,
            //    ref consumptionValue, ref reallowanceMoney);
            //result.ConsumptionValue = consumptionValue;
            //result.ReallowanceMoney = reallowanceMoney;

            //return result;

            return null;
        }
    }
}
