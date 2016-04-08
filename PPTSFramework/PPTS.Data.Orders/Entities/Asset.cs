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
	/// This object represents the properties and methods of a Asset.
	/// 订购资产表（需要快照）
	/// </summary>
	[Serializable]
    [ORTableMapping("Assets")]
    [DataContract]
	public class Asset
	{		
		public Asset()
		{
		}		

		/// <summary>
		/// 资产ID
		/// </summary>
		[ORFieldMapping("AssetID", PrimaryKey=true)]
        [DataMember]
		public string AssetID
		{
			get;
            set;
		}

		/// <summary>
		/// 资产编码
		/// </summary>
		[ORFieldMapping("AssetCode")]
        [DataMember]
		public string AssetCode
		{
			get;
            set;
		}

		/// <summary>
		/// 资产类型（0-课程，1-非课程）
		/// </summary>
		[ORFieldMapping("AssetType")]
        [DataMember]
		public string AssetType
		{
			get;
            set;
		}

		/// <summary>
		/// 资产来源（0-订单）
		/// </summary>
		[ORFieldMapping("AssetSource")]
        [DataMember]
		public string AssetSource
		{
			get;
            set;
		}

		/// <summary>
		/// 资产来源ID（订单明细ID）
		/// </summary>
		[ORFieldMapping("AssetSourceID")]
        [DataMember]
		public string AssetSourceID
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
		/// 产品编码
		/// </summary>
		[ORFieldMapping("ProductCode")]
        [DataMember]
		public string ProductCode
		{
			get;
            set;
		}

		/// <summary>
		/// 产品名称
		/// </summary>
		[ORFieldMapping("ProductName")]
        [DataMember]
		public string ProductName
		{
			get;
            set;
		}

		/// <summary>
		/// 产品归属校区ID
		/// </summary>
		[ORFieldMapping("ProductCampusID")]
        [DataMember]
		public string ProductCampusID
		{
			get;
            set;
		}

		/// <summary>
		/// 产品归属校区名称
		/// </summary>
		[ORFieldMapping("ProductCampusName")]
        [DataMember]
		public string ProductCampusName
		{
			get;
            set;
		}

		/// <summary>
		/// 过期日期
		/// </summary>
		[ORFieldMapping("ExpirationDate")]
        [DataMember]
		public DateTime ExpirationDate
		{
			get;
            set;
		}

		/// <summary>
		/// 已排数量（课程资产用）
		/// </summary>
		[ORFieldMapping("AssignedAmount")]
        [DataMember]
		public decimal AssignedAmount
		{
			get;
            set;
		}

		/// <summary>
		/// 已上数量（课程资产用）
		/// </summary>
		[ORFieldMapping("ConfirmedAmount")]
        [DataMember]
		public decimal ConfirmedAmount
		{
			get;
            set;
		}

		/// <summary>
		/// 已兑换数量（课程资产用）
		/// </summary>
		[ORFieldMapping("ExchangedAmount")]
        [DataMember]
		public decimal ExchangedAmount
		{
			get;
            set;
		}

		/// <summary>
		/// 已使用订购数量（课程资产用）
		/// </summary>
		[ORFieldMapping("UsedOrderAmount")]
        [DataMember]
		public decimal UsedOrderAmount
		{
			get;
            set;
		}

		/// <summary>
		/// 已使用赠送数量（课程资产用）
		/// </summary>
		[ORFieldMapping("UsedPresentAmount")]
        [DataMember]
		public decimal UsedPresentAmount
		{
			get;
            set;
		}

		/// <summary>
		/// 已退数量（课程资产用）
		/// </summary>
		[ORFieldMapping("DebookedAmount")]
        [DataMember]
		public decimal DebookedAmount
		{
			get;
            set;
		}

		/// <summary>
		/// 已确认金额（课程资产与非课程资产用）
		/// </summary>
		[ORFieldMapping("ConfirmedMoney")]
        [DataMember]
		public decimal ConfirmedMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 返还金额（课程资产用）
		/// </summary>
		[ORFieldMapping("ReturnedMoney")]
        [DataMember]
		public decimal ReturnedMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 当前数量
		/// </summary>
		[ORFieldMapping("Amount")]
        [DataMember]
		public decimal Amount
		{
			get;
            set;
		}

		/// <summary>
		/// 当前单价（针对课时资产由于退订可能与订购时不同）
		/// </summary>
		[ORFieldMapping("Price")]
        [DataMember]
		public decimal Price
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
    public class AssetCollection : EditableDataObjectCollectionBase<Asset>
    {
    }
}