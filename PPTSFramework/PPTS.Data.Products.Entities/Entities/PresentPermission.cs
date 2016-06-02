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
    /// This object represents the properties and methods of a PresentPermission.
    /// 买赠归属权限表
    /// </summary>
    [Serializable]
    [ORTableMapping("[PM].PresentPermissions")]
    [DataContract]
    public class PresentPermission : IEntityWithCreator
    {
        public PresentPermission()
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
        /// 买赠ID
        /// </summary>
        [ORFieldMapping("PresentID", PrimaryKey = true)]
        [DataMember]
        public string PresentID
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
    public class PresentPermissionCollection : EditableDataObjectCollectionBase<PresentPermission>
    {
    }
}