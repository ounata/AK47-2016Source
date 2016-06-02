using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a CustomerFollowItem.
    /// 跟进明细表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerFollowItems")]
    [DataContract]
	public class CustomerFollowItem
	{		
		public CustomerFollowItem()
		{
		}		

		/// <summary>
		/// 跟进ID
		/// </summary>
		[ORFieldMapping("FollowID")]
        [DataMember]
		public string FollowID
		{
			get;
            set;
		}

		/// <summary>
		/// 明细ID
		/// </summary>
		[ORFieldMapping("ItemID", PrimaryKey=true)]
        [DataMember]
		public string ItemID
		{
			get;
            set;
		}

		/// <summary>
		/// 科目代码
		/// </summary>
		[ORFieldMapping("Subject")]
        [DataMember]
		public string Subject
		{
			get;
            set;
		}

		/// <summary>
		/// 辅导机构
		/// </summary>
		[ORFieldMapping("Institude")]
        [DataMember]
		public string Institude
		{
			get;
            set;
		}

		/// <summary>
		/// 辅导起始时间
		/// </summary>
		[ORFieldMapping("StartDate")]
        [DataMember]
		public DateTime StartDate
		{
			get;
            set;
		}

		/// <summary>
		/// 辅导终止时间
		/// </summary>
		[ORFieldMapping("EndDate")]
        [DataMember]
		public DateTime EndDate
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class CustomerFollowItemCollection : EditableDataObjectCollectionBase<CustomerFollowItem>
    {
    }
}