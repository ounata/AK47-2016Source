using MCS.Library.Data.Builder;
using PPTS.Contracts.Search.Models;
using PPTS.Data.Customers.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace PPTS.Services.Search.Executors
{
    public abstract class CustomerSearchExecutorBase
    {
        /// <summary>
        /// 更新类型
        /// </summary>
        public abstract CustomerSearchUpdateType SearchUpdateType
        {
            get;
        }

        protected abstract DataTable PrepareData(IList<string> customerIDs);

        public void Execute(IList<string> customerIDs)
        {
            DataTable dt = this.PrepareData(customerIDs);

            StringBuilder sql = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
               
                string id = row["CustomerID"].ToString().Trim();
                sql.AppendLine(string.Format("if exists({0})", this.ExistSql(id)));
                sql.AppendLine("begin");
                sql.AppendLine(string.Format("{0}", this.UpdateSql(id, row)));
                sql.AppendLine("end");
                sql.AppendLine("else");
                sql.AppendLine("begin");
                sql.AppendLine(string.Format("{0}", this.InsertSql(row)));
                sql.AppendLine("end");
            }
            CustomerSearchAdapter.Instance.GetSqlContext().AppendSqlInContext(TSqlBuilder.Instance, sql.ToString());
            CustomerSearchAdapter.Instance.GetDbContext().ExecuteNonQuerySqlInContext();
        }

        private string UpdateSql(string id, DataRow row)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CustomerID", id);
            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();
            updateBuilder.AppendItems(row);
            updateBuilder.AppendItem("ServiceModifyTime", "getutcdate()", "=", true);
            string sql = string.Format(@"update {0} set {1} where {2}"
           , CustomerSearchAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
           , updateBuilder.ToSqlString(TSqlBuilder.Instance)
           , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        private string InsertSql(DataRow row)
        {
            InsertSqlClauseBuilder insertBuilder = new InsertSqlClauseBuilder();
            insertBuilder.AppendItems(row);
            string sql = string.Format(@"insert into {0} {1}"
           , CustomerSearchAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
           , insertBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        private string ExistSql(string customerID)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CustomerID", customerID);
            string sql = string.Format(@"select top 1 1 from {0} where {1}"
            , CustomerSearchAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}