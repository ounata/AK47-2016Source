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
	/// This object represents the properties and methods of a CustomerTeacherRelation.
	/// 
	/// </summary>
	[Serializable]
    [ORTableMapping("CustomerTeacherRelations")]
    [DataContract]
	public class CustomerTeacherRelation
	{		
		public CustomerTeacherRelation()
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
		/// 
		/// </summary>
		[ORFieldMapping("CustomerID")]
        [DataMember]
		public string CustomerID
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("OrgID")]
        [DataMember]
		public string OrgID
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("OrgName")]
        [DataMember]
		public string OrgName
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("TeacherID")]
        [DataMember]
		public string TeacherID
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("TeacherCode")]
        [DataMember]
		public string TeacherCode
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("TeacherName")]
        [DataMember]
		public string TeacherName
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("Subject")]
        [DataMember]
		public string Subject
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("Grade")]
        [DataMember]
		public string Grade
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("CreatorID")]
        [DataMember]
		public string CreatorID
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("CreatorName")]
        [DataMember]
		public string CreatorName
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("CreateTime")]
        [DataMember]
		public DateTime CreateTime
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class CustomerTeacherRelationCollection : EditableDataObjectCollectionBase<CustomerTeacherRelation>
    {
    }
}