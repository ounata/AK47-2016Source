using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Products.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a DiscountPermission.
    /// 折扣归属权限表
    /// </summary>
    [Serializable]
    [ORTableMapping("[PM].DiscountPermissions")]
    [DataContract]
    public class DiscountPermission : IEntityWithCreator
    {
        public DiscountPermission()
        {
        }

        /// <summary>
        /// 校区ID
        /// </summary>
        [ORFieldMapping("CampusID", PrimaryKey = true)]
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣ID
        /// </summary>
        [ORFieldMapping("DiscountID", PrimaryKey = true)]
        [DataMember]
        public string DiscountID
        {
            get;
            set;
        }

        [ORFieldMapping("StartDate", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public DateTime StartDate { get; set; }

        [ORFieldMapping("EndDate", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Insert)]
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class DiscountPermissionCollection : EditableDataObjectCollectionBase<DiscountPermission>
    {
    }
}