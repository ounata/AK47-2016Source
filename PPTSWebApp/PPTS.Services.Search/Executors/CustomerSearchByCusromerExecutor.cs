using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace PPTS.Services.Search.Executors
{
    public class CustomerSearchByCusromerExecutor : CustomerSearchExecutorBase
    {
        public readonly static CustomerSearchByCusromerExecutor Instance = new CustomerSearchByCusromerExecutor();
        public override void UpdateInContext(IEnumerable<string> customerIDs)
        {
            string sql = LoadCustomerInfoSQL(customerIDs);
            DataSet ds = DbHelper.RunSqlReturnDS(sql, CustomerAdapter.Instance.GetDbContext().Name);
            StringBuilder updatesql = new StringBuilder();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string customerID = dr["CustomerID"].ToString();
                updatesql.AppendLine(string.Format("if exists({0})", ExistCustomerInfoSQL(customerID)));
                updatesql.AppendLine("begin");
                updatesql.AppendLine(string.Format("{0}", UpdateCustomerInfoSQL(customerID, dr)));
                updatesql.AppendLine("end");
                updatesql.AppendLine("else");
                updatesql.AppendLine("begin");
                updatesql.AppendLine(string.Format("{0}", InsertCustomerInfoSQL(dr)));
                updatesql.AppendLine("end");
            }
            GetSearchAdapter().GetSqlContext().AppendSqlInContext(TSqlBuilder.Instance, updatesql.ToString());
        }

        public override void Execute(IEnumerable<string> customerIDs)
        {
            this.UpdateInContext(customerIDs);
            base.Execute(customerIDs);
        }

        #region 操作双向数据服务
        private string LoadCustomerInfoSQL(IEnumerable<string> customerIDs)
        {
            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("CustomerID");
            inBuilder.AppendItem(customerIDs.ToArray());
            SelectSqlClauseBuilder selectBuilder = new SelectSqlClauseBuilder();
            selectBuilder.AppendFields("CampusID"
                , "CampusName"
                , "CustomerID"
                , "CustomerCode"
                , "CustomerName"
                , "CustomerLevel"
                , "CustomerStatus"
                , "StudentStatus"
                , "IsOneParent"
                , "Character"
                , "Birthday"
                , "Gender"
                , "Email"
                , "IDType"
                , "IDNumber"
                , "VipType"
                , "VipLevel"
                , "EntranceGrade"
                , "Grade"
                , "SchoolYear"
                , "SubjectType"
                , "StudentType"
                , "ContactType"
                , "SourceMainType"
                , "SourceSubType"
                , "SourceSystem"
                , "ReferralStaffID"
                , "ReferralStaffName"
                , "ReferralStaffJobID"
                , "ReferralStaffJobName"
                , "ReferralStaffOACode"
                , "ReferralCustomerID"
                , "ReferralCustomerCode"
                , "ReferralCustomerName"
                , "ReferralCount"
                , "PurchaseIntention"
                , "Locked"
                , "LockMemo"
                , "Graduated"
                , "GraduateYear"
                , "SchoolID"
                , "IsStudyAgain"
                , "FirstSignTime"
                , "FirstSignerID"
                , "FirstSignerName"
                , "FollowTime"
                , "FollowStage"
                , "FollowedCount"
                , "NextFollowTime"
                , "VisitedCount"
                , "CreatorID"
                , "CreatorName"
                , "CreatorJobType"
                , "CreateTime"
                , "ModifierID"
                , "ModifierName"
                , "ModifyTime"
                , "TenantCode"
                , "VisitTime"
                , "NextVisitTime");

            string sql = string.Format(@"select {0} from {1} where {2}"
            , selectBuilder.ToSqlString(TSqlBuilder.Instance)
            , CustomerAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , inBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        /// <summary>
        /// CustomerSearch客户信息
        /// </summary>
        /// <param name="model">条件信息</param>
        /// <param name="row">行信息</param>
        /// <returns></returns>
        private string UpdateCustomerInfoSQL(string customerID, DataRow row)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CustomerID", customerID);
            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();
            updateBuilder.AppendItems(row);
            updateBuilder.AppendItem("ServiceModifyTime", "getutcdate()", "=", true);
            string sql = string.Format(@"update {0} set {1} where {2}"
           , GetSearchAdapter().GetQueryMappingInfo().GetQueryTableName()
           , updateBuilder.ToSqlString(TSqlBuilder.Instance)
           , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        private string InsertCustomerInfoSQL(DataRow row)
        {
            InsertSqlClauseBuilder insertBuilder = new InsertSqlClauseBuilder();
            insertBuilder.AppendItems(row);
            string sql = string.Format(@"insert into {0} {1}"
           , GetSearchAdapter().GetQueryMappingInfo().GetQueryTableName()
           , insertBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        private string ExistCustomerInfoSQL(string customerID)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CustomerID", customerID);
            string sql = string.Format(@"select top 1 1 from {0} where {1}"
            , GetSearchAdapter().GetQueryMappingInfo().GetQueryTableName()
            , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
        #endregion 
    }
}