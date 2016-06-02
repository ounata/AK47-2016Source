using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Customer.
    /// 学员信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.Customers", "CM.Customers_Current")]
    [DataContract]
    [CustomerRelationScope(ActionType = ActionType.Read, Functions = "查看客户（不含联系方式）"
        , RelationType = RelationType.Callcenter, RecordType = CustomerRecordType.Customer, Description = "电销关系读取客户权限分配")]

    [CustomerRelationScope(ActionType = ActionType.Read, Functions = "学员列表查看"
        , RelationType = RelationType.Consultant, RecordType = CustomerRecordType.Customer,Description ="咨询关系读取客户权限分配")]

    [CustomerRelationScope(ActionType = ActionType.Read, Functions = "成绩汇总列表查看"
        , RelationType = RelationType.Educator, RecordType = CustomerRecordType.Customer,Description ="学管关系读取客户权限分配")]

    [OrgCustomerRelationScope(OrgType = OrgType.Branch, ActionType = ActionType.Read
        , Functions = "上课记录（学员视图）", RelationType = RelationType.Marketing
        , RecordType = CustomerRecordType.Customer)]

    [OwnerRelationScope(ActionType = ActionType.Read, Functions = "技术支持", RecordType = RecordType.Customer)]

    [RecordOrgScope(OrgType = OrgType.Branch, ActionType = ActionType.Read, Functions = "b,c", RecordType = RecordType.Customer)]
    [RecordOrgScope(OrgType = OrgType.Campus, ActionType = ActionType.Read, Functions = "b,c", RecordType = RecordType.Customer)]
    public class Customer : CustomerBase
    {
        public Customer()
        {
        }

        /// <summary>
        /// 校区ID（学员属性）
        /// </summary>
        [ORFieldMapping("CampusID")]
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 校区名称（学员属性）
        /// </summary>
        [ORFieldMapping("CampusName")]
        [DataMember]
        public string CampusName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否锁定(学员属性)
        /// </summary>
        [ORFieldMapping("Locked")]
        [DataMember]
        public bool Locked
        {
            get;
            set;
        }

        /// <summary>
        /// 锁定描述（转学，毕业）(学员属性)
        /// </summary>
        [ORFieldMapping("LockMemo")]
        [DataMember]
        public string LockMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 学员状态
        /// </summary>
        public StudentStatusDefine StudentStatus
        {
            set;
            get;
        }

        /// <summary>
        /// 是否高三毕业(学员属性)
        /// </summary>
        [ORFieldMapping("Graduated")]
        [DataMember]
        public bool Graduated
        {
            get;
            set;
        }

        /// <summary>
        /// 高三毕业年份(学员属性)
        /// </summary>
        [ORFieldMapping("GraduateYear")]
        [DataMember]
        public string GraduateYear
        {
            get;
            set;
        }

        /// <summary>
        /// 首次签约时间
        /// </summary>
        [ORFieldMapping("FirstSignTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime FirstSignTime
        {
            get;
            set;
        }

        /// <summary>
        /// 首次签约人ID(学员属性)
        /// </summary>
        [ORFieldMapping("FirstSignerID")]
        [DataMember]
        public string FirstSignerID
        {
            get;
            set;
        }

        /// <summary>
        /// 首次签约人(学员属性)
        /// </summary>
        [ORFieldMapping("FirstSignerName")]
        [DataMember]
        public string FirstSignerName
        {
            get;
            set;
        }

        /// <summary>
        /// 当前回访时间(学员属性)
        /// </summary>
        [ORFieldMapping("VisitTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime VisitTime
        {
            get;
            set;
        }

        /// <summary>
        /// 已回访次数(学员属性)
        /// </summary>
        [ORFieldMapping("VisitedCount")]
        [DataMember]
        public int VisitedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 下次回访时间(学员属性)
        /// </summary>
        [ORFieldMapping("NextVisitTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime NextVisitTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class CustomerCollection : EditableDataObjectCollectionBase<Customer>
    {
    }
}