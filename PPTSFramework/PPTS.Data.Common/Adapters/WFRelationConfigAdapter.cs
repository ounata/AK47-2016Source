using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Adapters
{
    public class WFRelationConfigAdapter : UpdatableAndLoadableAdapterBase<WFRelationConfig, WFRelationConfigCollection>
    {
        public static readonly WFRelationConfigAdapter Instance = new WFRelationConfigAdapter();

        public WFRelationConfig LoadWFRelationConfig(string applicationName, string programName, IOrganization org, string jobName)
        {
            applicationName.NullCheck("applicationName");
            jobName.NullCheck("jobName");
            org.NullCheck("org");

            return this.QueryData(LoadWFRelationConfigSQL(applicationName, programName
                , org.GetAllDataScopeParents().ToList(), jobName)).FirstOrDefault();
        }

        public WFRelationConfig LoadWFRelationConfig(string applicationName, IOrganization org, string jobName)
        {
            applicationName.NullCheck("applicationName");
            jobName.NullCheck("jobName");
            org.NullCheck("org");

            return this.QueryData(LoadWFRelationConfigSQL(applicationName, null
                , org.GetAllDataScopeParents().ToList(), jobName)).FirstOrDefault();
        }

        public WFRelationConfig CheckAndLoadWFRelationConfig(string applicationName, IOrganization org, string jobName)
        {
            return CheckAndLoadWFRelationConfig(applicationName, null, org, jobName);
        }

        public WFRelationConfig CheckAndLoadWFRelationConfig(string applicationName, string programName, IOrganization org, string jobName)
        {
            WFRelationConfig result = LoadWFRelationConfig(applicationName, programName, org, jobName);

            (result != null).FalseThrow("不能根据{0}、{1}、{2}、{3}找到对应的流程",
                applicationName, programName, org.Name, jobName);

            return result;
        }

        private string LoadWFRelationConfigSQL(string applicationName, string programName, List<IOrganization> orgs, string jobName)
        {
            string sql = null;
            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            OrderBySqlClauseBuilder orderSqlClause = new OrderBySqlClauseBuilder();
            //查询
            string orderColumn = "(case {0} else 6 end) OrderInfo";
            StringBuilder whenSqlClause = new StringBuilder();
            string template = " when [OrgType]={0} then {1} ";
            List<string> orgIDs = new List<string>();
            orgIDs = orgs.Select((model) => { return model.ID; }).ToList();
            whenSqlClause.Append(string.Format(template, (int)DepartmentType.Campus, 1));
            whenSqlClause.Append(string.Format(template, (int)DepartmentType.Branch, 2));
            whenSqlClause.Append(string.Format(template, (int)DepartmentType.Region, 3));
            orderColumn = string.Format(orderColumn, whenSqlClause.ToString());
            //筛选
            sqlClause.Add(new WhereSqlClauseBuilder()
                    .AppendItem("ApplicationName", applicationName))
                .Add(new ConnectiveSqlClauseCollection(LogicOperatorDefine.Or)
                    .Add(new InSqlClauseBuilder("OrgID").AppendItem(orgIDs.ToArray()))
                    .Add(new WhereSqlClauseBuilder().AppendItem("OrgID", (string)null, "IS"))
                    )
                .Add(new WhereSqlClauseBuilder(LogicOperatorDefine.Or)
                    .AppendItem("JobName", jobName)
                    .AppendItem("JobName", (string)null, "IS"))
            .Add(new WhereSqlClauseBuilder().AppendItem("StartTime", SNTPClient.AdjustedUtcTime, "<="))
            .Add(new WhereSqlClauseBuilder().AppendItem("EndTime", SNTPClient.AdjustedUtcTime, ">"));
            programName.IsNotEmpty(value => sqlClause.Add(new WhereSqlClauseBuilder().AppendItem("ProgramName", value)));
            //排序
            orderSqlClause.AppendItem("OrderInfo", MCS.Library.Data.FieldSortDirection.Ascending)
             .AppendItem("JobName", MCS.Library.Data.FieldSortDirection.Descending);
            sql = string.Format("select top 1 *,{3} from {0} where {1} order by {2}"
               , this.GetMappingInfo().GetQueryTableName()
               , sqlClause.ToSqlString(TSqlBuilder.Instance)
               , orderSqlClause.ToSqlString(TSqlBuilder.Instance)
               , orderColumn);
            return sql;
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }
    }
}