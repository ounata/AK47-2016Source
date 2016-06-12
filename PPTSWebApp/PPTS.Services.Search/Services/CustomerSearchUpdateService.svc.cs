using PPTS.Contracts.Search.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Contracts.Search.Models;
using MCS.Library.WcfExtensions;
using System.ServiceModel.Web;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Adapters;
using System.Data;
using MCS.Library.Data.Adapters;

namespace PPTS.Services.Search.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“CustomerSearchUpdateService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 CustomerSearchUpdateService.svc 或 CustomerSearchUpdateService.svc.cs，然后开始调试。
    public class CustomerSearchUpdateService : ICustomerSearchUpdateService
    {
        #region 更新客户信息部分
        /// <summary>
        /// 更新客户部分信息
        /// </summary>
        /// <param name="model">数据模型</param>
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void UpdateByCustomerInfo(CustomerSearchUpdateModel model)
        {
            string sql = LoadCustomerInfoSQL(model);
            DataSet ds = DbHelper.RunSqlReturnDS(sql, CustomerAdapter.Instance.GetDbContext().Name);
            if (ds.Tables[0].Rows.Count <= 0)
                return;
            StringBuilder updatesql = new StringBuilder();
            updatesql.AppendLine(string.Format("if exists({0})", ExistCustomerInfoSQL(model)));
            updatesql.AppendLine("begin");
            updatesql.AppendLine(string.Format("{0}", UpdateCustomerInfoSQL(model, ds.Tables[0].Rows[0])));
            updatesql.AppendLine("end");
            updatesql.AppendLine("else");
            updatesql.AppendLine("begin");
            updatesql.AppendLine(string.Format("{0}", InsertCustomerInfoSQL(ds.Tables[0].Rows[0])));
            updatesql.AppendLine("end");
            DbHelper.RunSql(updatesql.ToString(), CustomerSearchAdapter.Instance.GetDbContext().Name);
        }

        /// <summary>
        /// 取得客户信息
        /// </summary>
        /// <param name="model">条件信息</param>
        /// <returns></returns>
        private string LoadCustomerInfoSQL(CustomerSearchUpdateModel model)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CustomerID", model.CustomerID);
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
                , "CreateTime"
                , "ModifierID"
                , "ModifierName"
                , "ModifyTime"
                , "TenantCode"
                , "VisitTime"
                , "NextVisitTime");

            string sql = string.Format(@"select top 1 {0} from {1} where {2}"
            , selectBuilder.ToSqlString(TSqlBuilder.Instance)
            , CustomerAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        /// <summary>
        /// CustomerSearch客户信息
        /// </summary>
        /// <param name="model">条件信息</param>
        /// <param name="row">行信息</param>
        /// <returns></returns>
        private string UpdateCustomerInfoSQL(CustomerSearchUpdateModel model, DataRow row)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CustomerID", model.CustomerID);
            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();
            updateBuilder.AppendItems(row);
            updateBuilder.AppendItem("ServiceModifyTime", "getutcdate()", "=", true);
            string sql = string.Format(@"update {0} set {1} where {2}"
           , CustomerSearchAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
           , updateBuilder.ToSqlString(TSqlBuilder.Instance)
           , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        private string InsertCustomerInfoSQL(DataRow row)
        {
            InsertSqlClauseBuilder insertBuilder = new InsertSqlClauseBuilder();
            insertBuilder.AppendItems(row);
            string sql = string.Format(@"insert into {0} {1}"
           , CustomerSearchAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
           , insertBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        #endregion

        private string ExistCustomerInfoSQL(CustomerSearchUpdateModel model)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CustomerID", model.CustomerID);
            string sql = string.Format(@"select top 1 1 from {0} where {1}"
            , CustomerSearchAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}
