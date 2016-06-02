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
	/// This object represents the properties and methods of a CustomerSchoolRelation.
	/// 客户在读学校关系表
	/// </summary>
	[Serializable]
    [ORTableMapping("CM.CustomerSchoolRelations")]
    [DataContract]
	public class CustomerSchoolRelation
	{		
		public CustomerSchoolRelation()
		{
		}		

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("ID", PrimaryKey=true)]
        [DataMember]
		public string ID
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
		/// 在读学校ID
		/// </summary>
		[ORFieldMapping("SchoolID")]
        [DataMember]
		public string SchoolID
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
		/// 创建日期
		/// </summary>
		[ORFieldMapping("CreateTime")]
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
		[ORFieldMapping("ModifyTime")]
        [DataMember]
		public DateTime ModifyTime
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class CustomerSchoolRelationCollection : EditableDataObjectCollectionBase<CustomerSchoolRelation>
    {
    }
}