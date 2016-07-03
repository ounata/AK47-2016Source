using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;
using System.Data;
using MCS.Library.Principal;
using MCS.Library.Data.Mapping;
using System.Reflection;
using PPTS.Data.Common.Entities;
using MCS.Library.Data;
using MCS.Library.Passport;

namespace PPTS.Data.Common.Authorization
{
    public class ScopeAuthorization<T>
    {

        private string connectionName = string.Empty;
        public ScopeAuthorization(string connName)
        {
            this.connectionName = connName;
        }

        public static ScopeAuthorization<T> GetInstance(string connName)
        {
            return new ScopeAuthorization<T>(connName);
        }

        #region Adpter集合
        /// <summary>
        /// 客户关系操作权限Adapter
        /// </summary>
        /// <returns></returns>
        protected CustomerRelationAuthorizationAdaper GetCustomerRelationAuthorizationAdaper()
        {
            return CustomerRelationAuthorizationAdaper.GetInstance(this.GetConnectionName());
        }

        /// <summary>
        /// 客户机构操作权限Adapter
        /// </summary>
        /// <returns></returns>
        protected CustomerOrgAuthorizationAdapter GetCustomerOrgAuthorizationAdapter()
        {
            return CustomerOrgAuthorizationAdapter.GetInstance(this.GetConnectionName());
        }

        /// <summary>
        /// 所有者操作权限Adpter
        /// </summary>
        /// <returns></returns>
        protected OwnerRelationAuthorizationAdapter GetOwnerRelationAuthorizationAdapter()
        {
            return OwnerRelationAuthorizationAdapter.GetInstance(this.GetConnectionName());
        }

        /// <summary>
        /// 记录操作权限Adpter
        /// </summary>
        /// <returns></returns>
        protected RecordOrgAuthorizationAdapter GetRecordOrgAuthorizationAdapter()
        {
            return RecordOrgAuthorizationAdapter.GetInstance(this.GetConnectionName());
        }

        /// <summary>
        /// 课时操作权限Adpter
        /// </summary>
        /// <returns></returns>
        protected CourseOrgAuthorizationAdapter GetCourseOrgAuthorizationAdapter()
        {
            return CourseOrgAuthorizationAdapter.GetInstance(this.GetConnectionName());
        }

        /// <summary>
        /// 课时所有者操作权限Adpter
        /// </summary>
        /// <returns></returns>
        protected CourseRelationAuthorizationAdpter GetCourseRelationAuthorizationAdpter()
        {
            return CourseRelationAuthorizationAdpter.GetInstance(this.GetConnectionName());
        }
        #endregion

