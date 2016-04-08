using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a CustomerStaffRelation.
	/// 客户员工的关系表
	/// </summary>
	[Serializable]
    [ORTableMapping("CustomerStaffRelations")]
    [DataContract]
	public class CustomerStaffRelation
	{		
		public CustomerStaffRelation()
		{
		}		

		/// <summary>
		/// 主键
		/// </summary>
		[ORFieldMapping("ID", PrimaryKey=true)]
        [DataMember]
		public string ID
		{
			get;
            set;
		}

		/// <summary>
		/// 1 销售关系: 学生，销售（咨询师）[原来写的是家长，销售关系，但是看到逻辑是学生和销售关系];2 教管关系：学生，学管（班主任）;3 教学关系: 学生，老师;4 电销关系
		/// </summary>
		[ORFieldMapping("RelationType")]
        [DataMember]
		public string RelationType
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
		/// 员工所属组织机构ID
		/// </summary>
		[ORFieldMapping("OrgID")]
        [DataMember]
		public string OrgID
		{
			get;
            set;
		}

		/// <summary>
		/// 员工所属组织机构名称
		/// </summary>
		[ORFieldMapping("OrgName")]
        [DataMember]
		public string OrgName
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
		[ORFieldMapping("CreateTime")]
        [DataMember]
		public DateTime CreateTime
		{
			get;
            set;
		}

		/// <summary>
		/// 租户的ID
		/// </summary>
		[ORFieldMapping("TenantCode")]
        [DataMember]
		public string TenantCode
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