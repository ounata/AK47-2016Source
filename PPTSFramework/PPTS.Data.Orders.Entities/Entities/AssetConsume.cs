using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a AssetConsume.
	/// 买赠折扣返还消耗记录表
	/// </summary>
	[Serializable]
    [ORTableMapping("OM.AssetConsumes")]
    [DataContract]
	public class AssetConsume
	{		
		public AssetConsume()
		{
		}		

		/// <summary>
		/// 收入归属校区ID
		/// </summary>
		[ORFieldMapping("CampusID")]
        [DataMember]
		public string CampusID
		{
			get;
            set;
		}

		/// <summary>
		/// 收入归属校区名称
		/// </summary>
		[ORFieldMapping("CampusName")]
        [DataMember]
		public string CampusName
		{
			get;
            set;
		}

		/// <summary>
		/// 学员ID
		/// </summary>
		[ORFieldMapping("CustomerID")]
        [DataMember]
		public string CustomerID
		{
			get;
            set;
		}

		/// <summary>
		/// 学员编码
		/// </summary>
		[ORFieldMapping("CustomerCode")]
        [DataMember]
		public string CustomerCode
		{
			get;
            set;
		}

		/// <summary>
		/// 学员姓名
		/// </summary>
		[ORFieldMapping("CustomerName")]
        [DataMember]
		public string CustomerName
		{
			get;
            set;
		}

		/// <summary>
		/// 账户ID
		/// </summary>
		[ORFieldMapping("AccountID")]
        [DataMember]
		public string AccountID
		{
			get;
            set;
		}

		/// <summary>
		/// 资产ID
		/// </summary>
		[ORFieldMapping("AssetID")]
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
		/// 资产类型（参考资产表）
		/// </summary>
		[ORFieldMapping("AssetType")]
        [DataMember]
		public string AssetType
		{
			get;
            set;
		}

		/// <summary>
		/// 资产来源类型
		/// </summary>
		[ORFieldMapping("AssetRefType")]
        [DataMember]
		public string AssetRefType
		{
			get;
            set;
		}

		/// <summary>
		/// 资产来源PID（存放订购单ID）
		/// </summary>
		[ORFieldMapping("AssetRefPID")]
        [DataMember]
		public string AssetRefPID
		{
			get;
            set;
		}

		/// <summary>
		/// 资产来源ID（存放订购明细ID）
		/// </summary>
		[ORFieldMapping("AssetRefID")]
        [DataMember]
		public string AssetRefID
		{
			get;
            set;
		}

		/// <summary>
		/// 之前资产剩余价值
		/// </summary>
		[ORFieldMapping("AssetMoney")]
        [DataMember]
		public decimal AssetMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 消耗单ID
		/// </summary>
		[ORFieldMapping("ConsumeID", PrimaryKey=true)]
        [DataMember]
		public string ConsumeID
		{
			get;
            set;
		}

		/// <summary>
		/// 消耗类别（0-课程，1-非课程）
		/// </summary>
		[ORFieldMapping("ConsumeKind")]
        [DataMember]
		public string ConsumeKind
		{
			get;
            set;
		}

		/// <summary>
		/// 消耗类型（0-排课确认，1非课程确认，2-买赠返还）
		/// </summary>
        [ConstantCategory("C_CODE_ABBR_Order_ConsumeType")]
		[ORFieldMapping("ConsumeType")]
        [DataMember]
		public ConsumeTypeDefine ConsumeType
		{
			get;
            set;
		}

		/// <summary>
		/// 消耗金额
		/// </summary>
		[ORFieldMapping("ConsumeMoney")]
        [DataMember]
		public decimal ConsumeMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 消耗说明
		/// </summary>
		[ORFieldMapping("ConsumeMemo")]
        [DataMember]
		public string ConsumeMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 消耗时间
		/// </summary>
		[ORFieldMapping("ConsumeTime")]
        [DataMember]
		public DateTime ConsumeTime
		{
			get;
            set;
		}

		/// <summary>
		/// 咨询师ID
		/// </summary>
		[ORFieldMapping("ConsultantID")]
        [DataMember]
		public string ConsultantID
		{
			get;
            set;
		}

		/// <summary>
		/// 咨询师姓名
		/// </summary>
		[ORFieldMapping("ConsultantName")]
        [DataMember]
		public string ConsultantName
		{
			get;
            set;
		}

		/// <summary>
		/// 咨询师岗位ID
		/// </summary>
		[ORFieldMapping("ConsultantJobID")]
        [DataMember]
		public string ConsultantJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 学管师ID
		/// </summary>
		[ORFieldMapping("EducatorID")]
        [DataMember]
		public string EducatorID
		{
			get;
            set;
		}

		/// <summary>
		/// 学管师姓名
		/// </summary>
		[ORFieldMapping("EducatorName")]
        [DataMember]
		public string EducatorName
		{
			get;
            set;
		}

		/// <summary>
		/// 学管师岗位ID
		/// </summary>
		[ORFieldMapping("EducatorJobID")]
        [DataMember]
		public string EducatorJobID
		{
			get;
            set;
		}
        
		/// <summary>
		/// 返还的课次数/课次数
		/// </summary>
		[ORFieldMapping("Amount")]
        [DataMember]
		public decimal Amount
		{
			get;
            set;
		}

		/// <summary>
		/// 返还的差价/课次单价
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
		[ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [DataMember]
		public DateTime CreateTime
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class AssetConsumeCollection : EditableDataObjectCollectionBase<AssetConsume>
    {
    }
}