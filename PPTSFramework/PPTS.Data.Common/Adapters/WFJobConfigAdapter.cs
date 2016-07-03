using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Net.SNTP;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Adapters
{
    public class WFJobConfigAdapter : UpdatableAndLoadableAdapterBase<WFJobConfig, WFJobConfigCollection>
    {
        public static readonly WFJobConfigAdapter Instance = new WFJobConfigAdapter();

        public WFJobConfig LoadJobConfig(string processKey, string orgID, string jobName)
        {
            WFJobConfig result = null;
            result = this.QueryData(LoadJobConfigSQL(processKey, orgID, jobName)).FirstOrDefault();
            return result;
        }

        private string LoadJobConfigSQL(string processKey, string orgID, string jobName)
        {
            string sql = null;
            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            OrderBySqlClauseBuilder orderSqlClause = new OrderBySqlClauseBuilder();
            //筛选
            sqlClause.Add(new WhereSqlClauseBuilder(LogicOperatorDefine.Or)
                .AppendItem("ProcessKey", processKey)
                .AppendItem("ProcessKey", (string)null, "is"))
           .Add(new WhereSqlClauseBuilder(LogicOperatorDefine.Or)
                .AppendItem("OrgID", orgID)
                .AppendItem("OrgID", (string)null, "is"))
           .Add(new WhereSqlClauseBuilder(LogicOperatorDefine.Or)
                .AppendItem("JobName", jobName)
                .AppendItem("JobName", (string)null, "is"))
            .Add(new WhereSqlClauseBuilder().AppendItem("StartTime", SNTPClient.AdjustedUtcTime, "<="))
            .Add(new WhereSqlClauseBuilder().AppendItem("EndTime", SNTPClient.AdjustedUtcTime, ">"));
            //排序
            orderSqlClause.AppendItem("ProcessKey", MCS.Library.Data.FieldSortDirection.Descending)
              .AppendItem("OrgID", MCS.Library.Data.FieldSortDirection.Descending)
              .AppendItem("JobName", MCS.Library.Data.FieldSortDirection.Descending);
            sql = string.Format("select top 1 * from {0} where {1} order by {2}"
                , this.GetMappingInfo().GetQueryTableName()
                , sqlClause.ToSqlString(TSqlBuilder.Instance)
                , orderSqlClause.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }
    }
}
