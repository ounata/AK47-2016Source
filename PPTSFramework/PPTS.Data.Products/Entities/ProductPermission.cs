using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Products.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a ProductPermission.
	/// 产品归属权限表
	/// </summary>
	[Serializable]
    [ORTableMapping("ProductPermissions")]
    [DataContract]
	public class ProductPermission
	{		
		public ProductPermission()
		{
		}		

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("PermissionID", PrimaryKey=true)]
        [DataMember]
		public string PermissionID
		{
			get;
            set;
		}

		/// <summary>
		/// 产品ID
		/// </summary>
		[ORFieldMapping("ProductID")]
        [DataMember]
		public string ProductID
		{
			get;
            set;
		}

		/// <summary>
		/// 使用者组织ID
		/// </summary>
		[ORFieldMapping("UseOrgID")]
        [DataMember]
		public string UseOrgID
		{
			get;
            set;
		}

		/// <summary>
		/// 使用者组织类型
		/// </summary>
		[ORFieldMapping("UseOrgType")]
        [DataMember]
		public int UseOrgType
		{
			get;
            set;
		}

		/// <summary>
		/// 创建人ID
		/// </summary>
		[ORFieldMapping("CreatorID")]
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
		/// 
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
    public class ProductPermissionCollection : EditableDataObjectCollectionBase<ProductPermission>
    {
    }
}