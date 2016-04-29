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
	/// This object represents the properties and methods of a CustomerServiceItem.
	/// 客服处理明细
	/// </summary>
	[Serializable]
    [ORTableMapping("CustomerServiceItems")]
    [DataContract]
	public class CustomerServiceItem
	{		
		public CustomerServiceItem()
		{
		}		

		/// <summary>
		/// 服务ID
		/// </summary>
		[ORFieldMapping("ServiceID")]
        [DataMember]
		public string ServiceID
		{
			get;
            set;
		}

		/// <summary>
		/// 处理ID
		/// </summary>
		[ORFieldMapping("ItemID", PrimaryKey=true)]
        [DataMember]
		public string ItemID
		{
			get;
            set;
		}

		/// <summary>
		/// 处理时间
		/// </summary>
		[ORFieldMapping("HandleTime")]
        [DataMember]
		public DateTime HandleTime
		{
			get;
            set;
		}

		/// <summary>
		/// 处理状态
		/// </summary>
		[ORFieldMapping("HandleStatus")]
        [DataMember]
		public string HandleStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 处理结果
		/// </summary>
		[ORFieldMapping("HandleMemo")]
        [DataMember]
		public string HandleMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 处理人ID
		/// </summary>
		[ORFieldMapping("HandlerID")]
        [DataMember]
		public string HandlerID
		{
			get;
            set;
		}

		/// <summary>
		/// 处理人姓名
		/// </summary>
		[ORFieldMapping("HandlerName")]
        [DataMember]
		public string HandlerName
		{
			get;
            set;
		}

		/// <summary>
		/// 处理人岗位ID
		/// </summary>
		[ORFieldMapping("HandlerJobID")]
        [DataMember]
		public string HandlerJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 处理人岗位名称
		/// </summary>
		[ORFieldMapping("HandlerJobName")]
        [DataMember]
		public string HandlerJobName
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class CustomerServiceItemCollection : EditableDataObjectCollectionBase<CustomerServiceItem>
    {
    }
}