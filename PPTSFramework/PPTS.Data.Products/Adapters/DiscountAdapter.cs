﻿using MCS.Library.Data;
using PPTS.Data.Products.Entities;
using System.Collections.Generic;
using System.Linq;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using System;
using MCS.Library.Data.Builder;

namespace PPTS.Data.Products.Adapters
{
    public class DiscountAdapter : ProductAdapterBase<Discount, DiscountCollection>
    {
        public static DiscountAdapter Instance = new DiscountAdapter();

        /// <summary>
        /// 根据折扣ID获取折扣信息。
        /// </summary>
        /// <param name="discountID">折扣ID</param>
        /// <returns></returns>
        public Discount LoadByDiscountID(string discountID)
        {
            return this.Load(builder => builder.AppendItem("DiscountID", discountID)).SingleOrDefault();
        }

        /// <summary>
        /// 根据所有者获取折扣表。
        /// </summary>
        /// <param name="ownerIDs">所有者ID列表</param>
        /// <returns></returns>
        public DiscountCollection LoadCollectionByOwnerIDs(string[] ownerIDs)
        {
            return null;
        }

        /// <summary>
        /// 通过校区获得折扣表信息
        /// </summary>
        /// <param name="CampusID">校区ID</param>
        /// <returns></returns>
        public Discount LoadByCampusID(string CampusID)
        {
            DiscountCollection dc = this.QueryData(PrepareLoadDiscountSqlByPermission(CampusID));
           return dc.FirstOrDefault();
        }

        /// <summary>
        /// 拼装通过校区获得折扣表信息
        /// </summary>
        /// <param name="CampusID">校区ID</param>
        /// <returns>拼装SQL</returns>
        private string PrepareLoadDiscountSqlByPermission(string CampusID)
        {
            WhereSqlClauseBuilder discountBuilder = new WhereSqlClauseBuilder();
            discountBuilder.AppendItem("DiscountStatus", DiscountStatusDefine.Enabled.GetHashCode());
            WhereSqlClauseBuilder discountPermissionBuilder = new WhereSqlClauseBuilder();
            discountPermissionBuilder.AppendItem("UseOrgType", PPTS.Data.Common.Security.DepartmentType.Campus.GetHashCode());
            discountPermissionBuilder.AppendItem("UseOrgID", CampusID);
            OrderBySqlClauseBuilder orderBuilder = new OrderBySqlClauseBuilder();
            orderBuilder.AppendItem("CreateTime", FieldSortDirection.Descending);
            string sql = string.Format(@"select top 1 * from {0} where {1} and DiscountID in 
                                    (
	                                    select DiscountID from {2}  where {3}
                                    ) 
                                    order by {4} "
            , this.GetQueryMappingInfo().GetQueryTableName()
            , discountBuilder.ToSqlString(TSqlBuilder.Instance)
            , DiscountPermissionAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , discountPermissionBuilder.ToSqlString(TSqlBuilder.Instance)
            , orderBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}