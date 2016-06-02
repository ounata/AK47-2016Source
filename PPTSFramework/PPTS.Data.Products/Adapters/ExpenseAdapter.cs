using MCS.Library.Data;
using MCS.Library.Data.Builder;
using PPTS.Data.Common.Security;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Adapters
{
    public class ExpenseAdapter : ProductAdapterBase<Expense, ExpenseCollection>
    {
        public static ExpenseAdapter Instance = new ExpenseAdapter();

        /// <summary>
        /// 通过校区获得服务费列表信息
        /// </summary>
        /// <param name="CampusID">校区ID</param>
        /// <returns></returns>
        public ExpenseCollection LoadByCampusID(string CampusID)
        {
            ExpenseCollection dc = this.QueryData(PrepareLoadExpenseSqlByPermission(CampusID));
            return dc;
        }

        /// <summary>
        /// 通过校区获得服务费信息
        /// </summary>
        /// <param name="CampusID">校区ID</param>
        /// <returns>拼装SQL</returns>
        private string PrepareLoadExpenseSqlByPermission(string CampusID)
        {
            WhereSqlClauseBuilder expense_builder = new WhereSqlClauseBuilder();
            WhereSqlClauseBuilder expensepermission_builder = new WhereSqlClauseBuilder();
            expensepermission_builder.AppendItem("CampusID", CampusID);
            OrderBySqlClauseBuilder orderbuilder = new OrderBySqlClauseBuilder();
            orderbuilder.AppendItem("CreateTime", FieldSortDirection.Descending);
            string sql = string.Format(@"select * from {0} where {1} ExpenseID in 
                                    (
	                                    select ExpenseID from {2}  where {3}
                                    ) 
                                    order by {4} "
            , this.GetQueryMappingInfo().GetQueryTableName()
            , expense_builder.ToSqlString(TSqlBuilder.Instance)
            , ExpensePermissionAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , expensepermission_builder.ToSqlString(TSqlBuilder.Instance)
            , orderbuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}
