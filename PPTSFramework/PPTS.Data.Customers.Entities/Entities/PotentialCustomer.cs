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

    #region 数据范围权限(存入识别)
    [EntityAuth(RecordType = RecordType.Customer)]
    #endregion 

    #region 数据范围权限(数据读取权限)
    [OwnerRelationScope(Name = "潜在客户列表-咨询关系", Functions = "潜客管理", RelationType = RelationType.Consultant, RecordType = RecordType.Customer)]
    [OwnerRelationScope(Name = "潜在客户列表-建档关系", Functions = "潜客管理,市场资源", RelationType = RelationType.Owner, RecordType = RecordType.Customer)]
    [OwnerRelationScope(Name = "潜在客户列表-电销关系", Functions = "潜客管理", RelationType = RelationType.Callcenter, RecordType = RecordType.Customer)]
    [OwnerRelationScope(Name = "潜在客户列表-市场关系", Functions = "市场资源", RelationType = RelationType.Marketing, RecordType = RecordType.Customer)]

    [RecordOrgScope(Name = "潜在客户列表-本部门", Functions = "潜客管理-本部门", OrgType = Common.Authorization.OrgType.Department, RecordType = RecordType.Customer)]
    [RecordOrgScope(Name = "潜在客户列表-本校区", Functions = "潜客管理-本校区", OrgType = Common.Authorization.OrgType.Campus, RecordType = RecordType.Customer)]
    [RecordOrgScope(Name = "潜在客户列表-本分公司", Functions = "潜客管理-本分公司", OrgType = Common.Authorization.OrgType.Branch, RecordType = RecordType.Customer)]
    [RecordOrgScope(Name = "潜在客户列表-全国", Functions = "潜客管理-全国", OrgType = Common.Authorization.OrgType.HQ, RecordType = RecordType.Customer)]
    #endregion

    #region 数据范围权限(编辑操作权限)
    [OwnerRelationScope(Name = "潜在客户编辑-咨询关系", Functions = "编辑潜客信息", RelationType = RelationType.Consultant ,ActionType =ActionType.Edit, RecordType = RecordType.Customer)]
    [OwnerRelationScope(Name = "潜在客户编辑-建档关系", Functions = "编辑潜客信息", RelationType = RelationType.Owner, ActionType = ActionType.Edit, RecordType = RecordType.Customer)]
    [OwnerRelationScope(Name = "潜在客户编辑-电销关系", Functions = "编辑潜客信息", RelationType = RelationType.Callcenter, ActionType = ActionType.Edit, RecordType = RecordType.Customer)]
    [OwnerRelationScope(Name = "潜在客户编辑-市场关系", Functions = "编辑潜客信息", RelationType = RelationType.Marketing, ActionType = ActionType.Edit, RecordType = RecordType.Customer)]

    [RecordOrgScope(Name = "潜在客户编辑-本部门", Functions = "编辑潜客信息-本部门", OrgType = Common.Authorization.OrgType.Department, ActionType = ActionType.Edit, RecordType = RecordType.Customer)]
    
    #endregion

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