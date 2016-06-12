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
            wSCB.AppendItem("AssignStatus", (int)AssignStatusDefine.Assigned, "=");

            //InLoadingCondition c =  new InLoadingCondition(p => p.AppendItem(assignID), "AssignID");


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

        /// <summary>
        /// 统计账户确认课时价值信息
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <param name="startTime">时间</param>
        /// <returns></returns>
        public decimal LoadAccountConfirmAssignByDateTime(string accountID, DateTime startTime)
        {
            accountID.NullCheck("accountID");
            decimal assignValue = 0;
            DataSet ds = DbHelper.RunSqlReturnDS(LoadAccountConfirmAssignForRefundByDateTimeSQL(accountID, startTime), this.ConnectionName);
            if (ds.Tables[0].Rows.Count > 0)
                assignValue = (decimal)(ds.Tables[0].Rows[0][0]);
            return assignValue;
        }

        /// <summary>
        /// 统计退费需要的课时消耗价值及折扣返还价值信息
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <param name="newDiscount">新折扣</param>
        /// <param name="startTime">起始时间</param>
        /// <returns></returns>
        public decimal LoadDiscountReturnConfirmAssignByDateTime(string accountID, decimal newDiscount, DateTime startTime)
        {
            accountID.NullCheck("accountID");
            decimal deductionValue = 0;
            DataSet ds = DbHelper.RunSqlReturnDS(LoadDiscountReturnConfirmAssignByDateTimeSQL(accountID, newDiscount, startTime), this.ConnectionName);
            if (ds.Tables[0].Rows.Count > 0)
                deductionValue = (decimal)(ds.Tables[0].Rows[0][0]);
            return deductionValue;
        }

        /// <summary>
        /// 统计退费需要的课时消耗价值及折扣返还价值信息
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="newDiscount"></param>
        /// <param name="startTime"></param>
        /// <param name="assignValue"></param>
        /// <param name="discountReturn"></param>
        public void LoadAccountRefundInfoByDateTime(string accountID, decimal newDiscount, DateTime startTime, ref decimal assignValue, ref decimal discountReturn)
        {
            accountID.NullCheck("accountID");
            string sql = string.Format("{0};{1};", LoadAccountConfirmAssignForRefundByDateTimeSQL(accountID, startTime), LoadDiscountReturnConfirmAssignByDateTimeSQL(accountID, newDiscount, startTime));
            DataSet ds = DbHelper.RunSqlReturnDS(sql, this.ConnectionName);
            if (ds.Tables[0].Rows.Count > 0)
                assignValue = (decimal)(ds.Tables[0].Rows[0][0]);
            if (ds.Tables[1].Rows.Count > 0)
                discountReturn = (decimal)(ds.Tables[1].Rows[0][0]);
        }

        public bool ExistAccountConfirmAssignByDateTime(string accountID, DateTime startTime)
        {
            accountID.NullCheck("accountID");
            DataSet ds = DbHelper.RunSqlReturnDS(ExistAccountConfirmAssignByDateTimeSQL(accountID, startTime), this.ConnectionName);
            return (ds.Tables[0].Rows.Count > 0);
        }

        /// <summary>
        /// 存在指定时间内的课时消耗记录信息
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <param name="startTime">起始时间</param>
        /// <returns></returns>
        private string ExistAccountConfirmAssignByDateTimeSQL(string accountID, DateTime startTime)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("asset.AccountID", accountID)
                .AppendItem("ass.AssignStatus", (int)AssignStatusDefine.Finished)
                .AppendItem("ass.StartTime", TimeZoneContext.Current.ConvertTimeToUtc(startTime), ">=");
            string sql = string.Format(@"select top 1 1 from {0}  as ass inner join {1} AS asset
            on ass.AssetID=asset.AssetID 
            where {2}"
            , this.GetQueryMappingInfo().GetQueryTableName()
            , AssetAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        /// <summary>
        /// 消耗价值取数规则[不包含买赠部分]
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <param name="startTime">开始时间</param>
        /// <returns></returns>
        private string LoadAccountConfirmAssignForRefundByDateTimeSQL(string accountID, DateTime startTime)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("asset.AccountID", accountID)
                .AppendItem("ass.AssignStatus", (int)AssignStatusDefine.Finished)
                .AppendItem("ass.StartTime", TimeZoneContext.Current.ConvertTimeToUtc(startTime), ">=");
            string sql = string.Format(@"select isnull(sum(isnull(ass.ConfirmPrice,0)*isnull(ass.Amount,0)),0) AssignValue from {0}  as ass inner join {1} AS asset
            on ass.AssetID=asset.AssetID 
            inner join {2} oi on asset.AssetRefID=oi.ItemID
            and oi.DiscountType in (1,2,3)
            where {3}"
            , this.GetQueryMappingInfo().GetQueryTableName()
            , AssetAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , OrderItemAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , whereBuilder.ToSqlString(TSqlBuilder.Instance)
            );
            return sql;
        }

        /// <summary>
        /// 折扣返还，指定日期的sum(消耗价值/对应订购单使用折扣率*(新折扣率-对应订购单使用折扣率))[不包含买赠部分]
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <param name="newDiscount">新折扣率</param>
        /// <param name="startTime">当前日期</param>
        /// <returns></returns>
        private string LoadDiscountReturnConfirmAssignByDateTimeSQL(string accountID, decimal newDiscount, DateTime startTime)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("asset.AccountID", accountID)
                .AppendItem("ass.AssignStatus", (int)AssignStatusDefine.Finished)
                .AppendItem("ass.StartTime", TimeZoneContext.Current.ConvertTimeToUtc(startTime), ">=");
            string sql = string.Format(@"select isnull(sum(isnull(ass.Amount,0)*isnull(ass.ConfirmPrice,0)/isnull(oi.DiscountRate,1)*({0}-isnull(oi.DiscountRate,1))),0) DeductionValue 
                                         from {1} ass 
                                         inner join {2} asset on ass.AssetID=asset.AssetID
                                         inner join {3} oi on asset.AssetRefID=oi.ItemID
                                         and oi.DiscountType in (1,2,3)
                                         where {4} "
                , newDiscount
                , this.GetQueryMappingInfo().GetQueryTableName()
                , AssetAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                , OrderItemAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}
