using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects;
using MCS.Web.MVC.Library.Models.UserTasks;
using System.Collections.Generic;

namespace MCS.Web.MVC.Library.DataSources
{
    public class UserAccomplishedTaskDataSource : ObjectDataSourceQueryAdapterBase<UserAccomplishedTaskModel, UserAccomplishedTaskModelCollection>
    {
        public static readonly UserAccomplishedTaskDataSource Instance = new UserAccomplishedTaskDataSource();

        private UserAccomplishedTaskDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.SearchConnectionName;
        }

        public PagedQueryResult<UserAccomplishedTaskModel, UserAccomplishedTaskModelCollection> LoadUserTasks(IPageRequestParams prp, UserTaskQueryCriteria condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            ConnectiveSqlClauseCollection sqlCollection = new ConnectiveSqlClauseCollection();
            sqlCollection.Add(ConditionMapping.GetConnectiveClauseBuilder(condition));

            string sqlBuilder = sqlCollection.ToSqlString(TSqlBuilder.Instance);
            var result = Query(prp, sqlBuilder, orderByBuilder);

            return result;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @"UT.[PROCESS_ID], UT.[TASK_GUID], UT.[APPLICATION_NAME],UT.[DRAFT_USER_ID],UT.[DRAFT_USER_NAME],UT.[PROGRAM_NAME], UT.[TASK_TITLE], UT.[URL], UT.[EMERGENCY], UT.[PURPOSE], UT.[SOURCE_NAME], UT.[READ_TIME], UT.[RESOURCE_ID], UT.[COMPLETED_TIME], UT.[DRAFT_DEPARTMENT_NAME],UT.[STATUS],UT.[SEND_TO_USER],PN.[OWNER_ACTIVITY_ID], P.PARAM_VALUE AS PROJECT_NAME";
            qc.OrderByClause = GetOrderByString(qc.OrderByClause);
            qc.FromClause = @"WF.PROCESS_INSTANCES(NOLOCK) PN RIGHT JOIN WF.USER_ACCOMPLISHED_TASK (NOLOCK) UT ON UT.[PROCESS_ID] = PN.[INSTANCE_ID] LEFT JOIN WF.PROCESS_RELATIVE_PARAMS(NOLOCK) P ON UT.PROCESS_ID = P.PROCESS_ID AND P.PARAM_KEY = 'ProjectName'";

            base.OnBuildQueryCondition(qc);
        }

        protected string GetOrderByString(string inputOrderBy)
        {
            if (string.IsNullOrEmpty(inputOrderBy))
                inputOrderBy = " UT.COMPLETED_TIME DESC";
            else
                inputOrderBy = "UT." + inputOrderBy;

            return inputOrderBy;
        }
    }
}