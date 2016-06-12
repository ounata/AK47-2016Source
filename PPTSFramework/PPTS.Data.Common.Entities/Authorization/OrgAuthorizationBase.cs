using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 机构授权基础结构
    /// </summary>
    [Serializable]
    [DataContract]
    public class OrgAuthorizationBase
    {/// <summary>
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
        public virtual OrgType ObjectType { get; set; }

        [ORFieldMapping("RelationType", PrimaryKey = true)]
        [DataMember]
        public virtual RelationType RelationType { get; set; }

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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update,DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
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
