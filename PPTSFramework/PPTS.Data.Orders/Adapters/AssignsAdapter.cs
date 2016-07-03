using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MCS.Library.Data.Adapters;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Core;

using PPTS.Data.Orders.Entities;
using System.Data;

namespace PPTS.Data.Orders.Adapters
{
    public class AssignsAdapter : OrderAdapterBase<Assign, AssignCollection>
    {
        public static readonly AssignsAdapter Instance = new AssignsAdapter();
        private AssignsAdapter()
        { }

        public void UpdateCollectionInContext(AssignCollection collection)
        {
            collection.IsNotNull(c =>
            {
                c.ForEach(item => { UpdateInContext(item); });
            });
        }

        /// <summary>
        /// 更改排课状态
        /// </summary>
        /// <param name="assignID"></param>
        /// <param name="status"></param>
        public void UpdateAssignStatusInContext(IList<string> assignID, string operaterID, string operaterName, AssignStatusDefine status)
        {
            assignID.NullCheck("assignID");
            operaterID.CheckStringIsNullOrEmpty("operaterID");
            operaterName.CheckStringIsNullOrEmpty("operaterName");

            SqlContextItem sCI = this.GetSqlContext();

            UpdateSqlClauseBuilder uSCB = new UpdateSqlClauseBuilder();
            uSCB.AppendItem("AssignStatus", (int)status, "=");
            uSCB.AppendItem("ModifyTime", "GETUTCDATE()", "=", true);
            uSCB.AppendItem("ModifierID", operaterID, "=");
            uSCB.AppendItem("ModifierName", operaterName, "=");

            StringBuilder sb = new StringBuilder();
            foreach (var v in assignID)
            {
                sb.AppendFormat(",'{0}'", v);
            }
            if (sb.Length == 0)
                return;

            string assignIDs = string.Format("({0})", sb.ToString().Substring(1));
            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
            wSCB.AppendItem("AssignID", assignIDs, "in", true);
            //wSCB.AppendItem("AssignStatus", (int)AssignStatusDefine.Assigned, "=");

            sCI.AppendSqlInContext(TSqlBuilder.Instance, "update {0} set {1} where {2};"
                , this.GetTableName()
                , uSCB.ToSqlString(TSqlBuilder.Instance)
                , wSCB.ToSqlString(TSqlBuilder.Instance));
        }
        public void ConfirmAssignInContext(Assign assign)
        {
            assign.NullCheck("assign");

            SqlContextItem sCI = this.GetSqlContext();

            UpdateSqlClauseBuilder uSCB = new UpdateSqlClauseBuilder();
            uSCB.AppendItem("AssignStatus", (int)assign.AssignStatus, "=");
            uSCB.AppendItem("ModifyTime", "GETUTCDATE()", "=", true);
            uSCB.AppendItem("ModifierID", assign.ModifierID, "=");
            uSCB.AppendItem("ModifierName", assign.ModifierName, "=");

            uSCB.AppendItem("ConfirmPrice", assign.ConfirmPrice, "=");
            uSCB.AppendItem("ConfirmID", assign.ConfirmID, "=");
            uSCB.AppendItem("ConfirmStatus", (int)assign.ConfirmStatus, "=");
            uSCB.AppendItem("ConfirmTime", "GETUTCDATE()", "=", true);

            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
            wSCB.AppendItem("AssignID", assign.AssignID, "=");

            sCI.AppendSqlInContext(TSqlBuilder.Instance, "update {0} set {1} where {2};"
                , this.GetTableName()
                , uSCB.ToSqlString(TSqlBuilder.Instance)
                , wSCB.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 学员排课及教师排课冲突检查
        /// </summary>
        /// <param name="model"></param>
        public void CheckConflictAssignInContext(Assign model)
        {
            SqlContextItem sCI = this.GetSqlContext();
            UpdateSqlClauseBuilder uSCB = new UpdateSqlClauseBuilder();
            uSCB.AppendItem("@assignID", model.AssignID);
            uSCB.AppendItem("@customerID", model.CustomerID);
            uSCB.AppendItem("@teacherID", model.TeacherID);
            uSCB.AppendItem("@startTime", model.StartTime);
            uSCB.AppendItem("@endTime", model.EndTime);
            sCI.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, "exec OM.CheckConflictAssign {0}", uSCB.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 获取指定时间区间段及学员ID的排课记录
        /// 例如：
        /// 2016-04-25 至  2016-04-29
        /// atStart ：  2016-04-25
        /// atEnd   ：  2016-04-30
        /// </summary>
        /// <param name="atStart"></param>
        /// <param name="atEnd"></param>
        /// <param name="cID">Customer ID</param>
        /// <returns></returns>
        public AssignCollection LoadCollection(AssignTypeDefine atd, string ID, DateTime atStart, DateTime atEnd, bool isUTCTime)
        {
            atStart.NullCheck("atStart");
            atEnd.NullCheck("atEnd");
            ID.NullCheck("ID");

            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
            if (isUTCTime)
                wSCB.AppendItem("StartTime", atStart, ">=").AppendItem("EndTime", atEnd, "<");
            else
            {
                string atStartTxt = string.Empty, atEndTxt = string.Empty;
                atStartTxt = string.Format("DATEADD(hour, DATEDIFF(hour,GETDATE(),GETUTCDATE()), '{0}')", atStart.ToString("yyyy-MM-dd"));
                atEndTxt = string.Format("DATEADD(hour, DATEDIFF(hour,GETDATE(),GETUTCDATE()), '{0}')", atEnd.ToString("yyyy-MM-dd"));
                wSCB.AppendItem("StartTime", atStartTxt, ">=", true).AppendItem("EndTime", atEndTxt, "<", true);
            }
            switch (atd)
            {
                case AssignTypeDefine.ByStudent:
                    wSCB.AppendItem("CustomerID", ID, "=");
                    break;
                case AssignTypeDefine.ByTeacher:
                    wSCB.AppendItem("TeacherJobID", ID, "=");
                    break;
            }
            WhereLoadingCondition wLC = new WhereLoadingCondition(p => { foreach (var v in wSCB) { p.Add(v); } });
            return this.Load(wLC);
        }

        /// <summary>
        /// 获取指定时间区间段及学员ID的排课记录
        /// 例如：
        /// 2016-04-25 至  2016-04-29
        /// atStart ：  2016-04-25
        /// atEnd   ：  2016-04-30
        /// </summary>
        /// <param name="atStart"></param>
        /// <param name="atEnd"></param>
        /// <param name="cID">Customer ID</param>
        /// <returns></returns>
        public AssignCollection LoadCollection(Assign assign)
        {
            assign.StartTime.NullCheck("StartTime");
            assign.EndTime.NullCheck("EndTime");
            assign.CampusID.NullCheck("CampusID");

            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();

            string atStartTxt = string.Empty, atEndTxt = string.Empty;
            atStartTxt = string.Format("DATEADD(hour, DATEDIFF(hour,GETDATE(),GETUTCDATE()), '{0}')", assign.StartTime.ToString("yyyy-MM-dd"));
            atEndTxt = string.Format("DATEADD(hour, DATEDIFF(hour,GETDATE(),GETUTCDATE()), '{0}')", assign.EndTime.ToString("yyyy-MM-dd"));
            wSCB.AppendItem("StartTime", atStartTxt, ">=", true).AppendItem("EndTime", atEndTxt, "<", true);

            wSCB.AppendItem("CampusID", assign.CampusID, "=");

            if (!string.IsNullOrEmpty(assign.TeacherName))
            {
                wSCB.AppendItem("TeacherName", string.Format("%{0}%", assign.TeacherName), "like");
            }
            if (!string.IsNullOrEmpty(assign.AccountID))
            {
                wSCB.AppendItem("AccountID", string.Format("%{0}%", assign.AccountID), "like");
            }

            if (!string.IsNullOrEmpty(assign.Subject))
            {
                wSCB.AppendItem("Subject", assign.Subject, "=");
            }
            if (!string.IsNullOrEmpty(assign.Grade))
            {
                wSCB.AppendItem("Grade", assign.Grade, "=");
            }
            if (assign.IsFullTimeTeacher != null)
            {
                wSCB.AppendItem("IsFullTimeTeacher", assign.IsFullTimeTeacher, "=");
            }
            WhereLoadingCondition wLC = new WhereLoadingCondition(p => { foreach (var v in wSCB) { p.Add(v); } });
            return this.Load(wLC);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignID"></param>
        /// <returns></returns>
        public AssignCollection LoadCollection(IList<string> assignID)
        {
            string assignIDs = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var v in assignID)
                sb.AppendFormat(",'{0}'", v);
            if (sb.Length == 0)
                return null;
            assignIDs = string.Format("({0})", sb.ToString().Substring(1));
            return this.Load(builder => builder.AppendItem("AssignID", assignIDs, "in", true));
        }
    }
}
