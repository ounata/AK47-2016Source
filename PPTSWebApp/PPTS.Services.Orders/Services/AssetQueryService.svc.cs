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
using PPTS.Data.Orders;
using MCS.Library.Core;
using PPTS.Data.Common;

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
            string sqlText = "select  sum(Amount*Price) AS AssetMoney,sum(AssignedAmount) as AssignedAmount,sum(ConfirmedAmount) as ConfirmedAmount from OM.Assets_Current where AccountID='{0}' group by AccountID";
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
            string sqlText = "select  sum(Amount*Price) AS AssetMoney,sum(AssignedAmount) as AssignedAmount,sum(ConfirmedAmount) as ConfirmedAmount from OM.Assets_Current where CustomerID='{0}' group by customerID";
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
            List<DateTime> dates = new List<DateTime>();
            if (CommonHelper.IsValidDbDate(criteria.LastChargeDate))
                dates.Add(criteria.LastChargeDate);
            if (CommonHelper.IsValidDbDate(criteria.LastestChargeDate))
                dates.Add(criteria.LastestChargeDate);
            if (CommonHelper.IsValidDbDate(criteria.LastestRefundDate))
                dates.Add(criteria.LastestRefundDate);

            dates = dates.OrderByDescending(x => x.Date).ToList();

            foreach (DateTime startDate in dates)
            {
                string format = @"select isnull(sum(isnull(x.ConfirmPrice,0)*isnull(x.Amount,0)),0) AssignValue from OM.Assigns  as x 
                            inner join OM.Assets_Current AS y on x.AssetID = y.AssetID
                            inner join OM.OrderItems z on y.AssetRefID = z.ItemID
                            where x.AssignStatus='{0}' and x.StartTime>='{1}' and z.DiscountType in ('{2}','{3}','{4}')";

                string sqlText = string.Format(format, (int)AssignStatusDefine.Finished
                    , TimeZoneContext.Current.ConvertTimeToUtc(startDate).ToString("yyyy-MM-dd")
                    , (int)DiscountTypeDefine.Tunland, (int)DiscountTypeDefine.Special, (int)DiscountTypeDefine.Other);

                object obj = DbHelper.RunSqlReturnScalar(sqlText, AssetAdapter.Instance.GetDbContext().Name);
                decimal v = Convert.ToDecimal(obj);
                if (v > 0)
                {
                    return new RefundConsumptionValueQueryResult()
                    {
                        AccountID = criteria.AccountID,
                        ConsumptionValue = v,
                        ReallowanceStartTime = startDate
                    };
                }
            }
            return new RefundConsumptionValueQueryResult()
            {
                AccountID = criteria.AccountID,
                ConsumptionValue = 0,
                ReallowanceStartTime = DateTime.MinValue
            };
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public RefundReallowanceMoneyQueryResult QueryReallowanceMoney(RefundReallowanceMoneyQueryCriteriaModel criteria)
        {
            if (!CommonHelper.IsValidDbDate(criteria.ReallowanceStartTime))
            {
                return new RefundReallowanceMoneyQueryResult()
                {
                    AccountID = criteria.AccountID,
                    ReallowanceMoney = 0
                };
            }
            string format = @"select isnull(sum(isnull(x.ConfirmPrice,0)*({5}-isnull(z.DiscountRate,0))*isnull(x.Amount,0)),0) AssignValue from OM.Assigns  as x 
                            inner join OM.Assets_Current AS y on x.AssetID = y.AssetID
                            inner join OM.OrderItems z on y.AssetRefID = z.ItemID
                            where x.AssignStatus='{0}' and x.StartTime>='{1}' and z.DiscountType in ('{2}','{3}','{4}')";

            string sqlText = string.Format(format, (int)AssignStatusDefine.Finished
                , TimeZoneContext.Current.ConvertTimeToUtc(criteria.ReallowanceStartTime).ToString("yyyy-MM-dd")
                , (int)DiscountTypeDefine.Tunland, (int)DiscountTypeDefine.Special, (int)DiscountTypeDefine.Other
                , criteria.RefundDiscountRate);

            object obj = DbHelper.RunSqlReturnScalar(sqlText, AssetAdapter.Instance.GetDbContext().Name);
            decimal v = Convert.ToDecimal(obj);
            return new RefundReallowanceMoneyQueryResult()
            {
                AccountID = criteria.AccountID,
                ReallowanceMoney = v
            };
        }
    }
}
