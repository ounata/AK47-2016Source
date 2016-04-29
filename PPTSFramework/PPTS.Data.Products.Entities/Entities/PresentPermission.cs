using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
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
    [ORTableMapping("PresentPermissions")]
    [DataContract]
    public class PresentPermission
    {
        public PresentPermission()
        {
        }

        /// <summary>
        /// 使用者组织ID
        /// </summary>
        [ORFieldMapping("UseOrgID", PrimaryKey = true)]
        [DataMember]
        public string UseOrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 使用者组织类型
        /// </summary>
        [ORFieldMapping("UseOrgType")]
        [DataMember]
        public string UseOrgType
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

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
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