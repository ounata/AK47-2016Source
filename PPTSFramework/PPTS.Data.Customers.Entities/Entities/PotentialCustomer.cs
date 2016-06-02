using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using System;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 潜在客户表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.PotentialCustomers", "CM.PotentialCustomers_Current")]
    [DataContract]

    [EntityAuth(RecordType =RecordType.PotentialCustomer)]

    [CustomerRelationScope(ActionType = ActionType.Read, Functions = "查看客户（不含联系方式）"
        , RelationType = RelationType.Callcenter, RecordType = CustomerRecordType.PotentialCustomer, Description = "电销关系读取客户权限分配")]

    [CustomerRelationScope(ActionType = ActionType.Read, Functions = "学员列表查看"
        , RelationType = RelationType.Consultant, RecordType = CustomerRecordType.PotentialCustomer, Description = "咨询关系读取客户权限分配")]

    [CustomerRelationScope(ActionType = ActionType.Read, Functions = "成绩汇总列表查看"
        , RelationType = RelationType.Educator, RecordType = CustomerRecordType.PotentialCustomer, Description = "学管关系读取客户权限分配")]

    [OrgCustomerRelationScope(OrgType = PPTS.Data.Common.Authorization.OrgType.HQ, ActionType = ActionType.Read
        , Functions = "上课记录（学员视图）", RelationType = RelationType.Marketing
        , RecordType = CustomerRecordType.PotentialCustomer)]

    [OwnerRelationScope(ActionType = ActionType.Read, Functions = "技术支持", RecordType = RecordType.PotentialCustomer)]

    [RecordOrgScope(OrgType = PPTS.Data.Common.Authorization.OrgType.Branch, ActionType = ActionType.Read, Functions = "b,c", RecordType = RecordType.PotentialCustomer)]

    public class PotentialCustomer : CustomerBase
    {
        public PotentialCustomer()
        {
        }

        /// <summary>
        /// 归属组织机构ID
        /// </summary>
        [ORFieldMapping("OrgID")]
        [DataMember]
        public string OrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        [ORFieldMapping("OrgName")]
        [DataMember]
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 归属组织机构类型
        /// </summary>
        [ORFieldMapping("OrgType")]
        [DataMember]
        public OrgTypeDefine OrgType
        {
            get;
            set;
        }

        /// <summary>
        /// 无效客户理由代码（参考跟进）
        /// </summary>
        [ORFieldMapping("InvalidReason")]
        [DataMember]
        public string InvalidReason
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class PotentialCustomerCollection : EditableDataObjectCollectionBase<PotentialCustomer>
    {
    }
}