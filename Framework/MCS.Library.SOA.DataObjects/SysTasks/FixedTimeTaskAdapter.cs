using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Configuration;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MCS.Library.SOA.DataObjects
{
    public class FixedTimeTaskAdapter : UpdatableAndLoadableAdapterBase<FixedTimeTask, FixedTimeTaskCollection>
    {
        public static readonly FixedTimeTaskAdapter Instance = new FixedTimeTaskAdapter();

        public FixedTimeTask Load(string taskID)
        {
            taskID.CheckStringIsNullOrEmpty("taskID");

            return Load(builder => builder.AppendItem("TASK_GUID", taskID)).FirstOrDefault();
        }

        /// <summary>
        /// 按照ResourceID进行加载
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public FixedTimeTaskCollection LoadByResourceID(string resourceID)
        {
            resourceID.CheckStringIsNullOrEmpty("resourceID");

            return Load(builder => builder.AppendItem("RESOURCE_ID", resourceID));
        }

        /// <summary>
        /// 查找在当前时间误差范围内的固定时间任务
        /// </summary>
        /// <param name="timeScope"></param>
        /// <param name="batchCount"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public FixedTimeTaskCollection FetchInTimeScopeTasks(TimeSpan timeScope, int batchCount, Action<FixedTimeTask> action)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                FixedTimeTaskCollection result = InnerQueryBatch(timeScope, batchCount);

                if (action != null)
                    result.ForEach(t => action(t));

                scope.Complete();

                return result;
            }
        }

        public void DeleteExpiredTasks()
        {
            this.DeleteExpiredTasks(FixedTimeTaskSettings.GetConfig().ExpireTime);
        }

        public void DeleteExpiredTasks(TimeSpan expiredTime)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("START_TIME", string.Format("DATEADD(second, -{0}, GETUTCDATE())", (int)expiredTime.TotalSeconds), "<", true);
            builder.AppendTenantCode();

            string sql = string.Format("DELETE {0} WHERE {1}",
                this.GetTableName(),
                builder.ToSqlString(TSqlBuilder.Instance));

            DbHelper.RunSql(sql, this.GetConnectionName());
        }

        private FixedTimeTaskCollection InnerQueryBatch(TimeSpan timeScope, int batchCount)
        {
            string batchClause = batchCount >= 0 ? "TOP " + batchCount : string.Empty;

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("START_TIME", string.Format("DATEADD(second, -{0}, GETUTCDATE())", (int)timeScope.TotalSeconds), ">=", true);
            builder.AppendItem("START_TIME", string.Format("DATEADD(second, {0}, GETUTCDATE())", (int)timeScope.TotalSeconds), "<", true);
            builder.AppendTenantCode();

            string sql = string.Format("SELECT {0} * FROM {1} WITH(UPDLOCK, READPAST) WHERE {2} ORDER BY SORT_ID ASC",
                batchClause,
                this.GetTableName(),
                builder.ToSqlString(TSqlBuilder.Instance));

            return this.QueryData(sql);
        }

        protected override string GetConnectionName()
        {
            return WorkflowSettings.GetConfig().ConnectionName;
        }
    }
}
