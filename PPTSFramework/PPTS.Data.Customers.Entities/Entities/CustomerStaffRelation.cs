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
    /// 客户员工的关系表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerStaffRelations", "CM.CustomerStaffRelations_Current")]
    [DataContract]
    [CustomerRelationScope(Name = "潜客信息编辑", Functions = "分配市场专员", ActionType = ActionType.Edit, RecordType = CustomerRecordType.Customer)]
    [CustomerRelationScope(Name = "潜客信息编辑-本部门", Functions = "分配咨询师-本部门", RecordType = CustomerRecordType.Customer)]
    [CustomerRelationScope(Name = "潜客信息编辑-本部门", Functions = "分配坐席-本部门", ActionType = ActionType.Edit, RecordType = CustomerRecordType.Customer)]
    public class CustomerStaffRelation : IVersionDataObjectWithoutID, IEntityWithCreator
    {
        public CustomerStaffRelation()
        {
        }

        /// <summary>
        /// 客户ID
        /// </summary>
        [ORFieldMapping("CustomerID", PrimaryKey = true)]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 1 销售关系: 学生，销售（咨询师）[原来写的是家长，销售关系，但是看到逻辑是学生和销售关系];2 教管关系：学生，学管（班主任）;3 教学关系: 学生，老师;4 电销关系
        /// </summary>
        [ORFieldMapping("RelationType",PrimaryKey =true)]
        [DataMember]
        public CustomerRelationType RelationType
        {
            get;
            set;
        }

        /// <summary>
        /// 员工所属组织机构ID
        /// </summary>
        [ORFieldMapping("StaffJobOrgID")]
        [DataMember]
        public string StaffJobOrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 员工所属组织机构名称
        /// </summary>
        [ORFieldMapping("StaffJobOrgName")]
        [DataMember]
        public string StaffJobOrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        [ORFieldMapping("StaffID")]
        [DataMember]
        public string StaffID
        {
            get;
            set;
        }

        /// <summary>
        /// 员工名称
        /// </summary>
        [ORFieldMapping("StaffName")]
        [DataMember]
        public string StaffName
        {
            get;
            set;
        }

        /// <summary>
        /// 员工岗位ID
        /// </summary>
        [ORFieldMapping("StaffJobID")]
        [DataMember]
        public string StaffJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 员工岗位名称
        /// </summary>
        [ORFieldMapping("StaffJobName")]
        [DataMember]
        public string StaffJobName
        {
            get;
            set;
        }        

        /// <summary>
        /// 创建者ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
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
        /// 版本开始时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionStartTime", PrimaryKey = true, UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
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
    public class CustomerStaffRelationCollection : EditableDataObjectCollectionBase<CustomerStaffRelation>
    {
    }
}