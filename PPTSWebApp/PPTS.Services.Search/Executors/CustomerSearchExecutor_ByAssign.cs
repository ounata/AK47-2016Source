using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PPTS.Contracts.Search.Models;
using MCS.Library.Data.Builder;
using PPTS.Data.Common;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Data.Adapters;

namespace PPTS.Services.Search.Executors
{
    /// <summary>
    /// 排课
    /// </summary>
    public class CustomerSearchExecutor_ByAssign : CustomerSearchExecutorBase
    {
        public override CustomerSearchUpdateType SearchUpdateType
        {
            get
            {
                return CustomerSearchUpdateType.Assign;
            }
        }

        protected override DataTable PrepareData(IList<string> customerIDs)
        {
            string sql = this.SelectSql(customerIDs);
            DataSet ds = DbHelper.RunSqlReturnDS(sql, OrdersAdapter.Instance.GetDbContext().Name);
            return ds.Tables[0];
        }

        private string SelectSql(IList<string> customerIDs)
        {
            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("CustomerID");
            inBuilder.AppendItem(customerIDs.ToArray());
            SelectSqlClauseBuilder selectBuilder = new SelectSqlClauseBuilder();
            selectBuilder.AppendFields(
                  "CustomerID",
                  "SUM(Amount) as AssetOneToOneAmount",        ////剩余资产(1对1)数量
                  "SUM(Amount * Price) as AssetOneToOneMoney",  //剩余资产(1对1)金额

                  "SUM(AssignedAmount) as AssetOneToOneAssignedAmount",  //排定资产(1对1)数量
                  "SUM(AssignedAmount * Price) as AssetOneToOneAssignedMoney", //排定资产(1对1)价值

                  "SUM(ConfirmedAmount) as AssetOneToOneConfirmedAmount", //消耗资产(1对1)数量
                  "SUM(ConfirmedMoney) as AssetOneToOneConfirmedMoney"     //消耗资产(1对1)价值
                   );
            string sql = string.Format(@"select {0} from {1} where CategoryType='{2}' and {3} group by CustomerID"
            , selectBuilder.ToSqlString(TSqlBuilder.Instance)
            , AssetViewAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , (int)CategoryType.OneToOne
            , inBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}