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

namespace PPTS.Data.Orders.Adapters
{
    public class AssignsAdapter : AssignAdapterBase<Assign, AssignCollection>
    {
        public static readonly AssignsAdapter Instance = new AssignsAdapter();
        private AssignsAdapter()
        { }
        /// <summary>
        /// 更新指定记录的排课时间
        /// </summary>
        /// <param name="model"></param>
        public void UpdateAssignTime(Assign model)
        {
            SqlContextItem sCI = AssignsAdapter.Instance.GetSqlContext();

            UpdateSqlClauseBuilder uSCB = new UpdateSqlClauseBuilder();
            uSCB.AppendItem("StartTime", model.StartTime, "=");
            uSCB.AppendItem("EndTime", model.EndTime, "=");
            uSCB.AppendItem("ModifierID", model.ModifierID, "=");
            uSCB.AppendItem("ModifierName", model.ModifierName, "=");
            uSCB.AppendItem("ModifyTime", "GETUTCDATE", "=", true);

            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
            wSCB.AppendItem("AssetID", model.AssetID, "in");

            sCI.AppendSqlInContext(TSqlBuilder.Instance, "update {0} set {1} where {2}"
                , this.GetTableName()
                , uSCB.ToSqlString(TSqlBuilder.Instance)
                , wSCB.ToSqlString(TSqlBuilder.Instance));

        }

        /// <summary>
        /// 取消排课
        /// </summary>
        /// <param name="assignID"></param>
        /// <param name="status"></param>
        public void CancelAssignInContext(IList<string> assignID, string operaterID, string operaterName)
        {
            assignID.NullCheck("assignID");
            operaterID.CheckStringIsNullOrEmpty("operaterID");
            operaterName.CheckStringIsNullOrEmpty("operaterName");

            SqlContextItem sCI = this.GetSqlContext();

            UpdateSqlClauseBuilder uSCB = new UpdateSqlClauseBuilder();
            uSCB.AppendItem("AssignStatus", (int)AssignStatusDefine.Invalid, "=");
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
            wSCB.AppendItem("AssignID", assignIDs, "in");
            wSCB.AppendItem("AssignStatus", (int)AssignStatusDefine.Assigned, "=");

            sCI.AppendSqlInContext(TSqlBuilder.Instance, "update {0} set {1} where {2}"
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
        /// 调课
        /// </summary>
        /// <param name="ac"></param>
        public void ResetAssignInContext(AssignCollection ac)
        {
            SqlContextItem sCI = this.GetSqlContext();
            foreach (var model in ac)
            {
                UpdateSqlClauseBuilder uSCB = new UpdateSqlClauseBuilder();
                uSCB.AppendItem("StartTime", model.StartTime);
                uSCB.AppendItem("EndTime", model.EndTime);
                uSCB.AppendItem("ModifierID", model.ModifierID);
                uSCB.AppendItem("ModifierName", model.ModifierName);
                uSCB.AppendItem("ModifyTime", "GETUTCDATE", "=", true);

                WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
                wSCB.AppendItem("AssetID", model.AssetID);
                wSCB.AppendItem("AssignStatus", (int)AssignStatusDefine.Assigned);

                sCI.AppendSqlInContext(TSqlBuilder.Instance, "update {0} set {1} where {2}" , this.GetTableName()
                    ,uSCB.ToSqlString(TSqlBuilder.Instance),wSCB.ToSqlString(TSqlBuilder.Instance));
            }
        }

        /// <summary>
        /// 获取指定时间区间段及学员ID的排课记录
        /// 注意时间要传递UTC时间
        /// </summary>
        /// <param name="atStart"></param>
        /// <param name="atEnd"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public AssignCollection LoadCollection(DateTime atStart, DateTime atEnd, string customerID)
        {
            atStart.NullCheck("atStart");
            atEnd.NullCheck("atEnd");
            customerID.NullCheck("customerID");

            return this.Load(build => build
            .AppendItem("StartTime", atStart, ">=")
            .AppendItem("StartTime", atEnd, "<=")
            .AppendItem("CustomerID", customerID, "="));
        }
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

        public IList<AssignSuper> LoadAssignSuper(string OperaterCampusID,string CustomerID)
        {
            string sql = @"select a.AssetID,a.AssetCode,a.AssetName,a.CustomerID,a.Price
                        ,b.Grade,b.GradeName ,b.[Subject] ,b.SubjectName ,b.CourseLevel ,b.CourseLevelName ,b.LessonDuration ,b.LessonDurationValue 
                        ,b.ProductID ,b.ProductCode,b.ProductName,b.ProductCampusID,b.ProductCampusName
                        ,c.CustomerCode,c.CustomerName,c.ConsultantID,c.ConsultantJobID,c.ConsultantName,c.EducatorID,c.EducatorJobID,c.EducatorName
                        from Assets a
                        left join OrderItems b on a.AssetRefID = b.ItemID
                        left join Orders c on c.OrderID = b.OrderID";

            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
            wSCB.AppendItem("a.Amount", 0,">");
            //wSCB.AppendItem("a.CustomerCampusID", OperaterCampusID);
            wSCB.AppendItem("a.CustomerID", CustomerID);

            sql = string.Format("{0} where {1}", sql, wSCB.ToSqlString(TSqlBuilder.Instance));
            System.Data.DataSet ds = DbHelper.RunSqlReturnDS(sql,this.GetConnectionName());
            IList<AssignSuper> result = GetAssignSupe(ds.Tables[0]);
            return result;           
        }
        private IList<AssignSuper> GetAssignSupe(System.Data.DataTable dt)
        {
            IList<AssignSuper> result = new List<AssignSuper>();
            if (dt == null || dt.Rows.Count == 0)
                return result;

            Type superType = typeof(AssignSuper);
            System.Reflection.PropertyInfo[] stPropertyInfo = superType.GetProperties();
            IList<System.Reflection.PropertyInfo> existsPI = new List<System.Reflection.PropertyInfo>();
            foreach (var v in stPropertyInfo)
            {
                if (dt.Columns.Contains(v.Name))
                {
                    existsPI.Add(v);
                }
            }
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                AssignSuper super = new AssignSuper();
                foreach (var pInfo in existsPI)
                {
                    try
                    {
                        pInfo.SetValue(super, dr[pInfo.Name] == System.DBNull.Value ? string.Empty : dr[pInfo.Name], null);
                    }
                    catch
                    {

                    }

                }
                result.Add(super);
            }
            return result;
        }

        ///// <summary>
        ///// 加载学员最近一个月的排课记录
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //public AssignCollection LoadCollection(string customerID,AssignStatusDefine)
        //{
        //    customerID.NullCheck("customerID");

        //    return this.Load(build => build
        //    .AppendItem("StartTime", "DATEADD(month,-1,(GETUTCDATE()-1))", ">=",true)
        //    .AppendItem("StartTime", "GETUTCDATE()+1", "<=",true)
        //    .AppendItem("CustomerID", customerID, "="));

        //}

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="assetid"></param>
        /// <returns></returns>
        public Assign Load(string assignID)
        {
            return this.Load(builder => builder.AppendItem("AssignID", assignID)).SingleOrDefault();
        }



    }
}
