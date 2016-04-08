﻿using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Workflow
{
    public class WfProcessCurrentAssigneeAdapter : UpdatableAndLoadableAdapterBase<WfProcessCurrentAssignee, WfProcessCurrentAssigneeCollection>
    {
        public static readonly WfProcessCurrentAssigneeAdapter Instance = new WfProcessCurrentAssigneeAdapter();

        private WfProcessCurrentAssigneeAdapter()
        {
        }

        public WfProcessCurrentAssigneeCollection Load(string processID)
        {
            return LoadByInBuilder(new InLoadingCondition(
                b => b.AppendItem(processID),
                "PROCESS_ID")
            );
        }

        public void Update(string processID, WfProcessCurrentAssigneeCollection assignees)
        {
            processID.CheckStringIsNullOrEmpty("processID");

            WhereSqlClauseBuilder wBuilder = new WhereSqlClauseBuilder();

            wBuilder.AppendItem("PROCESS_ID", processID);
            wBuilder.AppendTenantCodeSqlClause(typeof(WfProcessCurrentActivity));

            string sqlDelete = string.Format(
                "DELETE WF.PROCESS_CURRENT_ASSIGNEES WHERE {0}",
                wBuilder.ToSqlString(TSqlBuilder.Instance));

            StringBuilder strB = new StringBuilder();

            foreach (WfProcessCurrentAssignee assignee in assignees)
            {
                if (strB.Length > 0)
                    strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                strB.Append(ORMapping.GetInsertSql(assignee, TSqlBuilder.Instance));
            }

            string sql = sqlDelete;

            if (strB.Length > 0)
                sql += TSqlBuilder.Instance.DBStatementSeperator + strB.ToString();

            DbHelper.RunSqlWithTransaction(sql, GetConnectionName());
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }
    }
}
