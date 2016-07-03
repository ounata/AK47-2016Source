using MCS.Library.Core;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Builder;

namespace PPTS.Data.Orders.Adapters
{
    public class FinancialAssignIncomeAdapter : OrderAdapterBase<FinancialAssignIncome,FinancialAssignIncomeCollection>
    {
        public static readonly FinancialAssignIncomeAdapter Instance = new FinancialAssignIncomeAdapter();

        public FinancialAssignIncomeCollection LoadCollectionByMonth(DateTime startDate, DateTime endDate, AssignStatusDefine assignStatus)
        {
            return this.QueryData(QueryAssignIncomeByMonthSQL(startDate, endDate, assignStatus));
        }

        private string QueryAssignIncomeByMonthSQL(DateTime startDate, DateTime endDate, AssignStatusDefine assignStatus)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("AssignStatus", Convert.ToInt32(assignStatus).ToString())
                .AppendItem("StartTime", "cast('"+ startDate.ToString() + "' as datetime)", ">=",true)
                .AppendItem("StartTime", "cast('"+ endDate.ToString() + "' as datetime)", "<",true);

            string sql = string.Format(@"select 
                    assign.CampusID,assign.CampusName,asset.CategoryType,asset.CategoryTypeName
                    ,asset.Catalog,asset.CatalogName,assign.ConfirmPrice,assign.Amount 
                    from {0} as assign 
                    inner join {1} as asset  
                    on assign.AssetID = asset.AssetID 
                    where {2};"
                    , AssignsAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                    , AssetAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                    , whereBuilder.ToSqlString(TSqlBuilder.Instance));

            return sql;
        }
    }
}
