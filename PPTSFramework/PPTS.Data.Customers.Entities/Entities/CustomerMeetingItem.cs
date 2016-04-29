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
	/// This object represents the properties and methods of a CustomerMeetingItem.
	/// 学情会明细表
	/// </summary>
	[Serializable]
    [ORTableMapping("CustomerMeetingItems")]
    [DataContract]
	public class CustomerMeetingItem
	{		
		public CustomerMeetingItem()
		{
		}		

		/// <summary>
		/// 学情会ID
		/// </summary>
		[ORFieldMapping("MeetingID")]
        [DataMember]
		public string MeetingID
		{
			get;
            set;
		}

		/// <summary>
		/// 学情会详情ID
		/// </summary>
		[ORFieldMapping("ItemID", PrimaryKey=true)]
        [DataMember]
		public string ItemID
		{
			get;
            set;
		}

		/// <summary>
		/// 参与对象类型
		/// </summary>
		[ORFieldMapping("ObjectType")]
        [DataMember]
		public string ObjectType
		{
			get;
            set;
		}

		/// <summary>
		/// 会议内容类型
		/// </summary>
		[ORFieldMapping("ContentType")]
        [DataMember]
		public string ContentType
		{
			get;
            set;
		}

		/// <summary>
		/// 会议内容数据
		/// </summary>
		[ORFieldMapping("ContentData")]
        [DataMember]
		public string ContentData
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class CustomerMeetingItemCollection : EditableDataObjectCollectionBase<CustomerMeetingItem>
    {
    }
}