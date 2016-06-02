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
    /// This object represents the properties and methods of a CustomerVisit.
    /// 客户回访信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerVisits")]
    [DataContract]
    public class CustomerVisit : IEntityWithCreator, IEntityWithModifier
    {
        public CustomerVisit()
        {
        }

        /// <summary>
        /// 校区ID
        /// </summary>
        [ORFieldMapping("CampusID")]
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 校区名称
        /// </summary>
        [ORFieldMapping("CampusName")]
        [DataMember]
        public string CampusName
        {
            get;
            set;
        }

        /// <summary>
        /// 客户ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 回访ID
        /// </summary>
        [ORFieldMapping("VisitID", PrimaryKey = true)]
        [DataMember]
        public string VisitID
        {
            get;
            set;
        }

        /// <summary>
        /// 回访类型代码
        /// </summary>
        [ORFieldMapping("VisitType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_BO_Customer_ReturnInfoType")]
        public string VisitType
        {
            get;
            set;
        }

        /// <summary>
        /// 回访方式代码
        /// </summary>
        [ORFieldMapping("VisitWay")]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_ReturnWay")]
        [DataMember]
        public string VisitWay
        {
            get;
            set;
        }

        /// <summary>
        /// 回访内容
        /// </summary>
        [ORFieldMapping("VisitContent")]
        [DataMember]
        public string VisitContent
        {
            get;
            set;
        }

        /// <summary>
        /// 回访时间
        /// </summary>
        [ORFieldMapping("VisitTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime VisitTime
        {
            get;
            set;
        }

        /// <summary>
        /// 回访人ID
        /// </summary>
        [ORFieldMapping("VisitorID")]
        [DataMember]
        public string VisitorID
        {
            get;
            set;
        }

        /// <summary>
        /// 回访人姓名
        /// </summary>
        [ORFieldMapping("VisitorName")]
        [DataMember]
        public string VisitorName
        {
            get;
            set;
        }

        /// <summary>
        /// 回访人岗位ID
        /// </summary>
        [ORFieldMapping("VisitorJobID")]
        [DataMember]
        public string VisitorJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 回访人岗位名称
        /// </summary>
        [ORFieldMapping("VisitorJobName")]
        [DataMember]
        public string VisitorJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 预计下次回访时间
        /// </summary>
        [ORFieldMapping("NextVisitTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime NextVisitTime
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("Satisficing")]
        [ConstantCategory("C_CODE_ABBR_BO_Customer_Satisfaction")]
        [DataMember]
        public string Satisficing
        {
            get;
            set;
        }

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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
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
    }

    [Serializable]
    [DataContract]
    public class CustomerVisitCollection : EditableDataObjectCollectionBase<CustomerVisit>
    {
    }
}