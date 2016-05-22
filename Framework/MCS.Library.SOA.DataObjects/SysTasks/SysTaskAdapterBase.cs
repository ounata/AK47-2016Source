using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects
{
    public abstract class SysTaskAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection>
        where T : SysTaskBase
        where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public void Push(params T[] tasks)
        {
            if (tasks != null)
            {
                StringBuilder strB = new StringBuilder();

                foreach (T task in tasks)
                {
                    if (task != null)
                    {
                        if (strB.Length > 0)
                            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                        strB.Append(this.GetInsertSql(task, this.GetMappingInfo(), GetFixedContext(), StringExtension.EmptyStringArray));
                    }
                }

                if (strB.Length > 0)
                    DbHelper.RunSqlWithTransaction(strB.ToString(), this.GetConnectionName());
            }
        }

        public T Load(string taskID)
        {
            taskID.CheckStringIsNullOrEmpty("taskID");

            return Load(builder => builder.AppendItem("TASK_GUID", taskID)).FirstOrDefault();
        }

        /// <summary>
        /// 按照ResourceID进行加载
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public TCollection LoadByResourceID(string resourceID)
        {
            resourceID.CheckStringIsNullOrEmpty("resourceID");

            return Load(builder => builder.AppendItem("RESOURCE_ID", resourceID));
        }

        /// <summary>
        /// 更新任务的状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatus(string taskID, SysTaskStatus status)
        {
            taskID.CheckStringIsNullOrEmpty("taskID");

            ORMappingItemCollection mapping = GetMappingInfo(new Dictionary<string, object>());

            UpdateSqlClauseBuilder ub = new UpdateSqlClauseBuilder();
            ub.AppendItem("STATUS", status.ToString());
            ub.AppendItem("START_TIME", "GETUTCDATE()", "=", true);

            WhereSqlClauseBuilder wb = new WhereSqlClauseBuilder();
            wb.AppendItem("TASK_GUID", taskID);
            wb.AppendTenantCode(typeof(T));

            string sql = string.Format("UPDATE {0} SET {1} WHERE {2}",
                mapping.TableName,
                ub.ToSqlString(TSqlBuilder.Instance),
                wb.ToSqlString(TSqlBuilder.Instance));

            return DbHelper.RunSql(sql, this.GetConnectionName()) > 0;
        }

        protected override string GetConnectionName()
        {
            return WorkflowSettings.GetConfig().ConnectionName;
        }
    }
}
