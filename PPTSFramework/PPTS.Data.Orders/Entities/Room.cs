using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a Room.
	/// 教室表
	/// </summary>
	[Serializable]
    [ORTableMapping("Rooms")]
    [DataContract]
	public class Room
	{		
		public Room()
		{
		}		

		/// <summary>
		/// 校区ID
		/// </summary>
		[ORFieldMapping("CampusID")]
        [DataMember]
		public string CampusID
		{
			get;
            set;
		}

		/// <summary>
		/// 校区名称
		/// </summary>
		[ORFieldMapping("CampusName")]
        [DataMember]
		public string CampusName
		{
			get;
            set;
		}

		/// <summary>
		/// 教室ID
		/// </summary>
		[ORFieldMapping("RoomID", PrimaryKey=true)]
        [DataMember]
		public string RoomID
		{
			get;
            set;
		}

		/// <summary>
		/// 教室编码
		/// </summary>
		[ORFieldMapping("RoomCode")]
        [DataMember]
		public string RoomCode
		{
			get;
            set;
		}

		/// <summary>
		/// 教室名称
		/// </summary>
		[ORFieldMapping("RoomName")]
        [DataMember]
		public string RoomName
		{
			get;
            set;
		}

		/// <summary>
		/// 教室状态（0-禁用，1-启用）
		/// </summary>
		[ORFieldMapping("RoomStatus")]
        [DataMember]
		public string RoomStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 显示顺序
		/// </summary>
		[ORFieldMapping("SortNo")]
        [DataMember]
		public int SortNo
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
    public class RoomCollection : EditableDataObjectCollectionBase<Room>
    {
    }
}