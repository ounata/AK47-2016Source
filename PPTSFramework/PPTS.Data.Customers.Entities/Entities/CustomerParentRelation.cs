using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 家长和学员关系表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerParentRelations", "CM.CustomerParentRelations_Current")]
    [DataContract]
    public class CustomerParentRelation : IVersionDataObjectWithoutID, IEntityWithCreator, IEntityWithModifier
    {
        public CustomerParentRelation()
        {
        }

        /// <summary>
        /// 学生ID
        /// </summary>
        [ORFieldMapping("CustomerID", PrimaryKey = true)]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 家长ID
        /// </summary>
        [ORFieldMapping("ParentID", PrimaryKey = true)]
        [DataMember]
        public string ParentID
        {
            get;
            set;
        }

        /// <summary>
        /// 学生对家长的亲属关系(C_CODE_ABBR_CHILDMALEDICTIONARY,C_CODE_ABBR_CHILDFEMALEDICTIONARY)
        /// </summary>
        [ORFieldMapping("CustomerRole")]
        [DataMember]
        public string CustomerRole
        {
            get;
            set;
        }

        /// <summary>
        /// 家长对学生的亲属关系(C_CODE_ABBR_PARENTMALEDICTIONARY,C_CODE_ABBR_PARENTFEMALEDICTIONARY)
        /// </summary>
        [ORFieldMapping("ParentRole")]
        [DataMember]
        public string ParentRole
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是主要监护人
        /// </summary>
        [ORFieldMapping("IsPrimary")]
        [DataMember]
        public bool IsPrimary
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者ID
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
        /// 创建者名称
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
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [ORFieldMapping("ModifierID")]
        [DataMember]
        public string ModifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [DataMember]
        public string ModifierName
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update, DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 版本开始时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionStartTime", PrimaryKey = true, UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 版本结束时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionEndTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionEndTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class CustomerParentRelationCollection : EditableDataObjectCollectionBase<CustomerParentRelation>
    {
    }
}