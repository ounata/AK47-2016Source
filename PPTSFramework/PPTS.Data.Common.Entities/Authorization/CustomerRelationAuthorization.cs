using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// 客户关系授权对象
    /// </summary>
    [Serializable]
    [ORTableMapping("MT.CustomerRelationAuthorizations")]
    [DataContract]
    public class CustomerRelationAuthorization
    {
        /// <summary>
        /// 授权对象ID
        /// </summary>
        [ORFieldMapping("ObjectID", PrimaryKey = true)]
        [DataMember]
        public string ObjectID { get; set; }

        /// <summary>
        /// 授权对象关系类别
        /// </summary>
        [ORFieldMapping("ObjectType", PrimaryKey = true)]
        [DataMember]
        public RelationType ObjectType { get; set; }

        /// <summary>
        /// 授权对象机构(仅做记录)
        /// </summary>
        [ORFieldMapping("OrgID")]
        [DataMember]
        public string OrgID { get; set; }

        /// <summary>
        /// 授权对象机构(仅做记录)
        /// </summary>
        [ORFieldMapping("OrgType")]
        [DataMember]
        public OrgType OrgType { get; set; }

        /// <summary>
        /// 被授权对象ID
        /// </summary>
        [ORFieldMapping("OwnerID", PrimaryKey = true)]
        [DataMember]
        public string OwnerID { get; set; }

        /// <summary>
        /// 被授权对象类型
        /// </summary>
        [ORFieldMapping("OwnerType", PrimaryKey = true)]
        [DataMember]
        public RecordType OwnerType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime")]
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime")]
        [DataMember]
        public DateTime ModifyTime { get; set; }
    }

    public class CustomerRelationAuthorizationCollection : EditableDataObjectCollectionBase<CustomerRelationAuthorization>
    { }
}
