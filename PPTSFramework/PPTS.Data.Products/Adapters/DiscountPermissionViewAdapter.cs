using MCS.Library.Data;
using PPTS.Data.Products.Entities;
using System.Collections.Generic;
using System.Linq;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using System;

namespace PPTS.Data.Products.Adapters
{
    public class DiscountPermissionViewAdapter : ProductAdapterBase<DiscountPermissionView, DiscountPermissionViewCollection>
    {
        public static DiscountPermissionViewAdapter Instance = new DiscountPermissionViewAdapter();

        /// <summary>
        /// 获取指定校区折扣信息。
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <returns></returns>
        public DiscountPermissionViewCollection LoadCollectionByCampusID(string campusID)
        {
            return this.Load(builder => builder.AppendItem("UseOrgID", campusID));
        }

        /// <summary>
        /// 获取指定校区当前有效的折扣ID。
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <returns></returns>
        public DiscountPermissionView LoadByCampusID(string campusID)
        {
            return this.Load(builder => builder.AppendItem("UseOrgID", campusID).AppendItem("DiscountStatus", (int)DiscountStatusDefine.Enabled)).SingleOrDefault();
        }
    }
}