        #region 权限Builder拼接查询条件
        private WhereSqlClauseBuilder BuildRelationExistsWhereBuilder(string objectID,
            string owenerColumnName, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("OwnerID", owenerColumnName, "=", true)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuildRelationExistsWhereBuilder(string objectID, RelationType objectType,
            string owenerColumnName, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("ObjectType", (int)objectType)
                .AppendItem("OwnerID", owenerColumnName, "=", true)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuildRelationWhereBuilder(string objectID,
            string ownerID, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
               .AppendItem("OwnerID", ownerID)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuildRelationWhereBuilder(string objectID, RelationType objectType,
            string ownerID, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("ObjectType", (int)objectType)
                .AppendItem("OwnerID", ownerID)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuildOrgExistsWhereBuilder(string objectID, OrgType objectType, RelationType relationType,
            string owenerColumnName, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("ObjectType", (int)objectType)
                .AppendItem("RelationType", (int)relationType)
                .AppendItem("OwnerID", owenerColumnName, "=", true)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuildOrgWhereBuilder(string objectID, OrgType objectType, RelationType relationType,
            string ownerID, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("ObjectType", (int)objectType)
                .AppendItem("RelationType", (int)relationType)
                .AppendItem("OwnerID", ownerID)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuildOrgExistsWhereBuilder(string objectID, OrgType objectType,
            string owenerColumnName, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("ObjectType", (int)objectType)
                .AppendItem("OwnerID", owenerColumnName, "=", true)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuildOrgWhereBuilder(string objectID, OrgType objectType,
            string ownerID, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("ObjectType", (int)objectType)
                .AppendItem("OwnerID", ownerID)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }


        #endregion

        #region 权限Builder
        private WhereSqlClauseBuilder CustomerRelationWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            CustomerRelationAuthorizationAdaper adapter = GetCustomerRelationAuthorizationAdaper();
            string sql = BuildExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder CustomerOrgWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            CustomerOrgAuthorizationAdapter adapter = GetCustomerOrgAuthorizationAdapter();
            string sql = BuildExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder OwnerRelationWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            OwnerRelationAuthorizationAdapter adapter = GetOwnerRelationAuthorizationAdapter();
            string sql = BuildExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder RecordOrgWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            RecordOrgAuthorizationAdapter adapter = GetRecordOrgAuthorizationAdapter();
            string sql = BuildExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder CourseOrgWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            CourseOrgAuthorizationAdapter adapter = GetCourseOrgAuthorizationAdapter();
            string sql = BuildExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder CourseRelationWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            CourseRelationAuthorizationAdpter adapter = GetCourseRelationAuthorizationAdpter();
            string sql = BuildExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        /// <summary>
        /// 构建单个查询条件
        /// </summary>
        /// <param name="queryTableName">查询数据表</param>
        /// <param name="whereBuilder">查询条件</param>
        /// <returns></returns>
        private string BuildExistsSQL(string queryTableName, string whereBuilder)
        {
            return string.Format("exists(select 1 from {0} with(nolock) where {1})", queryTableName, whereBuilder);
        }
        #endregion

        #region 拼接查询条件
        protected ConnectiveSqlClauseCollection CustomerRelationAuthExistsBuilder(string jobID, string ownerColumnName, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<CustomerRelationScopeAttribute> existAttributes = attributeModels.Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as CustomerRelationScopeAttribute).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            existAttributes.ForEach(action =>
            {
                if (Enum.GetValues(typeof(RelationType)).Exists<int>((a) => { return (a == (int)action.RelationType); }))
                {
                    sqlClause.Add(CustomerRelationWhereBuilder(BuildRelationExistsWhereBuilder(jobID
                   , action.RelationType
                   , ownerColumnName
                   , (RecordType)action.RecordType)));
                }
                else
                {
                    sqlClause.Add(CustomerRelationWhereBuilder(BuildRelationExistsWhereBuilder(jobID
                     , ownerColumnName
                     , (RecordType)action.RecordType)));
                }
            });

            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection CustomerRelationAuthSelectBuilder(string jobID, string ownerID, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<CustomerRelationScopeAttribute> existAttributes = attributeModels.Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as CustomerRelationScopeAttribute).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            existAttributes.ForEach(action =>
            {
                if (Enum.GetValues(typeof(RelationType)).Exists<int>((a) =>
                {
                    return (a == (int)action.RelationType);
                }))
                {
                    sqlClause.Add(CustomerRelationWhereBuilder(BuildRelationWhereBuilder(jobID
                   , action.RelationType
                   , ownerID
                   , (RecordType)action.RecordType)));
                }
                else
                {
                    sqlClause.Add(CustomerRelationWhereBuilder(BuildRelationWhereBuilder(jobID
                    , ownerID
                    , (RecordType)action.RecordType)));
                }
            });

            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection OrgAuthExistsBuilder(IOrganization org, string ownerColumnName, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<RecordOrgScopeAttribute> existAttributes = attributeModels.Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as RecordOrgScopeAttribute).ToList();

            //分类
            List<RecordOrgScopeAttribute> existCustomerAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Customer)).ToList();
            List<RecordOrgScopeAttribute> existCourseAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Assign)).ToList();
            List<RecordOrgScopeAttribute> existRecordAttributes = existAttributes.Where(att => (att.RecordType != RecordType.Customer && att.RecordType != RecordType.Assign)).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            OrgExistsSqlClauseBuilder(sqlClause, org, ownerColumnName, existCustomerAttributes, CustomerOrgWhereBuilder);
            OrgExistsSqlClauseBuilder(sqlClause, org, ownerColumnName, existCourseAttributes, CourseOrgWhereBuilder);
            OrgExistsSqlClauseBuilder(sqlClause, org, ownerColumnName, existRecordAttributes, RecordOrgWhereBuilder);

            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection OrgAuthSelectBuilder(IOrganization org, string ownerID, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<RecordOrgScopeAttribute> existAttributes = attributeModels.Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as RecordOrgScopeAttribute).ToList();

            List<RecordOrgScopeAttribute> existCustomerAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Customer)).ToList();
            List<RecordOrgScopeAttribute> existCourseAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Assign)).ToList();
            List<RecordOrgScopeAttribute> existRecordAttributes = existAttributes.Where(att => (att.RecordType != RecordType.Customer && att.RecordType != RecordType.Assign)).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            OrgSelectSqlClauseBuilder(sqlClause, org, ownerID, existCustomerAttributes, CustomerOrgWhereBuilder);
            OrgSelectSqlClauseBuilder(sqlClause, org, ownerID, existCourseAttributes, CourseOrgWhereBuilder);
            OrgSelectSqlClauseBuilder(sqlClause, org, ownerID, existRecordAttributes, RecordOrgWhereBuilder);

            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection OrgCustomerRelationAuthExistsBuilder(IOrganization org, string jobID, string ownerColumnName, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<OrgCustomerRelationScopeAttribute> existAttributes = attributeModels.Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as OrgCustomerRelationScopeAttribute).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;
            existAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgExistsSqlClauseBuilder(org, ownerColumnName, attribute, CustomerOrgWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection OrgCustomerRelationAuthSelectBuilder(IOrganization org, string jobID, string ownerID, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<OrgCustomerRelationScopeAttribute> existAttributes = attributeModels.Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as OrgCustomerRelationScopeAttribute).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;
            existAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgSelectSqlClauseBuilder(org, ownerID, attribute, CustomerOrgWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection OwnerRelationAuthExistsBuilder(string jobID, string ownerColumnName, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<OwnerRelationScopeAttribute> existAttributes = attributeModels
                .Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as OwnerRelationScopeAttribute).ToList();

            List<OwnerRelationScopeAttribute> existCustomerAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Customer)).ToList();
            List<OwnerRelationScopeAttribute> existCourseAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Assign)).ToList();
            List<OwnerRelationScopeAttribute> existRecordAttributes = existAttributes.Where(att => (att.RecordType != RecordType.Customer && att.RecordType != RecordType.Assign)).ToList();


            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            OwnerRelationExistsSqlClauseBuilder(sqlClause, jobID, ownerColumnName, existCustomerAttributes, CustomerRelationWhereBuilder);
            OwnerRelationExistsSqlClauseBuilder(sqlClause, jobID, ownerColumnName, existCourseAttributes, CourseRelationWhereBuilder);
            OwnerRelationExistsSqlClauseBuilder(sqlClause, jobID, ownerColumnName, existRecordAttributes, OwnerRelationWhereBuilder);

            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection OwnerRelationAuthSelectBuilder(string jobID, string ownerID, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<OwnerRelationScopeAttribute> existAttributes = attributeModels
                .Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as OwnerRelationScopeAttribute).ToList();

            List<OwnerRelationScopeAttribute> existCustomerAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Customer)).ToList();
            List<OwnerRelationScopeAttribute> existCourseAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Assign)).ToList();
            List<OwnerRelationScopeAttribute> existRecordAttributes = existAttributes.Where(att => (att.RecordType != RecordType.Customer && att.RecordType != RecordType.Assign)).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            OwnerRelationSelectSqlClauseBuilder(sqlClause, jobID, ownerID, existCustomerAttributes, CustomerRelationWhereBuilder);
            OwnerRelationSelectSqlClauseBuilder(sqlClause, jobID, ownerID, existCourseAttributes, CourseRelationWhereBuilder);
            OwnerRelationSelectSqlClauseBuilder(sqlClause, jobID, ownerID, existRecordAttributes, OwnerRelationWhereBuilder);

            return sqlClause;
        }

        #region 拼装方法
        private WhereSqlClauseBuilder OrgExistsSqlClauseBuilder(IOrganization org
            , string ownerColumnName
            , RecordOrgScopeAttribute attribute
            , Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun
            )
        {
            IOrganization parentOrg = org.GetParentOrganizationByType((DepartmentType)Enum.Parse(typeof(DepartmentType), ((int)attribute.OrgType).ToString()));
            if (parentOrg == null) return null;
            if (Enum.GetValues(typeof(RelationType)).Exists<int>((a) => { return (a == (int)attribute.RelationType); }))
                return fun(BuildOrgExistsWhereBuilder(parentOrg.ID, attribute.OrgType, attribute.RelationType, ownerColumnName, attribute.RecordType));
            else
                return fun(BuildOrgExistsWhereBuilder(parentOrg.ID, attribute.OrgType, ownerColumnName, attribute.RecordType));
        }

        private WhereSqlClauseBuilder OrgSelectSqlClauseBuilder(IOrganization org
            , string ownerID
            , RecordOrgScopeAttribute attribute
            , Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun)
        {
            string orgtype = ((int)attribute.OrgType).ToString();
            IOrganization parentOrg = org.GetParentOrganizationByType((DepartmentType)Enum.Parse(typeof(DepartmentType), ((int)attribute.OrgType).ToString()));
            if (parentOrg == null) return null;
            if (Enum.GetValues(typeof(RelationType)).Exists<int>((a) => { return (a == (int)attribute.RelationType); }))
                return fun(BuildOrgWhereBuilder(parentOrg.ID, attribute.OrgType, attribute.RelationType, ownerID, attribute.RecordType));
            else
                return fun(BuildOrgWhereBuilder(parentOrg.ID, attribute.OrgType, ownerID, attribute.RecordType));
        }

        private WhereSqlClauseBuilder OwnerRelationExistsSqlClauseBuilder(string jobID
            , string ownerColumnName
            , OwnerRelationScopeAttribute attribute
            , Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun
            )
        {
            if (Enum.GetValues(typeof(RelationType)).Exists<int>((a) => { return (a == (int)attribute.RelationType); }))
                return fun(BuildRelationExistsWhereBuilder(jobID, attribute.RelationType, ownerColumnName, attribute.RecordType));
            else
                return fun(BuildRelationExistsWhereBuilder(jobID, ownerColumnName, attribute.RecordType));
        }

        private WhereSqlClauseBuilder OwnerRelationSelectSqlClauseBuilder(string jobID
            , string ownerID
            , OwnerRelationScopeAttribute attribute
            , Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun
            )
        {
            if (Enum.GetValues(typeof(RelationType)).Exists<int>((a) => { return (a == (int)attribute.RelationType); }))
                return fun(BuildRelationWhereBuilder(jobID, attribute.RelationType, ownerID, attribute.RecordType));
            else
                return fun(BuildRelationWhereBuilder(jobID, ownerID, attribute.RecordType));
        }

        private void OrgExistsSqlClauseBuilder(ConnectiveSqlClauseCollection sqlClause, IOrganization org, string ownerColumnName
            , IEnumerable<RecordOrgScopeAttribute> existRecordAttributes, Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun)
        {
            existRecordAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgExistsSqlClauseBuilder(org, ownerColumnName, attribute, fun);
                if (builder.IsEmpty == false)
                    sqlClause.Add(builder);
            });
        }

        private void OrgSelectSqlClauseBuilder(ConnectiveSqlClauseCollection sqlClause, IOrganization org, string ownerColumnName
            , IEnumerable<RecordOrgScopeAttribute> existRecordAttributes, Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun)
        {
            existRecordAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgSelectSqlClauseBuilder(org, ownerColumnName, attribute, fun);
                if (builder.IsEmpty == false)
                    sqlClause.Add(builder);
            });
        }

        private void OwnerRelationExistsSqlClauseBuilder(ConnectiveSqlClauseCollection sqlClause, string jobID, string ownerColumnName
, IEnumerable<OwnerRelationScopeAttribute> existRecordAttributes, Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun)
        {
            existRecordAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OwnerRelationExistsSqlClauseBuilder(jobID, ownerColumnName, attribute, fun);
                if (builder.IsEmpty == false)
                    sqlClause.Add(builder);
            });
        }

        private void OwnerRelationSelectSqlClauseBuilder(ConnectiveSqlClauseCollection sqlClause, string jobID, string ownerID
    , IEnumerable<OwnerRelationScopeAttribute> existRecordAttributes, Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun)
        {
            existRecordAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OwnerRelationSelectSqlClauseBuilder(jobID, ownerID, attribute, fun);
                if (builder.IsEmpty == false)
                    sqlClause.Add(builder);
            });
        }
        #endregion 

        #endregion

        #region 查询集合汇总
        protected ConnectiveSqlClauseCollection AuthExistsBuilder(string jobID, IOrganization org, string recordKeyColumnName, string recordCusomerIDColumnName, List<string> jobFunctions, List<ScopeBaseAttribute> attributes)
        {
            List<ScopeAttributeModel> models = InitScopeAttributeModelCollection(attributes);

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            ConnectiveSqlClauseCollection sqlChildClause;

            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            if (!IsFullOrgAuth(jobFunctions, models.Where(action => action.ScopeAttributeType == typeof(RecordOrgScopeAttribute)
            && Enum.GetValues(typeof(RelationType)).NotExists<int>((a) => { return (a == (int)((RecordOrgScopeAttribute)action.ScopeAttribute).RelationType); })
            && ((RecordOrgScopeAttribute)action.ScopeAttribute).OrgType == OrgType.HQ).ToList()))//总公司权限
            {
                sqlChildClause = CustomerRelationAuthExistsBuilder(jobID, recordCusomerIDColumnName, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(CustomerRelationScopeAttribute)).ToList());
                if (!sqlChildClause.IsEmpty)
                    sqlClause.Add(sqlChildClause);

                sqlChildClause = OrgAuthExistsBuilder(org, recordKeyColumnName, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(RecordOrgScopeAttribute)).ToList());
                if (!sqlChildClause.IsEmpty)
                    sqlClause.Add(sqlChildClause);

                sqlChildClause = OrgCustomerRelationAuthExistsBuilder(org, jobID, recordCusomerIDColumnName, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(OrgCustomerRelationScopeAttribute)).ToList());

                if (!sqlChildClause.IsEmpty)
                    sqlClause.Add(sqlChildClause);

                sqlChildClause = OwnerRelationAuthExistsBuilder(jobID, recordKeyColumnName, jobFunctions
                                    , models.Where(action => action.ScopeAttributeType == typeof(OwnerRelationScopeAttribute)).ToList());
                if (!sqlChildClause.IsEmpty)
                    sqlClause.Add(sqlChildClause);

                if (sqlClause.IsEmpty)
                    sqlClause.Add(new WhereSqlClauseBuilder().AppendItem(null, " 1 = 0 ", null, true));
            }
            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection AuthSelectBuilder(string jobID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> jobFunctions, List<ScopeBaseAttribute> attributes)
        {
            List<ScopeAttributeModel> models = InitScopeAttributeModelCollection(attributes);

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;
            ConnectiveSqlClauseCollection sqlChildClause;

            if (!IsFullOrgAuth(jobFunctions, models.Where(action =>
            action.ScopeAttributeType == typeof(RecordOrgScopeAttribute)
            && Enum.GetValues(typeof(RelationType)).NotExists<RelationType>((a) => (a == ((RecordOrgScopeAttribute)action.ScopeAttribute).RelationType))
            && ((RecordOrgScopeAttribute)action.ScopeAttribute).OrgType == OrgType.HQ).ToList()))//总公司权限
            {
                sqlChildClause = CustomerRelationAuthSelectBuilder(jobID, recordKeyID, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(CustomerRelationScopeAttribute)).ToList());
                if (!sqlChildClause.IsEmpty)
                    sqlClause.Add(sqlChildClause);

                sqlChildClause = OrgAuthSelectBuilder(org, recordCusomerID, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(RecordOrgScopeAttribute)).ToList());
                if (!sqlChildClause.IsEmpty)
                    sqlClause.Add(sqlChildClause);

                sqlChildClause = OrgCustomerRelationAuthSelectBuilder(org, jobID, recordCusomerID, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(OrgCustomerRelationScopeAttribute)).ToList());
                if (!sqlChildClause.IsEmpty)
                    sqlClause.Add(sqlChildClause);

                sqlChildClause = OwnerRelationAuthSelectBuilder(jobID, recordKeyID, jobFunctions
                                    , models.Where(action => action.ScopeAttributeType == typeof(OwnerRelationScopeAttribute)).ToList());
                if (!sqlChildClause.IsEmpty)
                    sqlClause.Add(sqlChildClause);

                if (sqlClause.IsEmpty)
                    sqlClause.Add(new WhereSqlClauseBuilder().AppendItem(null, " 1 = 0 ", null, true));
            }
            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection AuthExistsBuilder(string jobID, IOrganization org, string recordKeyColumnName, string recordCusomerIDColumnName, List<string> jobFunctions, ActionType type)
        {

            List<ScopeBaseAttribute> classAttributes = GetCurrentScopeBaseAttribute();
            return AuthExistsBuilder(jobID, org, recordKeyColumnName, recordCusomerIDColumnName, jobFunctions, classAttributes.Where(action => action.ActionType == type).ToList());

        }

        protected ConnectiveSqlClauseCollection AuthSelectBuilder(string jobID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> jobFunctions, ActionType type)
        {
            List<ScopeBaseAttribute> classAttributes = GetCurrentScopeBaseAttribute();
            return AuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, jobFunctions, classAttributes.Where(action => action.ActionType == type).ToList());
        }

        protected string AuthSelectBuilderSQL(string jobID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> jobFunctions, ActionType type)
        {
            string result = null;
            ConnectiveSqlClauseCollection sqlClause = AuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, jobFunctions, type);
            if (!sqlClause.IsEmpty)
            {
                string sqlTemplate = @"if({0}) begin select 1 end;";
                result = string.Format(sqlTemplate, sqlClause.ToSqlString(TSqlBuilder.Instance));
            }
            return result;
        }

        protected bool HasAuth(string jobID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> jobFunctions, ActionType type)
        {
            bool result = false;
            string sql = AuthSelectBuilderSQL(jobID, org, recordKeyID, recordCusomerID, jobFunctions, type);
            if (sql.IsNotEmpty())
            {
                DataSet ds = MCS.Library.Data.Adapters.DbHelper.RunSqlReturnDS(sql, this.GetConnectionName());
                result = ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
            }
            else
                result = true;
            return result;
        }

        private List<ScopeAttributeModel> InitScopeAttributeModelCollection(IEnumerable<ScopeBaseAttribute> attributes)
        {
            List<ScopeAttributeModel> result = new List<ScopeAttributeModel>();
            RecordType entityRecordType = GetRecordType();
            foreach (ScopeBaseAttribute attribute in attributes)
            {
                if (attribute.Functions.IsNullOrEmpty()) continue;
                if (attribute is CustomerRelationScopeAttribute)
                {
                    CustomerRelationScopeAttribute crAttribute = attribute as CustomerRelationScopeAttribute;
                    ScopeAttributeModel attributeModel = result.Where(model => model.ScopeAttribute is CustomerRelationScopeAttribute
                    && ((CustomerRelationScopeAttribute)model.ScopeAttribute).RecordType == crAttribute.RecordType
                    && ((CustomerRelationScopeAttribute)model.ScopeAttribute).RelationType == crAttribute.RelationType
                    )
                    .FirstOrDefault();
                    ScopeAttributeModel newAttributeModel = InitScopeAttributeModel(attributeModel, crAttribute, typeof(CustomerRelationScopeAttribute));
                    attributeModel.IsNull(() => result.Add(newAttributeModel));
                }
                else if (attribute is OrgCustomerRelationScopeAttribute)
                {
                    OrgCustomerRelationScopeAttribute ocrAttribute = attribute as OrgCustomerRelationScopeAttribute;
                    ScopeAttributeModel attributeModel = result
                .Where(model => model.ScopeAttribute is OrgCustomerRelationScopeAttribute
                && ((OrgCustomerRelationScopeAttribute)model.ScopeAttribute).RecordType == ocrAttribute.RecordType
                && ((OrgCustomerRelationScopeAttribute)model.ScopeAttribute).RelationType == ocrAttribute.RelationType
                && ((OrgCustomerRelationScopeAttribute)model.ScopeAttribute).OrgType == ocrAttribute.OrgType
                )
                .FirstOrDefault();
                    ScopeAttributeModel newAttributeModel = InitScopeAttributeModel(attributeModel, ocrAttribute, typeof(OrgCustomerRelationScopeAttribute));
                    attributeModel.IsNull(() => result.Add(newAttributeModel));
                }
                else if (attribute is OwnerRelationScopeAttribute)
                {
                    OwnerRelationScopeAttribute orAttribute = attribute as OwnerRelationScopeAttribute;
                    ScopeAttributeModel attributeModel = result.Where(model => model.ScopeAttribute is OwnerRelationScopeAttribute
                    && ((OwnerRelationScopeAttribute)model.ScopeAttribute).RecordType == orAttribute.RecordType
                    && ((OwnerRelationScopeAttribute)model.ScopeAttribute).RelationType == orAttribute.RelationType

                    )
                    .FirstOrDefault();
                    ScopeAttributeModel newAttributeModel = InitScopeAttributeModel(attributeModel, orAttribute, typeof(OwnerRelationScopeAttribute));
                    newAttributeModel.IsNotNull(action =>
                    {
                        if (((OwnerRelationScopeAttribute)action.ScopeAttribute).RecordType <= 0)
                            ((OwnerRelationScopeAttribute)action.ScopeAttribute).RecordType = entityRecordType;
                    });
                    attributeModel.IsNull(() => result.Add(newAttributeModel));
                }
                else if (attribute is RecordOrgScopeAttribute)
                {
                    RecordOrgScopeAttribute roAttribute = attribute as RecordOrgScopeAttribute;
                    ScopeAttributeModel attributeModel = result
                    .Where(model => model.ScopeAttribute is RecordOrgScopeAttribute
                    && ((RecordOrgScopeAttribute)model.ScopeAttribute).RecordType == roAttribute.RecordType
                    && ((RecordOrgScopeAttribute)model.ScopeAttribute).RelationType == roAttribute.RelationType
                    && ((RecordOrgScopeAttribute)model.ScopeAttribute).OrgType == roAttribute.OrgType
                    )
                    .FirstOrDefault();
                    ScopeAttributeModel newAttributeModel = InitScopeAttributeModel(attributeModel, roAttribute, typeof(RecordOrgScopeAttribute));
                    newAttributeModel.IsNotNull(action =>
                    {
                        if (((RecordOrgScopeAttribute)action.ScopeAttribute).RecordType <= 0)
                            ((RecordOrgScopeAttribute)action.ScopeAttribute).RecordType = entityRecordType;
                    });

                    attributeModel.IsNull(() => result.Add(newAttributeModel));
                }
            }
            return result;
        }

        private ScopeAttributeModel InitScopeAttributeModel(ScopeAttributeModel attributeModel, ScopeBaseAttribute attribute, Type t)
        {
            List<string> functions;
            ScopeAttributeModel result = null;
            functions = attribute.Functions.Split(',').ToList();
            attributeModel.IsNotNull(model =>
            {
                model.Functions.IntersectWith(functions);
            });
            attributeModel.IsNull(() =>
            {
                result = new ScopeAttributeModel()
                {
                    ScopeAttribute = attribute,
                    ScopeAttributeType = t,
                    Functions = new HashSet<string>(functions)
                };
            });
            return result;
        }
        #endregion

        #region 对外开放权限验证体系(应用方法)
        private ConnectiveSqlClauseCollection ConnectiveUserCondition(ConnectiveSqlClauseCollection authSqlClause, object conditionObject = null)
        {
            ConnectiveSqlClauseCollection result = null;
            conditionObject.IsNotNull((condition) =>
            {
                result = new ConnectiveSqlClauseCollection();
                if (conditionObject is string)
                {
                    ((string)conditionObject).IsNotEmpty((value) => result.Add(new WhereSqlClauseBuilder().AppendItem(null, (string)value, null, true)));
                }
                else
                {
                    result.Add(ConditionMapping.GetConnectiveClauseBuilder(conditionObject));
                }
                authSqlClause.IsNotNull((clause) => result.Add(clause));
            });
            result.IsNull(() => result = authSqlClause);
            return result;
        }

        #region 列表过滤权限体(读操作)
        public ConnectiveSqlClauseCollection ReadAuthExistsBuilder(string objectID, IOrganization org, string recordKeyColumnName, string recordCusomerIDColumnName, List<string> validFunctions, object conditionObject = null)
        {
            return ConnectiveUserCondition(AuthExistsBuilder(objectID, org, recordKeyColumnName, recordCusomerIDColumnName, validFunctions, ActionType.Read), conditionObject);
        }

        /// <summary>
        /// 读取权限获取规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyColumnName">主键字段名</param>
        /// <param name="recordCusomerIDColumnName">关联客户字段名</param>
        /// <param name="validFunctions">验证权限点集合</param>
        /// <returns>表达式集合</returns>
        public ConnectiveSqlClauseCollection ReadAuthExistsBuilder(PPTSJob job, string recordKeyColumnName, string recordCusomerIDColumnName, List<string> validFunctions, object conditionObject = null)
        {
            StringBuilder sql = new StringBuilder();
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");
            return ReadAuthExistsBuilder(jobID, org, recordKeyColumnName, recordCusomerIDColumnName, validFunctions, conditionObject);
        }

        /// <summary>
        /// 读取权限获取规则(特殊情况,即验证的权限ID不是岗位ID，而是员工ID，例如：周反馈的个人权限过滤)
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="org"></param>
        /// <param name="recordKeyColumnName"></param>
        /// <param name="recordCusomerIDColumnName"></param>
        /// <param name="validFunctions"></param>
        /// <param name="conditionObject"></param>
        /// <returns></returns>
        public ConnectiveSqlClauseCollection ReadAuthExistsBuilder(IUser staff, IOrganization org, string recordKeyColumnName, string recordCusomerIDColumnName, List<string> validFunctions, object conditionObject = null)
        {
            staff.NullCheck("staff");
            staff.ID.CheckStringIsNullOrEmpty("StaffID");

            string jobID = staff.ID;

            return ReadAuthExistsBuilder(jobID, org, RecordKeyColumnName, CustomerIDColumnName, validFunctions, conditionObject);
        }

        /// <summary>
        /// 读取权限获取规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="validFunctions">验证权限点信息</param>
        /// <returns></returns>
        public ConnectiveSqlClauseCollection ReadAuthExistsBuilder(PPTSJob job, List<string> validFunctions, string aliasName = null, object conditionObject = null)
        {
            InitColumenName();
            aliasName.IsNotNull((name) =>
            {
                RecordKeyColumnName = string.Format("{0}.{1}", aliasName, RecordKeyColumnName);
                CustomerIDColumnName = string.Format("{0}.{1}", aliasName, CustomerIDColumnName);
            });
            return ReadAuthExistsBuilder(job, RecordKeyColumnName, CustomerIDColumnName, validFunctions, conditionObject);
        }

        /// <summary>
        ///  读取权限获取规则(特殊情况,即验证的权限ID不是岗位ID，而是员工ID，例如：周反馈的个人权限过滤)
        /// </summary>
        /// <param name="staff">员工信息</param>
        /// <param name="org">机构信息</param>
        /// <param name="validFunctions">权限点信息</param>
        /// <param name="aliasName">别名</param>
        /// <param name="conditionObject">叠加条件</param>
        /// <returns></returns>
        public ConnectiveSqlClauseCollection ReadAuthExistsBuilder(IUser staff, IOrganization org, List<string> validFunctions, string aliasName = null, object conditionObject = null)
        {
            InitColumenName();
            aliasName.IsNotNull((name) =>
            {
                RecordKeyColumnName = string.Format("{0}.{1}", aliasName, RecordKeyColumnName);
                CustomerIDColumnName = string.Format("{0}.{1}", aliasName, CustomerIDColumnName);
            });

            return ReadAuthExistsBuilder(staff, org, RecordKeyColumnName, CustomerIDColumnName, validFunctions, conditionObject);


        }

        /// <summary>
        /// 当前岗位关联的权限绑定
        /// </summary>
        /// <returns></returns>
        public ConnectiveSqlClauseCollection ReadAuthExistsBuilder(string aliasName = null, object conditionObject = null)
        {
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();
            job.NullCheck("CurrentJob");
            return ReadAuthExistsBuilder(job, DeluxePrincipal.Current.MatchedJobFunctions().ToList(), aliasName, conditionObject);
        }

        /// <summary>
        /// 当前用户关联的权限绑定，机构信息取自当前岗位
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="conditionObject"></param>
        /// <returns></returns>
        public ConnectiveSqlClauseCollection ReadStaffAuthExistsBuilder(string aliasName = null, object conditionObject = null)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            user.NullCheck("user");
            PPTSJob job = user.GetCurrentJob();
            job.NullCheck("job");
            return ReadAuthExistsBuilder(user, job.Organization(), DeluxePrincipal.Current.MatchedJobFunctions().ToList(), aliasName, conditionObject);
        }
        #endregion

        #region 单条校验权限体(读操作)
        protected ConnectiveSqlClauseCollection ReadAuthSelectBuilder(string objectID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            return AuthSelectBuilder(objectID, org, recordKeyID, recordCusomerID, validFunctions, ActionType.Read);
        }

        protected ConnectiveSqlClauseCollection ReadAuthSelectBuilder(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");

            return ReadAuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, validFunctions);
        }

        protected ConnectiveSqlClauseCollection ReadAuthSelectBuilder(IUser staff, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            staff.NullCheck("staff");
            staff.ID.CheckStringIsNullOrEmpty("StaffID");

            string jobID = staff.ID;

            org.NullCheck("org");

            return ReadAuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, validFunctions);
        }

        public bool HasReadAuth(string objectID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            return HasAuth(objectID, org, recordKeyID, recordCusomerID, validFunctions, ActionType.Read);
        }

        /// <summary>
        /// 验证单条记录是否有读取权限规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyID">主键字段ID</param>
        /// <param name="recordCusomerID">客户ID</param>
        /// <param name="validFunctions">验证权限点集合</param>
        /// <returns></returns>
        public bool HasReadAuth(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");

            return HasReadAuth(jobID, org, recordKeyID, recordCusomerID, validFunctions);
        }

        public bool HasReadAuth(IUser staff, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            staff.NullCheck("staff");
            staff.ID.CheckStringIsNullOrEmpty("StaffID");

            string jobID = staff.ID;

            org.NullCheck("org");

            return HasReadAuth(jobID, org, recordKeyID, recordCusomerID, validFunctions);
        }

        /// <summary>
        /// 验证单条记录读取权限规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyID">主键字段ID</param>
        /// <param name="recordCusomerID">客户ID</param>
        /// <param name="validFunctions">验证权限点集合</param>
        /// <returns></returns>
        public void CheckReadAuth(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            HasReadAuth(job, recordKeyID, recordCusomerID, validFunctions).FalseThrow("岗位{0}缺少读取数据{1}的权限，类型依赖{2}", job.Name, recordKeyID, typeof(T).ToString());
        }

        public void CheckReadAuth(IUser staff, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            HasReadAuth(staff, org, recordKeyID, recordCusomerID, validFunctions).FalseThrow("员工{0}缺少读取数据{1}的权限，类型依赖{2}", staff.Name, recordKeyID, typeof(T).ToString());
        }

        /// <summary>
        /// 当前岗位对某条数据是否具有操作权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">客户ID</param>
        /// <returns></returns>
        public bool HasReadAuth(string recordKeyID, string recordCustomerID = null)
        {
            bool result = false;
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();
            job.NullCheck("CurrentJob");
            result = HasReadAuth(job, recordKeyID, recordCustomerID, DeluxePrincipal.Current.MatchedJobFunctions().ToList());
            return result;
        }

        /// <summary>
        /// 当前用户对某条数据是否具有操作权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">客户ID</param>
        /// <returns></returns>
        public bool HasStaffReadAuth(string recordKeyID, string recordCustomerID = null)
        {
            bool result = false;
            IUser user = DeluxeIdentity.CurrentUser;
            user.NullCheck("user");
            PPTSJob job = user.GetCurrentJob();
            job.NullCheck("CurrentJob");
            result = HasReadAuth(user, job.Organization(), recordKeyID, recordCustomerID, DeluxePrincipal.Current.MatchedJobFunctions().ToList());
            return result;
        }

        /// <summary>
        /// 检查当前岗位是否具有指定的主键ID及客户ID读取权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">客户ID，如果没有可以提供null</param>
        public void CheckReadAuth(string recordKeyID, string recordCustomerID = null)
        {
            HasReadAuth(recordKeyID, recordCustomerID).FalseThrow("当前岗位缺少读取数据{0}的权限，类型依赖{1}", recordKeyID, typeof(T).ToString());
        }

        /// <summary>
        /// 检查当前岗位是否具有指定的主键ID及客户ID读取权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">客户ID，如果没有可以提供null</param>
        public void CheckStaffReadAuth(string recordKeyID, string recordCustomerID = null)
        {
            HasStaffReadAuth(recordKeyID, recordCustomerID).FalseThrow("当前用户缺少读取数据{0}的权限，类型依赖{1}", recordKeyID, typeof(T).ToString());
        }
        #endregion

        #region 单条校验权限体(写操作)
        /// <summary>
        /// 验证单条记录写权限规则表达式生成
        /// </summary>
        /// <param name="objectID">岗位ID/账户ID</param>
        /// <param name="org">机构信息</param>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCusomerID">客户ID，如果没有客户校验而无需提供</param>
        /// <param name="validFunctions">验证权限点集合</param>
        /// <returns>表达式集合</returns>
        protected ConnectiveSqlClauseCollection EditAuthSelectBuilder(string objectID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            return AuthSelectBuilder(objectID, org, recordKeyID, recordCusomerID, validFunctions, ActionType.Edit);
        }

        /// <summary>
        /// 验证单条记录写权限规则表达式生成
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCusomerID">客户ID，如果没有客户校验而无需提供</param>
        /// <param name="validFunctions">验证权限点集合</param>
        /// <returns>表达式集合</returns>
        protected ConnectiveSqlClauseCollection EditAuthSelectBuilder(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            StringBuilder sql = new StringBuilder();
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");

            return EditAuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, validFunctions);
        }

        protected ConnectiveSqlClauseCollection EditAuthSelectBuilder(IUser staff, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            staff.NullCheck("staff");
            staff.ID.CheckStringIsNullOrEmpty("StaffID");

            string jobID = staff.ID;

            org.NullCheck("org");

            return EditAuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, validFunctions);
        }

        public bool HasEditAuth(string objectID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            return HasAuth(objectID, org, recordKeyID, recordCusomerID, validFunctions, ActionType.Edit);
        }

        /// <summary>
        /// 验证单条记录是否有写入权限规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyID">主键字段ID</param>
        /// <param name="recordCusomerID">客户ID</param>
        /// <param name="validFunctions">验证权限点集合</param>
        /// <returns></returns>
        public bool HasEditAuth(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            StringBuilder sql = new StringBuilder();
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");

            return HasEditAuth(jobID, org, recordKeyID, recordCusomerID, validFunctions);
        }

        public bool HasEditAuth(IUser staff, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            staff.NullCheck("staff");
            staff.ID.CheckStringIsNullOrEmpty("StaffID");

            string jobID = staff.ID;

            org.NullCheck("org");

            return HasEditAuth(jobID, org, recordKeyID, recordCusomerID, validFunctions);
        }

        /// <summary>
        /// 验证单条记录写入权限
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyID">主键字段ID</param>
        /// <param name="recordCusomerID">客户ID</param>
        /// <param name="validFunctions">验证权限点集合</param>
        /// <returns></returns>
        public void CheckEditAuth(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            HasEditAuth(job, recordKeyID, recordCusomerID, validFunctions).FalseThrow("岗位{0}缺少写入数据{1}的权限，类型依赖{2}", job.Name, recordKeyID, typeof(T).ToString());
        }

        public void CheckEditAuth(IUser staff, IOrganization org, string recordKeyID, string recordCusomerID, List<string> validFunctions)
        {
            HasEditAuth(staff, org, recordKeyID, recordCusomerID, validFunctions).FalseThrow("员工{0}缺少写入数据{1}的权限，类型依赖{2}", staff.Name, recordKeyID, typeof(T).ToString());
        }

        /// <summary>
        /// 当前岗位对某条数据是否具有操作权限
        /// </summary>
        /// <param name="recordKeyID"></param>
        /// <param name="recordCustomerID"></param>
        /// <returns></returns>
        public bool HasEditAuth(string recordKeyID, string recordCustomerID = null)
        {
            bool result = false;
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();
            job.NullCheck("CurrentJob");
            result = HasEditAuth(job, recordKeyID, recordCustomerID, DeluxePrincipal.Current.MatchedJobFunctions().ToList());
            return result;
        }

        /// <summary>
        /// 当前用户对某条数据是否具有操作权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">外键关联客户ID</param>
        /// <returns></returns>
        public bool HasStaffEditAuth(string recordKeyID, string recordCustomerID = null)
        {
            bool result = false;
            IUser user = DeluxeIdentity.CurrentUser;
            user.NullCheck("user");
            PPTSJob job = user.GetCurrentJob();
            job.NullCheck("CurrentJob");
            result = HasEditAuth(user, job.Organization(), recordKeyID, recordCustomerID, DeluxePrincipal.Current.MatchedJobFunctions().ToList());
            return result;
        }

        /// <summary>
        /// 检查当前岗位是否具有指定的主键ID及客户ID写入权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">客户ID，如果没有可以提供null</param>
        public void CheckEditAuth(string recordKeyID, string recordCustomerID = null)
        {
            HasEditAuth(recordKeyID, recordCustomerID).FalseThrow("当前岗位缺少写入数据{0}的权限，类型依赖{1}", recordKeyID, typeof(T).ToString());
        }

        /// <summary>
        /// 检查当前用户是否具有指定的主键ID及客户ID写入权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">客户ID</param>
        public void CheckStaffEditAuth(string recordKeyID, string recordCustomerID = null)
        {
            HasStaffEditAuth(recordKeyID, recordCustomerID).FalseThrow("当前用户缺少写入数据{0}的权限，类型依赖{1}", recordKeyID, typeof(T).ToString());
        }
        #endregion
        #endregion

        #region 对外开放权限生成(应用方法)

        private void UpdateAuth(string objectID, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            org.NullCheck("org");
            Enum.GetValues(typeof(RecordType)).NotExists<int>(a => { return a == (int)recordType; }).TrueThrow("recordType Not Exist");
            switch (recordType)
            {
                //case RecordType.PotentialCustomer:
                case RecordType.Customer:
                    GetCustomerRelationAuthorizationAdaper().DeleteRelationAuthorizationsBase(keyID, recordType, relationType);
                    GetCustomerRelationAuthorizationAdaper().UpdateRelationAuthorizations(new CustomerRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetCustomerOrgAuthorizationAdapter().DeleteOrgAuthorizations(keyID, recordType, relationType);
                    GetCustomerOrgAuthorizationAdapter().UpdateOrgAuthorizations(keyID, org, recordType, relationType);
                    break;
                case RecordType.Assign:
                    GetCourseRelationAuthorizationAdpter().DeleteRelationAuthorizationsBase(keyID, recordType, relationType);
                    GetCourseRelationAuthorizationAdpter().UpdateRelationAuthorizations(new CourseRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetCourseOrgAuthorizationAdapter().DeleteOrgAuthorizations(keyID, recordType, relationType);
                    GetCourseOrgAuthorizationAdapter().UpdateOrgAuthorizations(keyID, org, recordType, relationType);
                    break;
                default:
                    GetOwnerRelationAuthorizationAdapter().DeleteRelationAuthorizationsBase(keyID, recordType, relationType);
                    GetOwnerRelationAuthorizationAdapter().UpdateRelationAuthorizations(new OwnerRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetRecordOrgAuthorizationAdapter().DeleteOrgAuthorizations(keyID, recordType, relationType);
                    GetRecordOrgAuthorizationAdapter().UpdateOrgAuthorizations(keyID, org, recordType, relationType);
                    break;
            }
        }

        public void InitAuth(IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            org.NullCheck("org");
            Enum.GetValues(typeof(RecordType)).NotExists<int>(a => { return a == (int)recordType; }).TrueThrow("recordType Not Exist");
            switch (recordType)
            {
                //case RecordType.PotentialCustomer:
                case RecordType.Customer:
                    GetCustomerRelationAuthorizationAdaper().DeleteRelationAuthorizations(keyID, recordType, relationType);
                    GetCustomerOrgAuthorizationAdapter().UpdateOrgAuthorizations(keyID, org, recordType, relationType);
                    break;
                case RecordType.Assign:
                    GetCourseRelationAuthorizationAdpter().DeleteRelationAuthorizations(keyID, recordType, relationType);
                    GetCourseOrgAuthorizationAdapter().UpdateOrgAuthorizations(keyID, org, recordType, relationType);
                    break;
                default:
                    GetOwnerRelationAuthorizationAdapter().DeleteRelationAuthorizations(keyID, recordType, relationType);
                    GetRecordOrgAuthorizationAdapter().UpdateOrgAuthorizations(keyID, org, recordType, relationType);
                    break;
            }
        }

        private void UpdateAuthInContext(string objectID, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            org.NullCheck("org");
            Enum.GetValues(typeof(RecordType)).NotExists<int>(a => { return a == (int)recordType; }).TrueThrow("recordType Not Exist");
            switch (recordType)
            {
                //case RecordType.PotentialCustomer:
                case RecordType.Customer:
                    GetCustomerRelationAuthorizationAdaper().DeleteRelationAuthorizationsInContextBase(keyID, recordType, relationType);
                    GetCustomerRelationAuthorizationAdaper().UpdateRelationAuthorizationsInContext(new CustomerRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetCustomerOrgAuthorizationAdapter().DeleteOrgAuthorizationsInContext(keyID, recordType, relationType);
                    GetCustomerOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContext(keyID, org, recordType, relationType);
                    break;
                case RecordType.Assign:
                    GetCourseRelationAuthorizationAdpter().DeleteRelationAuthorizationsInContextBase(keyID, recordType, relationType);
                    GetCourseRelationAuthorizationAdpter().UpdateRelationAuthorizationsInContext(new CourseRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetCourseOrgAuthorizationAdapter().DeleteOrgAuthorizationsInContext(keyID, recordType, relationType);
                    GetCourseOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContext(keyID, org, recordType, relationType);
                    break;
                default:
                    GetOwnerRelationAuthorizationAdapter().DeleteRelationAuthorizationsInContextBase(keyID, recordType, relationType);
                    GetOwnerRelationAuthorizationAdapter().UpdateRelationAuthorizationsInContext(new OwnerRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetRecordOrgAuthorizationAdapter().DeleteOrgAuthorizationsInContext(keyID, recordType, relationType);
                    GetRecordOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContext(keyID, org, recordType, relationType);
                    break;
            }
        }

        public void InitAuthInContext(IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            org.NullCheck("org");
            Enum.GetValues(typeof(RecordType)).NotExists<int>(a => { return a == (int)recordType; }).TrueThrow("recordType Not Exist");
            switch (recordType)
            {
                //case RecordType.PotentialCustomer:
                case RecordType.Customer:
                    GetCustomerRelationAuthorizationAdaper().DeleteRelationAuthorizationsInContext(keyID, recordType, relationType);
                    GetCustomerOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContext(keyID, org, recordType, relationType);
                    break;
                case RecordType.Assign:
                    GetCourseRelationAuthorizationAdpter().DeleteRelationAuthorizationsInContext(keyID, recordType, relationType);
                    GetCourseOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContext(keyID, org, recordType, relationType);
                    break;
                default:
                    GetOwnerRelationAuthorizationAdapter().DeleteRelationAuthorizationsInContext(keyID, recordType, relationType);
                    GetRecordOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContext(keyID, org, recordType, relationType);
                    break;
            }
        }

        public void UpdateAuth(PPTSJob job, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            job.NullCheck("job");
            UpdateAuthInContext(job.ID, org, keyID, recordType, relationType);
            this.SQLExecute();
        }

        public void UpdateAuth(PPTSJob job, IOrganization org, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            UpdateAuth(job, org, keyID, recordType, relationType);
        }

        public void InitAuth(IOrganization org, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            InitAuthInContext(org, keyID, recordType, relationType);
            this.SQLExecute();
        }

        public void UpdateAuth(IUser staff, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            staff.NullCheck("staff");
            UpdateAuthInContext(staff.ID, org, keyID, recordType, relationType);
            this.SQLExecute();
        }

        public void UpdateAuth(IUser staff, IOrganization org, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            UpdateAuth(staff, org, keyID, recordType, relationType);
        }

        public void UpdateAuthInContext(PPTSJob job, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            job.NullCheck("job");
            UpdateAuthInContext(job.ID, org, keyID, recordType, relationType);
        }

        public void UpdateAuthInContext(IUser staff, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            staff.NullCheck("staff");
            UpdateAuthInContext(staff.ID, org, keyID, recordType, relationType);
        }

        public void UpdateAuthInContext(PPTSJob job, IOrganization org, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            UpdateAuthInContext(job, org, keyID, recordType, relationType);
        }

        public void UpdateAuthInContext(IUser staff, IOrganization org, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            UpdateAuthInContext(staff, org, keyID, recordType, relationType);
        }

        public void InitAuthInContext(IOrganization org, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            InitAuthInContext(org, keyID, recordType, relationType);
        }

        private RecordType GetRecordType()
        {
            RecordType resultvalue = 0;
            EntityAuthAttribute attribute = GetCurrentEntityAuthAttribute();
            attribute.IsNotNull((att) => resultvalue = att.RecordType);
            return resultvalue;
        }

        private EntityAuthAttribute GetCurrentEntityAuthAttribute()
        {
            return (EntityAuthAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(EntityAuthAttribute));
        }
        #endregion

        #region 对外开放权限教师批量生成(应用方法)
        private void DeleteAuthByCollectionInContext(string keyID, RecordType recordType, RelationType relationType)
        {
            keyID.NullCheck("keyID");
            Enum.GetValues(typeof(RecordType)).NotExists<int>(a => { return a == (int)recordType; }).TrueThrow("recordType Not Exist");
            switch (recordType)
            {
                //case RecordType.PotentialCustomer:
                case RecordType.Customer:
                    GetCustomerRelationAuthorizationAdaper().DeleteRelationAuthorizationsInContextBase(keyID, recordType, relationType);
                    GetCustomerOrgAuthorizationAdapter().DeleteOrgAuthorizationsInContext(keyID, recordType, relationType);
                    break;
                case RecordType.Assign:
                    GetCourseRelationAuthorizationAdpter().DeleteRelationAuthorizationsInContextBase(keyID, recordType, relationType);
                    GetCourseOrgAuthorizationAdapter().DeleteOrgAuthorizationsInContext(keyID, recordType, relationType);
                    break;
                default:
                    GetOwnerRelationAuthorizationAdapter().DeleteRelationAuthorizationsInContextBase(keyID, recordType, relationType);
                    GetRecordOrgAuthorizationAdapter().DeleteOrgAuthorizationsInContext(keyID, recordType, relationType);
                    break;
            }
        }

        private void UpdateAuthByCollectionInContext(string objectID, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            org.NullCheck("org");
            Enum.GetValues(typeof(RecordType)).NotExists<int>(a => { return a == (int)recordType; }).TrueThrow("recordType Not Exist");
            switch (recordType)
            {
                //case RecordType.PotentialCustomer:
                case RecordType.Customer:
                    GetCustomerRelationAuthorizationAdaper().UpdateInContext(new CustomerRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetCustomerOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContextBase(keyID, org, recordType, relationType);
                    break;
                case RecordType.Assign:
                    GetCourseRelationAuthorizationAdpter().UpdateInContext(new CourseRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetCourseOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContextBase(keyID, org, recordType, relationType);
                    break;
                default:
                    GetOwnerRelationAuthorizationAdapter().UpdateInContext(new OwnerRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetRecordOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContextBase(keyID, org, recordType, relationType);
                    break;
            }
        }

        private void CopyTaskByCollectionInContext(string keyID, RecordType recordType, RelationType relationType)
        {
            keyID.NullCheck("keyID");
            Enum.GetValues(typeof(RecordType)).NotExists<int>(a => { return a == (int)recordType; }).TrueThrow("recordType Not Exist");
            switch (recordType)
            {
                //case RecordType.PotentialCustomer:
                case RecordType.Customer:
                    GetCustomerRelationAuthorizationAdaper().CreateCopyTaskInContext(keyID, recordType, relationType);
                    GetCustomerOrgAuthorizationAdapter().CreateCopyTaskInContext(keyID, recordType, relationType);
                    break;
                case RecordType.Assign:
                    GetCourseRelationAuthorizationAdpter().CreateCopyTaskInContext(keyID, recordType, relationType);
                    GetCourseOrgAuthorizationAdapter().CreateCopyTaskInContext(keyID, recordType, relationType);
                    break;
                default:
                    GetOwnerRelationAuthorizationAdapter().CreateCopyTaskInContext(keyID, recordType, relationType);
                    GetRecordOrgAuthorizationAdapter().CreateCopyTaskInContext(keyID, recordType, relationType);
                    break;
            }
        }

        public void UpdateAuthByJobCollectionInContext(List<string> jobIDs, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            jobIDs.NullCheck("jobIDs");
            PPTSJobCollection jobs = new PPTSJobCollection();
            jobs = OGUExtensions.GetPPTSJobByJobIDs(jobIDs.ToArray());

            DeleteAuthByCollectionInContext(keyID, recordType, relationType);
            jobs.ForEach(job => UpdateAuthByCollectionInContext(job.ID, job.Organization(), keyID, recordType, relationType));
            CopyTaskByCollectionInContext(keyID, recordType, relationType);
        }

        public void UpdateAuthByJobCollectionInContext(List<string> jobIDs, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            UpdateAuthByJobCollectionInContext(jobIDs, keyID, recordType, relationType);

        }

        public void UpdateAuthByJobCollection(List<string> jobIDs, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            UpdateAuthByJobCollectionInContext(jobIDs, keyID, recordType, relationType);
            this.SQLExecute();
        }

        public void UpdateAuthByJobCollection(List<string> jobIDs, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            UpdateAuthByJobCollection(jobIDs, keyID, recordType, relationType);
        }

        #endregion 

        #region 机构数据范围获取
        /// <summary>
        /// 获得可看到的有效机构最高点
        /// </summary>
        /// <param name="job">岗位</param>
        /// <param name="validFunctions">岗位权限点</param>
        /// <returns></returns>
        public IOrganization GetFirstAuthorizationOrg(PPTSJob job, List<string> validFunctions)
        {
            string jobID = job.ID;
            IOrganization org = job.Organization();
            IOrganization result = null;
            job.NullCheck("job");
            job.ID.NullCheck("jobID");
            org.NullCheck("");
            //记录机构权限
            List<ScopeBaseAttribute> attributes = GetCurrentScopeBaseAttribute();
            List<ScopeAttributeModel> models = InitScopeAttributeModelCollection(attributes.Where(model => model is RecordOrgScopeAttribute));
            if (models.Exists(a => ((RecordOrgScopeAttribute)a.ScopeAttribute).OrgType == OrgType.HQ && HasFunction(validFunctions, a.Functions)))
                result = org.GetParentOrganizationByType(DepartmentType.HQ) ?? org.GetUpperDataScope();
            else if (models.Exists(a => ((RecordOrgScopeAttribute)a.ScopeAttribute).OrgType == OrgType.Region && HasFunction(validFunctions, a.Functions)))
                result = org.GetParentOrganizationByType(DepartmentType.Region) ?? org.GetUpperDataScope();
            else if (models.Exists(a => ((RecordOrgScopeAttribute)a.ScopeAttribute).OrgType == OrgType.Branch && HasFunction(validFunctions, a.Functions)))
                result = org.GetParentOrganizationByType(DepartmentType.Branch) ?? org.GetUpperDataScope();
            else if (models.Exists(a => ((RecordOrgScopeAttribute)a.ScopeAttribute).OrgType == OrgType.Campus && HasFunction(validFunctions, a.Functions)))
                result = org.GetParentOrganizationByType(DepartmentType.Branch) ?? org.GetUpperDataScope();
            else
                result = org.GetUpperDataScope();
            return result;
        }

        /// <summary>
        /// 获得当前岗位可看到的有效机构最高点
        /// </summary>
        /// <returns></returns>
        public IOrganization GetFirstAuthorizationOrg(List<string> validFunctions)
        {
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();
            job.NullCheck("CurrentJob");
            return GetFirstAuthorizationOrg(job, validFunctions);
        }
        #endregion

        #region 主键名称，客户外键名称获取
        private string RecordKeyColumnName;
        private string CustomerIDColumnName;

        private void InitColumenName()
        {
            RecordKeyColumnName = null;
            CustomerIDColumnName = null;
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo p in properties)
            {
                KeyFieldMappingAttribute keyFieldMapping = AttributeHelper.GetCustomAttribute<KeyFieldMappingAttribute>(p);
                keyFieldMapping.IsNotNull(action => RecordKeyColumnName = (action as KeyFieldMappingAttribute).Name);
                CustomerFieldMappingAttribute customerFieldMapping = AttributeHelper.GetCustomAttribute<CustomerFieldMappingAttribute>(p);
                customerFieldMapping.IsNotNull(action => CustomerIDColumnName = (action as CustomerFieldMappingAttribute).Name);
                if (RecordKeyColumnName.IsNotEmpty() && CustomerIDColumnName.IsNotEmpty())
                    break;
            }
        }
        #endregion

        #region 规则命中
        private static bool IsFullOrgAuth(IEnumerable<string> jobFunctions, IEnumerable<ScopeAttributeModel> hqattributes)
        {
            bool result = false;
            //if (Configuration.ScopeAuthSettingsSection.GetConfig().Eabled)
            if(RolesDefineConfig.GetConfig().Enabled)
            {
                //定位命中规则
                result = hqattributes.Where(model => model.ScopeAttribute is RecordOrgScopeAttribute).Exists(att => HasFunction(jobFunctions, att.Functions));
            }
            else
                result = true;
            return result;
        }

        private static bool HasFunction(IEnumerable<string> jobFunctions, IEnumerable<string> scopeFunctions)
        {
            bool result = false;
            result = (from a in jobFunctions join b in scopeFunctions on a equals b select a).ToList().Count > 0;
            return result;
        }
        #endregion

        #region 属性获取方法
        private List<ScopeBaseAttribute> GetCurrentScopeBaseAttribute()
        {
            Type entityType = typeof(T);
            List<ScopeBaseAttribute> attributes = ((ScopeBaseAttribute[])Attribute.GetCustomAttributes(entityType, typeof(ScopeBaseAttribute), true)).ToList();
            return attributes;
        }
        #endregion

        protected virtual string GetConnectionName()
        {
            string result = this.connectionName;
            if (result.IsNullOrEmpty())
                result = ConnectionDefine.PPTSMetaDataConnectionName;
            return result;
        }

        protected virtual void SQLExecute()
        {
            using (DbContext db = DbContext.GetContext(this.GetConnectionName()))
            {
                db.ExecuteNonQuerySqlInContext();
            }
        }
    }
}
