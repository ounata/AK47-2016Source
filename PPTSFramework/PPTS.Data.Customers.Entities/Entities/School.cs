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
	/// This object represents the properties and methods of a School.
	/// 在读学校信息
	/// </summary>
	[Serializable]
    [ORTableMapping("Schools")]
    [DataContract]
	public class School
	{		
		public School()
		{
		}		

		/// <summary>
		/// 组织机构ID
		/// </summary>
		[ORFieldMapping("OrgID")]
        [DataMember]
		public string OrgID
		{
			get;
            set;
		}

		/// <summary>
		/// 在读学校ID
		/// </summary>
		[ORFieldMapping("SchoolID", PrimaryKey=true)]
        [DataMember]
		public string SchoolID
		{
			get;
            set;
		}

		/// <summary>
		/// 在读学校名称
		/// </summary>
		[ORFieldMapping("SchoolName")]
        [DataMember]
		public string SchoolName
		{
			get;
            set;
		}

		/// <summary>
		/// 学年制（三年制-八年制）
		/// </summary>
		[ORFieldMapping("SchoolYear")]
        [DataMember]
		public string SchoolYear
		{
			get;
            set;
		}

		/// <summary>
		/// 学段（小学，中学，高中，完中，九年一贯，十二年一贯）
		/// </summary>
		[ORFieldMapping("SchoolRange")]
        [DataMember]
		public string SchoolRange
		{
			get;
            set;
		}

		/// <summary>
		/// 学校等级（普通学校，区重点，市重点等）
		/// </summary>
		[ORFieldMapping("SchoolLevel")]
        [DataMember]
		public string SchoolLevel
		{
			get;
            set;
		}

		/// <summary>
		/// 学校性质（不确定，公立，私立）
		/// </summary>
		[ORFieldMapping("SchoolNature")]
        [DataMember]
		public string SchoolNature
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
    public class SchoolCollection : EditableDataObjectCollectionBase<School>
    {
    }
}