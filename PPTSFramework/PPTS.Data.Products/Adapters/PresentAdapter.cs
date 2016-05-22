using MCS.Library.Data;
using PPTS.Data.Products.Entities;
using System.Collections.Generic;
using System.Linq;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using System;
using MCS.Library.Data.Builder;

namespace PPTS.Data.Products.Adapters
{
    /// <summary>
    /// 买赠 相关的Adapter的基类
    /// </summary>
    public class PresentAdapter : UpdatableAndLoadableAdapterBase<Present, PresentCollection> 
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }

        public static PresentAdapter Instance = new PresentAdapter();

        public PresentCollection LoadByOrgId(string orgId)
        {
            return this.Load(builder => builder.AppendItem("OwnOrgID", orgId));
        }

        public void LoadByOrgIdInContext(string orgId,Action<PresentCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("OwnOrgID", orgId)), action);
        }

        /// <summary>
        /// 通过校区获得买赠表信息
        /// </summary>
        /// <param name="CampusID">校区ID</param>
        /// <returns></returns>
        public Present LoadByCampusID(string CampusID)
        {
            PresentCollection dc = this.QueryData(PrepareLoadPresentSqlByPermission(CampusID));
            return dc.FirstOrDefault();
        }

        /// <summary>
        /// 拼装通过校区获得买赠表信息
        /// </summary>
        /// <param name="CampusID">校区ID</param>
        /// <returns>拼装SQL</returns>
        private string PrepareLoadPresentSqlByPermission(string CampusID)
        {
            WhereSqlClauseBuilder presentBuilder = new WhereSqlClauseBuilder();
            presentBuilder.AppendItem("PresentStatus", PresentStatusDefine.Enabled.GetHashCode());
            WhereSqlClauseBuilder presentPermissionBuilder = new WhereSqlClauseBuilder();
            presentPermissionBuilder.AppendItem("UseOrgType", PPTS.Data.Common.Security.DepartmentType.Campus.GetHashCode());
            presentPermissionBuilder.AppendItem("UseOrgID", CampusID);
            OrderBySqlClauseBuilder orderBuilder = new OrderBySqlClauseBuilder();
            orderBuilder.AppendItem("CreateTime", FieldSortDirection.Descending);
            string sql = string.Format(@"select top 1 * from {0} where {1} and PresentID in 
                                    (
	                                    select PresentID from {2}  where {3}
                                    ) 
                                    order by {4} "
            , this.GetQueryMappingInfo().GetQueryTableName()
            , presentBuilder.ToSqlString(TSqlBuilder.Instance)
            , PresentPermissionAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , presentPermissionBuilder.ToSqlString(TSqlBuilder.Instance)
            , orderBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}
