using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using PPTS.Contracts.Search.Models;

namespace PPTS.Services.Search.Executors
{
    public class CustomerSearchExecutor_ByCustomer : CustomerSearchExecutorBase
    {
        public override CustomerSearchUpdateType SearchUpdateType
        {
            get
            {
                return CustomerSearchUpdateType.Customer;
            }
        }
        protected override DataTable PrepareData(IList<string> customerIDs)
        {
            string sql = this.SelectSql(customerIDs);
            DataSet ds = DbHelper.RunSqlReturnDS(sql, CustomerAdapter.Instance.GetDbContext().Name);
            return ds.Tables[0];
        }

        private string SelectSql(IList<string> customerIDs)
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
    }
}