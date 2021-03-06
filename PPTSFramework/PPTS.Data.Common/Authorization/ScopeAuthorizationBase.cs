﻿using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;
using System.Data;
using System.Reflection;
using MCS.Library.Principal;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 授权校验体系
    /// </summary>
    public abstract class ScopeAuthorizationBase<T>
    {
        protected abstract string GetConnectionName();

        #region Adpter集合
        /// <summary>
        /// 客户关系操作权限Adapter
        /// </summary>
        /// <returns></returns>
        public abstract CustomerRelationAuthorizationAdaperBase GetCustomerRelationAuthorizationAdaper();

        /// <summary>
        /// 客户机构操作权限Adapter
        /// </summary>
        /// <returns></returns>
        public abstract CustomerOrgAuthorizationAdapterBase GetCustomerOrgAuthorizationAdapter();

        /// <summary>
        /// 所有者操作权限Adpter
        /// </summary>
        /// <returns></returns>
        public abstract OwnerRelationAuthorizationAdapterBase GetOwnerRelationAuthorizationAdapter();

        /// <summary>
        /// 记录操作权限Adpter
        /// </summary>
        /// <returns></returns>
        public abstract RecordOrgAuthorizationAdapterBase GetRecordOrgAuthorizationAdapter();

        /// <summary>
        /// 课时操作权限Adpter
        /// </summary>
        /// <returns></returns>
        public abstract CourseOrgAuthorizationAdapterBase GetCourseOrgAuthorizationAdapter();

        /// <summary>
        /// 课时所有者操作权限Adpter
        /// </summary>
        /// <returns></returns>
        public abstract CourseRelationAuthorizationAdpterBase GetCourseRelationAuthorizationAdpter();
        #endregion

        #region 权限Builder拼接查询条件
        private WhereSqlClauseBuilder BuilderRelationExistsWhereBuilder(string objectID,
            string owenerColumnName, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("OwnerID", owenerColumnName, "=", true)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuilderRelationExistsWhereBuilder(string objectID, RelationType objectType,
            string owenerColumnName, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("ObjectType", (int)objectType)
                .AppendItem("OwnerID", owenerColumnName, "=", true)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuilderRelationWhereBuilder(string objectID,
            string ownerID, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
               .AppendItem("OwnerID", ownerID)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuilderRelationWhereBuilder(string objectID, RelationType objectType,
            string ownerID, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("ObjectType", (int)objectType)
                .AppendItem("OwnerID", ownerID)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuilderOrgExistsWhereBuilder(string objectID, OrgType objectType, RelationType relationType,
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

        private WhereSqlClauseBuilder BuilderOrgWhereBuilder(string objectID, OrgType objectType, RelationType relationType,
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

        private WhereSqlClauseBuilder BuilderOrgExistsWhereBuilder(string objectID, OrgType objectType,
            string owenerColumnName, RecordType ownerType)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", objectID)
                .AppendItem("ObjectType", (int)objectType)
                .AppendItem("OwnerID", owenerColumnName, "=", true)
                .AppendItem("OwnerType", (int)ownerType);
            return whereBuilder;
        }

        private WhereSqlClauseBuilder BuilderOrgWhereBuilder(string objectID, OrgType objectType,
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
            CustomerRelationAuthorizationAdaperBase adapter = GetCustomerRelationAuthorizationAdaper();
            string sql = BuilderExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder CustomerOrgWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            CustomerOrgAuthorizationAdapterBase adapter = GetCustomerOrgAuthorizationAdapter();
            string sql = BuilderExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder OwnerRelationWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            OwnerRelationAuthorizationAdapterBase adapter = GetOwnerRelationAuthorizationAdapter();
            string sql = BuilderExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder RecordOrgWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            RecordOrgAuthorizationAdapterBase adapter = GetRecordOrgAuthorizationAdapter();
            string sql = BuilderExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder CourseOrgWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            CourseOrgAuthorizationAdapterBase adapter = GetCourseOrgAuthorizationAdapter();
            string sql = BuilderExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        private WhereSqlClauseBuilder CourseRelationWhereBuilder(WhereSqlClauseBuilder requestBuilder)
        {
            WhereSqlClauseBuilder result = new WhereSqlClauseBuilder();
            CourseRelationAuthorizationAdpterBase adapter = GetCourseRelationAuthorizationAdpter();
            string sql = BuilderExistsSQL(adapter.GetMappingInfo().GetQueryTableName()
                , requestBuilder.ToSqlString(TSqlBuilder.Instance));
            return result.AppendItem(null, sql, null, true);
        }

        /// <summary>
        /// 构建单个查询条件
        /// </summary>
        /// <param name="queryTableName">查询数据表</param>
        /// <param name="whereBuilder">查询条件</param>
        /// <returns></returns>
        private string BuilderExistsSQL(string queryTableName, string whereBuilder)
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
                    sqlClause.Add(CustomerRelationWhereBuilder(BuilderRelationExistsWhereBuilder(jobID
                   , action.RelationType
                   , ownerColumnName
                   , (RecordType)action.RecordType)));
                }
                else
                {
                    sqlClause.Add(CustomerRelationWhereBuilder(BuilderRelationExistsWhereBuilder(jobID
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
                    sqlClause.Add(CustomerRelationWhereBuilder(BuilderRelationWhereBuilder(jobID
                   , action.RelationType
                   , ownerID
                   , (RecordType)action.RecordType)));
                }
                else
                {
                    sqlClause.Add(CustomerRelationWhereBuilder(BuilderRelationWhereBuilder(jobID
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
            List<RecordOrgScopeAttribute> existCustomerAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Customer || att.RecordType == RecordType.PotentialCustomer) && HasFunction(jobFunctions, att)).ToList();
            List<RecordOrgScopeAttribute> existCourseAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Assign) && HasFunction(jobFunctions, att)).ToList();
            List<RecordOrgScopeAttribute> existRecordAttributes = existAttributes.Where(att => (att.RecordType != RecordType.Customer && att.RecordType != RecordType.PotentialCustomer && att.RecordType != RecordType.Assign) && HasFunction(jobFunctions, att)).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            existCustomerAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgExistsSqlClauseBuilder(org, ownerColumnName, attribute, CustomerOrgWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            existCourseAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgExistsSqlClauseBuilder(org, ownerColumnName, attribute, CourseOrgWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            existRecordAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgExistsSqlClauseBuilder(org, ownerColumnName, attribute, RecordOrgWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection OrgAuthSelectBuilder(IOrganization org, string ownerID, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<RecordOrgScopeAttribute> existAttributes = attributeModels.Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as RecordOrgScopeAttribute).ToList();
            List<RecordOrgScopeAttribute> existCustomerAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Customer || att.RecordType == RecordType.PotentialCustomer)).ToList();
            List<RecordOrgScopeAttribute> existCourseAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Assign)).ToList();
            List<RecordOrgScopeAttribute> existRecordAttributes = existAttributes.Where(att => (att.RecordType != RecordType.Customer && att.RecordType != RecordType.PotentialCustomer && att.RecordType != RecordType.Assign)).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            existCustomerAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgSelectSqlClauseBuilder(org, ownerID, attribute, CustomerOrgWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            existCourseAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgSelectSqlClauseBuilder(org, ownerID, attribute, CourseOrgWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            existRecordAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OrgSelectSqlClauseBuilder(org, ownerID, attribute, RecordOrgWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
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

            List<OwnerRelationScopeAttribute> existCustomerAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Customer || att.RecordType == RecordType.PotentialCustomer)).ToList();
            List<OwnerRelationScopeAttribute> existCourseAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Assign)).ToList();
            List<OwnerRelationScopeAttribute> existRecordAttributes = existAttributes.Where(att => (att.RecordType != RecordType.Customer && att.RecordType != RecordType.PotentialCustomer && att.RecordType != RecordType.Assign)).ToList();


            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            existCustomerAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OwnerRelationExistsSqlClauseBuilder(jobID, ownerColumnName, attribute, CustomerRelationWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            existCourseAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OwnerRelationExistsSqlClauseBuilder(jobID, ownerColumnName, attribute, CourseRelationWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            existRecordAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OwnerRelationExistsSqlClauseBuilder(jobID, ownerColumnName, attribute, OwnerRelationWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection OwnerRelationAuthSelectBuilder(string jobID, string ownerID, List<string> jobFunctions, List<ScopeAttributeModel> attributeModels)
        {
            //定位命中规则
            List<OwnerRelationScopeAttribute> existAttributes = attributeModels
                .Where(att => HasFunction(jobFunctions, att.Functions.ToList()))
                .Select(model => model.ScopeAttribute as OwnerRelationScopeAttribute).ToList();

            List<OwnerRelationScopeAttribute> existCustomerAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Customer || att.RecordType == RecordType.PotentialCustomer)).ToList();
            List<OwnerRelationScopeAttribute> existCourseAttributes = existAttributes.Where(att => (att.RecordType == RecordType.Assign)).ToList();
            List<OwnerRelationScopeAttribute> existRecordAttributes = existAttributes.Where(att => (att.RecordType != RecordType.Customer && att.RecordType != RecordType.PotentialCustomer && att.RecordType != RecordType.Assign)).ToList();

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;

            existCustomerAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OwnerRelationSelectSqlClauseBuilder(jobID, ownerID, attribute, CustomerRelationWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            existCourseAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OwnerRelationSelectSqlClauseBuilder(jobID, ownerID, attribute, CourseRelationWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            existRecordAttributes.ForEach(attribute =>
            {
                WhereSqlClauseBuilder builder = OwnerRelationSelectSqlClauseBuilder(jobID, ownerID, attribute, OwnerRelationWhereBuilder);
                builder.IsNotNull(action => sqlClause.Add(action));
            });
            return sqlClause;
        }

        private bool HasFunction(List<string> jobFunctions, List<string> scopeFunctions)
        {
            bool result = false;
            result = (from a in jobFunctions join b in scopeFunctions on a equals b select a).ToList().Count > 0;
            return result;
        }

        private WhereSqlClauseBuilder OrgExistsSqlClauseBuilder(IOrganization org
            , string ownerColumnName
            , RecordOrgScopeAttribute attribute
            , Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun
            )
        {
            IOrganization parentOrg = org.GetParentOrganizationByType((DepartmentType)Enum.Parse(typeof(DepartmentType), ((int)attribute.OrgType).ToString()));
            if (parentOrg == null) return null;
            if (Enum.GetValues(typeof(RelationType)).Exists<int>((a) => { return (a == (int)attribute.RelationType); }))
                return fun(BuilderOrgExistsWhereBuilder(parentOrg.ID, attribute.OrgType, attribute.RelationType, ownerColumnName, attribute.RecordType));
            else
                return fun(BuilderOrgExistsWhereBuilder(parentOrg.ID, attribute.OrgType, ownerColumnName, attribute.RecordType));
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
                return fun(BuilderOrgWhereBuilder(parentOrg.ID, attribute.OrgType, attribute.RelationType, ownerID, attribute.RecordType));
            else
                return fun(BuilderOrgWhereBuilder(parentOrg.ID, attribute.OrgType, ownerID, attribute.RecordType));
        }

        private WhereSqlClauseBuilder OwnerRelationExistsSqlClauseBuilder(string jobID
            , string ownerColumnName
            , OwnerRelationScopeAttribute attribute
            , Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun
            )
        {
            if (Enum.GetValues(typeof(RelationType)).Exists<int>((a) => { return (a == (int)attribute.RelationType); }))
                return fun(BuilderRelationExistsWhereBuilder(jobID, attribute.RelationType, ownerColumnName, attribute.RecordType));
            else
                return fun(BuilderRelationExistsWhereBuilder(jobID, RelationType.Owner, ownerColumnName, attribute.RecordType));
        }

        private WhereSqlClauseBuilder OwnerRelationSelectSqlClauseBuilder(string jobID
            , string ownerID
            , OwnerRelationScopeAttribute attribute
            , Func<WhereSqlClauseBuilder, WhereSqlClauseBuilder> fun
            )
        {
            if (Enum.GetValues(typeof(RelationType)).Exists<int>((a) => { return (a == (int)attribute.RelationType); }))
                return fun(BuilderRelationWhereBuilder(jobID, attribute.RelationType, ownerID, attribute.RecordType));
            else
                return fun(BuilderRelationWhereBuilder(jobID, RelationType.Owner, ownerID, attribute.RecordType));
        }

        private bool IsFullOrgAuth(List<string> jobFunctions, List<ScopeAttributeModel> hqattributes)
        {
            //定位命中规则
            List<RecordOrgScopeAttribute> existAttributes = hqattributes.Select(model => model.ScopeAttribute as RecordOrgScopeAttribute)
                .Where(att => HasFunction(jobFunctions, att)).ToList();
            return existAttributes.Count > 0;
        }
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
            //((RecordOrgScopeAttribute)action.ScopeAttribute).RelationType==0
            && ((RecordOrgScopeAttribute)action.ScopeAttribute).OrgType == OrgType.HQ).ToList()))//总公司权限
            {
                sqlChildClause = CustomerRelationAuthExistsBuilder(jobID, recordCusomerIDColumnName, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(CustomerRelationScopeAttribute)).ToList());
                sqlChildClause.IsNotNull(action => sqlClause.Add(action));

                sqlChildClause = OrgAuthExistsBuilder(org, recordKeyColumnName, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(RecordOrgScopeAttribute)).ToList());
                sqlChildClause.IsNotNull(action => sqlClause.Add(action));

                sqlChildClause = OrgCustomerRelationAuthExistsBuilder(org, jobID, recordCusomerIDColumnName, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(OrgCustomerRelationScopeAttribute)).ToList());
                sqlChildClause.IsNotNull(action => sqlClause.Add(action));

                sqlChildClause = OwnerRelationAuthExistsBuilder(jobID, recordKeyColumnName, jobFunctions
                                    , models.Where(action => action.ScopeAttributeType == typeof(OwnerRelationScopeAttribute)).ToList());
                sqlChildClause.IsNotNull(action => sqlClause.Add(action));
                sqlChildClause.IsNull(() => new WhereSqlClauseBuilder().AppendItem(null, "1=0", null, true));
            }
            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection AuthSelectBuilder(string jobID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> jobFunctions, List<ScopeBaseAttribute> attributes)
        {
            List<ScopeAttributeModel> models = InitScopeAttributeModelCollection(attributes);

            ConnectiveSqlClauseCollection sqlClause = new ConnectiveSqlClauseCollection();
            sqlClause.LogicOperator = LogicOperatorDefine.Or;
            ConnectiveSqlClauseCollection sqlChildClause;

            if (!IsFullOrgAuth(jobFunctions, models.Where(action => action.ScopeAttributeType == typeof(RecordOrgScopeAttribute)
            && Enum.GetValues(typeof(RelationType)).NotExists<int>((a) => { return (a == (int)((RecordOrgScopeAttribute)action.ScopeAttribute).RelationType); })
            //((RecordOrgScopeAttribute)action.ScopeAttribute).RelationType==0
            && ((RecordOrgScopeAttribute)action.ScopeAttribute).OrgType == OrgType.HQ).ToList()))//总公司权限
            {
                sqlChildClause = CustomerRelationAuthSelectBuilder(jobID, recordKeyID, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(CustomerRelationScopeAttribute)).ToList());
                sqlChildClause.IsNotNull(action => sqlClause.Add(action));
                sqlChildClause = OrgAuthSelectBuilder(org, recordCusomerID, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(RecordOrgScopeAttribute)).ToList());
                sqlChildClause.IsNotNull(action => sqlClause.Add(action));
                sqlChildClause = OrgCustomerRelationAuthSelectBuilder(org, jobID, recordCusomerID, jobFunctions
                    , models.Where(action => action.ScopeAttributeType == typeof(OrgCustomerRelationScopeAttribute)).ToList());
                sqlChildClause.IsNotNull(action => sqlClause.Add(action));
                sqlChildClause = OwnerRelationAuthSelectBuilder(jobID, recordKeyID, jobFunctions
                                    , models.Where(action => action.ScopeAttributeType == typeof(OwnerRelationScopeAttribute)).ToList());
                sqlChildClause.IsNotNull(action => sqlClause.Add(action));
                sqlChildClause.IsNull(() => sqlClause.Add(new WhereSqlClauseBuilder().AppendItem(null, "1 = 0", null, true)));
            }
            return sqlClause;
        }

        protected ConnectiveSqlClauseCollection AuthExistsBuilder(string jobID, IOrganization org, string recordKeyColumnName, string recordCusomerIDColumnName, List<string> jobFunctions, ActionType type)
        {
            Type entityType = typeof(T);

            List<ScopeBaseAttribute> classAttributes = ((ScopeBaseAttribute[])Attribute.GetCustomAttributes(entityType, typeof(ScopeBaseAttribute), true)).ToList();

            return AuthExistsBuilder(jobID, org, recordKeyColumnName, recordCusomerIDColumnName, jobFunctions, classAttributes.Where(action => action.ActionType == type).ToList());

        }

        protected ConnectiveSqlClauseCollection AuthSelectBuilder(string jobID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> jobFunctions, ActionType type)
        {
            Type entityType = typeof(T);
            List<ScopeBaseAttribute> classAttributes = ((ScopeBaseAttribute[])Attribute.GetCustomAttributes(entityType, typeof(ScopeBaseAttribute), true)).ToList();
            return AuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, jobFunctions, classAttributes.Where(action => action.ActionType == type).ToList());
        }

        protected string AuthSelectBuiderSQL(string jobID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> jobFunctions, ActionType type)
        {
            ConnectiveSqlClauseCollection sqlClause = AuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, jobFunctions, type);
            string sqlTemplate = @"if({0}) begin select 1 end;";
            return string.Format(sqlTemplate, sqlClause.ToSqlString(TSqlBuilder.Instance));
        }

        protected bool HasAuth(string jobID, IOrganization org, string recordKeyID, string recordCusomerID, List<string> jobFunctions, ActionType type)
        {
            bool result = false;
            string sql = AuthSelectBuiderSQL(jobID, org, recordKeyID, recordCusomerID, jobFunctions, type);
            if (sql.IsNotEmpty())
            {
                DataSet ds = MCS.Library.Data.Adapters.DbHelper.RunSqlReturnDS(sql, this.GetConnectionName());
                result = ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
            }
            return result;
        }

        private List<ScopeAttributeModel> InitScopeAttributeModelCollection(List<ScopeBaseAttribute> attributes)
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
        #region 列表过滤权限体
        public ConnectiveSqlClauseCollection ReadAuthExistsBuider(string objectID, IOrganization org, string recordKeyColumnName, string recordCusomerIDColumnName, List<string> jobFunctions, object conditionObject = null)
        {
            return ConnectiveUserCondition(AuthExistsBuilder(objectID, org, recordKeyColumnName, recordCusomerIDColumnName, jobFunctions, ActionType.Read), conditionObject);
        }

        /// <summary>
        /// 读取权限获取规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyColumnName">主键字段名</param>
        /// <param name="recordCusomerIDColumnName">关联客户字段名</param>
        /// <param name="jobFunctions">验证权限点集合</param>
        /// <returns>表达式集合</returns>
        public ConnectiveSqlClauseCollection ReadAuthExistsBuider(PPTSJob job, string recordKeyColumnName, string recordCusomerIDColumnName, List<string> jobFunctions, object conditionObject = null)
        {
            StringBuilder sql = new StringBuilder();
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");
            return ReadAuthExistsBuider(jobID, org, recordKeyColumnName, recordCusomerIDColumnName, jobFunctions, conditionObject);
        }


        /// <summary>
        /// 写权限获取规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyColumnName">主键字段名</param>
        /// <param name="recordCusomerIDColumnName">关联客户字段名</param>
        /// <param name="jobFunctions">验证权限点集合</param>
        /// <returns>表达式集合</returns>
        protected ConnectiveSqlClauseCollection WriteAuthExistBuider(PPTSJob job, string recordKeyColumnName, string recordCusomerIDColumnName, List<string> jobFunctions, object conditionObject = null)
        {
            StringBuilder sql = new StringBuilder();
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");
            return ConnectiveUserCondition(AuthExistsBuilder(jobID, org, recordKeyColumnName, recordCusomerIDColumnName, jobFunctions, ActionType.Write), conditionObject);
        }

        /// <summary>
        /// 读取权限获取规则(特殊情况,即验证的权限ID不是岗位ID，而是员工ID，例如：周反馈的个人权限过滤)
        /// </summary>
        /// <param name="objectID">对象ID</param>
        /// <param name="org">验证所属机构</param>
        /// <param name="recordKeyColumnName">验证主键字段</param>
        /// <param name="recordCusomerIDColumnName">关联客户字段名</param>
        /// <param name="jobFunctions">权限点集合</param>
        /// <param name="conditionObject">附加条件</param>
        /// <returns>表达式集合</returns>


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

        /// <summary>
        /// 读取权限获取规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="jobFunctions">验证权限点信息</param>
        /// <returns></returns>
        public ConnectiveSqlClauseCollection ReadAuthExistsBuider(PPTSJob job, List<string> jobFunctions, string aliasName = null, object conditionObject = null)
        {
            InitColumenName();
            aliasName.IsNotNull((name) =>
            {
                RecordKeyColumnName = string.Format("{0}.{1}", aliasName, RecordKeyColumnName);
                CustomerIDColumnName = string.Format("{0}.{1}", aliasName, CustomerIDColumnName);
            });
            return ReadAuthExistsBuider(job, RecordKeyColumnName, CustomerIDColumnName, jobFunctions, conditionObject);
        }

        /// <summary>
        ///  读取权限获取规则(特殊情况,即验证的权限ID不是岗位ID，而是员工ID，例如：周反馈的个人权限过滤)
        /// </summary>
        /// <param name="staff">员工信息</param>
        /// <param name="org">机构信息</param>
        /// <param name="jobFunctions">权限点信息</param>
        /// <param name="aliasName">别名</param>
        /// <param name="conditionObject">叠加条件</param>
        /// <returns></returns>
        public ConnectiveSqlClauseCollection ReadAuthExistsBuider(IUser staff, IOrganization org, List<string> jobFunctions, string aliasName = null, object conditionObject = null)
        {
            InitColumenName();
            aliasName.IsNotNull((name) =>
            {
                RecordKeyColumnName = string.Format("{0}.{1}", aliasName, RecordKeyColumnName);
                CustomerIDColumnName = string.Format("{0}.{1}", aliasName, CustomerIDColumnName);
            });
            staff.NullCheck("staff");
            staff.ID.CheckStringIsNullOrEmpty("StaffID");

            string jobID = staff.ID;

            return ReadAuthExistsBuider(jobID, org, RecordKeyColumnName, CustomerIDColumnName, jobFunctions, conditionObject);


        }

        protected ConnectiveSqlClauseCollection WriteAuthExistsBuider(PPTSJob job, List<string> jobFunctions, string aliasName = null, object conditionObject = null)
        {
            InitColumenName();
            aliasName.IsNotEmpty((name) =>
            {
                string.Format("{0}.{1}", aliasName, RecordKeyColumnName);
                string.Format("{0}.{1}", aliasName, CustomerIDColumnName);
            });
            return WriteAuthExistBuider(job, RecordKeyColumnName, CustomerIDColumnName, jobFunctions, conditionObject);
        }

        /// <summary>
        /// 当前岗位关联的权限绑定
        /// </summary>
        /// <returns></returns>
        public ConnectiveSqlClauseCollection ReadAuthExistsBuider(string aliasName = null, object conditionObject = null)
        {
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();
            job.NullCheck("CurrentJob");
            List<string> functions = new List<string>();
            job.Functions.IsNotNull((model) => functions = model.ToList());
            return ReadAuthExistsBuider(job, functions, aliasName, conditionObject);
        }

        protected ConnectiveSqlClauseCollection WriteAuthExistsBuider(string aliasName = null, object conditionObject = null)
        {
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();
            job.NullCheck("CurrentJob");
            List<string> functions = new List<string>();
            job.Functions.IsNotNull((model) => functions = model.ToList());
            return WriteAuthExistsBuider(job, functions, aliasName, conditionObject);
        }

        #endregion

        #region 单条校验权限体
        protected ConnectiveSqlClauseCollection ReadAuthSelectBuider(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> jobFunctions)
        {
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");

            return AuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, jobFunctions, ActionType.Read);
        }

        /// <summary>
        /// 验证单条记录是否有读取权限规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyID">主键字段ID</param>
        /// <param name="recordCusomerID">客户ID</param>
        /// <param name="jobFunctions">验证权限点集合</param>
        /// <returns></returns>
        public bool HasReadAuth(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> jobFunctions)
        {
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");

            return HasAuth(jobID, org, recordKeyID, recordCusomerID, jobFunctions, ActionType.Read);
        }

        /// <summary>
        /// 验证单条记录读取权限规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyID">主键字段ID</param>
        /// <param name="recordCusomerID">客户ID</param>
        /// <param name="jobFunctions">验证权限点集合</param>
        /// <returns></returns>
        public void CheckReadAuth(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> jobFunctions)
        {
            HasReadAuth(job, recordKeyID, recordCusomerID, jobFunctions).FalseThrow("岗位{0}缺少读取数据{1}的权限，类型依赖{2}", job.Name, recordKeyID, typeof(T).ToString());
        }


        /// <summary>
        /// 验证单条记录写权限规则表达式生成
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyColumnName">主键字段名</param>
        /// <param name="recordCusomerIDColumnName">关联客户字段名</param>
        /// <param name="jobFunctions">验证权限点集合</param>
        /// <returns>表达式集合</returns>
        protected ConnectiveSqlClauseCollection WriteAuthSelectBuider(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> jobFunctions)
        {
            StringBuilder sql = new StringBuilder();
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");

            return AuthSelectBuilder(jobID, org, recordKeyID, recordCusomerID, jobFunctions, ActionType.Write);
        }

        /// <summary>
        /// 验证单条记录是否有写入权限规则
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyID">主键字段ID</param>
        /// <param name="recordCusomerID">客户ID</param>
        /// <param name="jobFunctions">验证权限点集合</param>
        /// <returns></returns>
        public bool HasWriteAuth(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> jobFunctions)
        {
            StringBuilder sql = new StringBuilder();
            job.NullCheck("job");
            job.ID.CheckStringIsNullOrEmpty("jobID");

            string jobID = job.ID;

            IOrganization org = job.Organization();
            org.NullCheck("org");

            return HasAuth(jobID, org, recordKeyID, recordCusomerID, jobFunctions, ActionType.Write);
        }
        /// <summary>
        /// 验证单条记录写入权限
        /// </summary>
        /// <param name="job">岗位信息</param>
        /// <param name="recordKeyID">主键字段ID</param>
        /// <param name="recordCusomerID">客户ID</param>
        /// <param name="jobFunctions">验证权限点集合</param>
        /// <returns></returns>
        public void CheckWriteAuth(PPTSJob job, string recordKeyID, string recordCusomerID, List<string> jobFunctions)
        {
            HasWriteAuth(job, recordKeyID, recordCusomerID, jobFunctions).FalseThrow("岗位{0}缺少写入数据{1}的权限，类型依赖{2}", job.Name, recordKeyID, typeof(T).ToString());
        }

        /// <summary>
        /// 当前岗位对某条数据是否具有操作权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">客户ID</param>
        /// <returns></returns>
        public bool HasReadAuth(string recordKeyID, string recordCustomerID)
        {
            bool result = false;
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();
            job.NullCheck("CurrentJob");
            List<string> functions = new List<string>();
            job.Functions.IsNotNull((model) => functions = model.ToList());
            result = HasReadAuth(job, recordKeyID, recordCustomerID, functions);
            return result;
        }

        /// <summary>
        /// 当前岗位对某条数据是否具有操作权限
        /// </summary>
        /// <param name="recordKeyID"></param>
        /// <param name="recordCustomerID"></param>
        /// <returns></returns>
        public bool HasWriteAuth(string recordKeyID, string recordCustomerID)
        {
            bool result = false;
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();
            job.NullCheck("CurrentJob");
            List<string> functions = new List<string>();
            job.Functions.IsNotNull((model) => functions = model.ToList());
            result = HasWriteAuth(job, recordKeyID, recordCustomerID, functions);
            return result;
        }

        /// <summary>
        /// 检查当前岗位是否具有指定的主键ID及客户ID是否有读取权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">客户ID，如果没有可以提供null</param>
        public void CheckReadAuth(string recordKeyID, string recordCustomerID)
        {
            HasReadAuth(recordKeyID, recordCustomerID).FalseThrow("当前岗位缺少读取数据{0}的权限，类型依赖{1}", recordKeyID, typeof(T).ToString());
        }

        /// <summary>
        /// 检查当前岗位是否具有指定的主键ID及客户ID是否有写入权限
        /// </summary>
        /// <param name="recordKeyID">主键ID</param>
        /// <param name="recordCustomerID">客户ID，如果没有可以提供null</param>
        public void CheckWriteAuth(string recordKeyID, string recordCustomerID)
        {
            HasWriteAuth(recordKeyID, recordCustomerID).FalseThrow("当前岗位缺少写入数据{0}的权限，类型依赖{1}", recordKeyID, typeof(T).ToString());
        }
        #endregion
        #endregion

        #region 对外开放权限生成(应用方法)
        private void UpdateAuth(string objectID, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            objectID.NullCheck("objectID");
            org.NullCheck("org");
            Enum.GetValues(typeof(RecordType)).NotExists<int>(a => { return a == (int)recordType; }).TrueThrow("recordType Not Exist");
            switch (recordType)
            {
                case RecordType.PotentialCustomer:
                case RecordType.Customer:
                    GetCustomerRelationAuthorizationAdaper().Update(new CustomerRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetCustomerOrgAuthorizationAdapter().UpdateOrgAuthorizations(keyID, org, recordType, relationType);
                    break;
                case RecordType.Assign:
                    GetCourseRelationAuthorizationAdpter().Update(new CourseRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetCourseOrgAuthorizationAdapter().UpdateOrgAuthorizations(keyID, org, recordType, relationType);
                    break;
                default:
                    GetOwnerRelationAuthorizationAdapter().Update(new OwnerRelationAuthorization()
                    {
                        ObjectID = objectID,
                        ObjectType = relationType,
                        OrgID = org.ID,
                        OrgType = (OrgType)org.PPTSDepartmentType(),
                        OwnerID = keyID,
                        OwnerType = recordType
                    });
                    GetRecordOrgAuthorizationAdapter().UpdateOrgAuthorizations(keyID, org, recordType, relationType);
                    break;
            }
        }

        public void UpdateAuth(PPTSJob job, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            job.NullCheck("job");
            UpdateAuth(job.ID, org, keyID, recordType, relationType);
        }

        public void UpdateAuth(PPTSJob job, IOrganization org, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            UpdateAuth(job, org, keyID, recordType, relationType);
        }

        public void UpdateAuth(IUser staff, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            staff.NullCheck("staff");
            UpdateAuth(staff.ID, org, keyID, recordType, relationType);
        }

        public void UpdateAuth(IUser staff, IOrganization org, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            UpdateAuth(staff, org, keyID, recordType, relationType);
        }

        private void UpdateAuthInContext(string objectID, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            objectID.NullCheck("objectID");
            org.NullCheck("org");
            Enum.GetValues(typeof(RecordType)).NotExists<int>(a => { return a == (int)recordType; }).TrueThrow("recordType Not Exist");
            switch (recordType)
            {
                case RecordType.PotentialCustomer:
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
                    GetCustomerOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContext(keyID, org, recordType, relationType);
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
                    GetCourseOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContext(keyID, org, recordType, relationType);
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
                    GetRecordOrgAuthorizationAdapter().UpdateOrgAuthorizationsInContext(keyID, org, recordType, relationType);
                    break;
            }

        }

        public void UpdateAuthInContext(PPTSJob job, IOrganization org, string keyID, RecordType recordType, RelationType relationType = RelationType.Owner)
        {
            job.NullCheck("job");
            UpdateAuthInContext(job.ID, org, keyID, recordType, relationType);
        }

        public void UpdateAuthInContext(PPTSJob job, IOrganization org, string keyID, RelationType relationType = RelationType.Owner)
        {
            RecordType recordType = GetRecordType();
            UpdateAuthInContext(job, org, keyID, recordType, relationType);
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

        #region 机构数据范围获取
        /// <summary>
        /// 获得可看到的有效机构最高点
        /// </summary>
        /// <param name="job">岗位</param>
        /// <param name="jobFunctions">岗位权限点</param>
        /// <returns></returns>
        public IOrganization GetFirstAuthorizationOrg(PPTSJob job, List<string> jobFunctions)
        {
            StringBuilder sql = new StringBuilder();
            job.NullCheck("job");
            job.ID.NullCheck("jobID");
            string jobID = job.ID;
            IOrganization org = job.Organization();
            org.NullCheck("");
            //记录机构权限
            List<RecordOrgScopeAttribute> recordAttributes = new List<RecordOrgScopeAttribute>();
            Type entityType = typeof(T);
            List<ScopeBaseAttribute> attributes = ((ScopeBaseAttribute[])Attribute.GetCustomAttributes(entityType, typeof(ScopeBaseAttribute), true)).ToList();
            foreach (ScopeBaseAttribute attribute in attributes)
            {
                if (attribute is RecordOrgScopeAttribute)
                    recordAttributes.Add((RecordOrgScopeAttribute)attribute);
            }
            if (recordAttributes.Where(a => a.OrgType == OrgType.HQ && HasFunction(jobFunctions, a)).Count() > 0)
                return org.GetParentOrganizationByType(DepartmentType.HQ) ?? org.GetUpperDataScope();
            else if (recordAttributes.Where(a => a.OrgType == OrgType.Region && HasFunction(jobFunctions, a)).Count() > 0)
                return org.GetParentOrganizationByType(DepartmentType.Region) ?? org.GetUpperDataScope();
            else if (recordAttributes.Where(a => a.OrgType == OrgType.Branch && HasFunction(jobFunctions, a)).Count() > 0)
                return org.GetParentOrganizationByType(DepartmentType.Branch) ?? org.GetUpperDataScope();
            else if (recordAttributes.Where(a => a.OrgType == OrgType.Campus && HasFunction(jobFunctions, a)).Count() > 0)
                return org.GetParentOrganizationByType(DepartmentType.Campus) ?? org.GetUpperDataScope();
            else
                return org.GetUpperDataScope();
        }

        /// <summary>
        /// 获得当前岗位可看到的有效机构最高点
        /// </summary>
        /// <returns></returns>
        public IOrganization GetFirstAuthorizationOrg()
        {
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();
            job.NullCheck("CurrentJob");
            List<string> functions = new List<string>();
            job.Functions.IsNotNull((model) => functions = model.ToList());
            return GetFirstAuthorizationOrg(job, functions);
        }
        #endregion

        #region 主键名称，客户外键名称获取
        private string RecordKeyColumnName;
        private string CustomerIDColumnName;

        private void InitColumenName()
        {
            RecordKeyColumnName = null;
            CustomerIDColumnName = null;
            PropertyInfo[] propertys = typeof(T).GetProperties();
            foreach (PropertyInfo p in propertys)
            {
                Attribute att = p.GetCustomAttribute(typeof(KeyFiledMappingAttribute));
                att.IsNotNull(action => RecordKeyColumnName = (action as KeyFiledMappingAttribute).Name);
                att = p.GetCustomAttribute(typeof(CustomerFiledMappingAttribute));
                att.IsNotNull(action => CustomerIDColumnName = (action as CustomerFiledMappingAttribute).Name);
                if (RecordKeyColumnName.IsNotEmpty() && CustomerIDColumnName.IsNotEmpty())
                    break;
            }
        }
        #endregion

        #region 规则命中
        private bool HasFunction(List<string> jobFunctions, ScopeBaseAttribute attribute)
        {
            bool resultvalue = false;
            if (!attribute.Functions.IsNullOrEmpty())
            {
                List<string> functions = attribute.Functions.Split(',').ToList();
                foreach (string function in jobFunctions)
                {
                    resultvalue = functions.Contains(function);
                    if (resultvalue)
                        break;
                }
            }
            return resultvalue;
        }
        #endregion

    }
}
