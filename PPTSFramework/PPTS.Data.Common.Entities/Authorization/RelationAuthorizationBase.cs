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
    /// 关系授权基础结构
    /// </summary>
    [Serializable]
    [DataContract]
    public class RelationAuthorizationBase
    {
        /// <summary>
        /// 授权对象ID
        /// </summary>
        [ORFieldMapping("ObjectID")]
        [DataMember]
        public virtual string ObjectID { get; set; }

        /// <summary>
        /// 授权对象关系类别
        /// </summary>
        [ORFieldMapping("ObjectType", PrimaryKey = true)]
        [DataMember]
        public virtual RelationType ObjectType { get; set; }

        /// <summary>
        /// 授权对象机构(仅做记录)
        /// </summary>
        [ORFieldMapping("OrgID")]
        [DataMember]
        public virtual string OrgID { get; set; }

        /// <summary>
        /// 授权对象机构(仅做记录)
        /// </summary>
        [ORFieldMapping("OrgType")]
        [DataMember]
        public virtual OrgType OrgType { get; set; }

        /// <summary>
        /// 被授权对象ID
        /// </summary>
        [ORFieldMapping("OwnerID", PrimaryKey = true)]
        [DataMember]
        public virtual string OwnerID { get; set; }

        /// <summary>
        /// 被授权对象类型
        /// </summary>
        [ORFieldMapping("OwnerType", PrimaryKey = true)]
        [DataMember]
        public virtual RecordType OwnerType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update, DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public virtual DateTime ModifyTime { get; set; }
    }
}
