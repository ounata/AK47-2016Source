using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Common.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a ConfigOrg.
	/// 配置的机构信息
	/// </summary>
	[Serializable]
    [ORTableMapping("MT.ConfigOrgs")]
    [DataContract]
	public class ConfigOrg
	{		
		public ConfigOrg()
		{
		}		

		/// <summary>
		/// 机构ID
		/// </summary>
		[ORFieldMapping("OrgID", PrimaryKey=true)]
        [DataMember]
		public string OrgID
		{
			get;
            set;
		}

		/// <summary>
		/// 机构名称
		/// </summary>
		[ORFieldMapping("OrgName")]
        [DataMember]
		public string OrgName
		{
			get;
            set;
		}

		/// <summary>
		/// 机构类型
		/// </summary>
		[ORFieldMapping("OrgType")]
        [DataMember]
		public string OrgType
		{
			get;
            set;
		}

		/// <summary>
		/// 父机构ID
		/// </summary>
		[ORFieldMapping("ParentOrgID")]
        [DataMember]
		public string ParentOrgID
		{
			get;
            set;
		}

		/// <summary>
		/// 父机构名称
		/// </summary>
		[ORFieldMapping("ParentOrgName")]
        [DataMember]
		public string ParentOrgName
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
		/// 创建人
		/// </summary>
		[ORFieldMapping("CreatorName")]
        [DataMember]
		public string CreatorName
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
		/// 最后修改人
		/// </summary>
		[ORFieldMapping("ModifierName")]
        [DataMember]
		public string ModifierName
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class ConfigOrgCollection : EditableDataObjectCollectionBase<ConfigOrg>
    {
    }
}