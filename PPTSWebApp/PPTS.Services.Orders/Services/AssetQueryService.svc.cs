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
            return null;
            //AssetMoneyQueryResult result = new AssetMoneyQueryResult();
            //string[] accountIDs = new string[] { accountID };
            //DataSet ds = DbHelper.RunSqlReturnDS(GetAssetMoneyCollectionByAccountIDsSQL(accountIDs), AssetAdapter.Instance.GetDbContext().Name);
            //if (ds.Tables[0].Rows.Count > 0)
            //    result = ORMapping.DataRowToObject<AssetMoneyQueryResult>(ds.Tables[0].Rows[0], new AssetMoneyQueryResult());
            //return result;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public AssetStatisticQueryResult QueryAssetStatisticByCustomerID(string customerID)
        {
            return null;
            //HasPeriodCourseQueryResult result = new HasPeriodCourseQueryResult();
            //result.HasPeriod = AssignsAdapter.Instance.Exists(builder => builder
            // .AppendItem("CustomerID", customerID)
            // .AppendItem("AssignStatus", (int)AssignStatusDefine.Finished));
            //return result;
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

        private string GetAssetMoneyCollectionByAccountIDsSQL(string[] accountIDs)
        {
            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder();
            inBuilder.AppendItem(accountIDs).DataField = "AccountID";
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("Amount", 0, ">");
            ConnectiveSqlClauseCollection connectiveBuilder = new ConnectiveSqlClauseCollection();
            connectiveBuilder.Add(inBuilder).Add(whereBuilder).LogicOperator = LogicOperatorDefine.And;
            string sql = string.Format(@"select AccountID,isnull(sum(isnull(Price,0)*isnull(Amount,0)),0) AssetMoney from {0} where {1} group by AccountID"
            , AssetAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , connectiveBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}
