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
	/// This object represents the properties and methods of a AssetConfirm.
	/// 账户缴费申请表
	/// </summary>
	[Serializable]
    [ORTableMapping("AssetConfirms")]
    [DataContract]
	public class AssetConfirm
	{		
		public AssetConfirm()
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
		/// 确认单ID
		/// </summary>
		[ORFieldMapping("ConfirmID", PrimaryKey=true)]
        [DataMember]
		public string ConfirmID
		{
			get;
            set;
		}

		/// <summary>
		/// 确认类型（0-非课程，1-课程类）
		/// </summary>
		[ORFieldMapping("ConfirmType")]
        [DataMember]
		public string ConfirmType
		{
			get;
            set;
		}

		/// <summary>
		/// 确认金额
		/// </summary>
		[ORFieldMapping("ConfirmMoney")]
        [DataMember]
		public decimal ConfirmMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 确认说明
		/// </summary>
		[ORFieldMapping("ConfirmMemo")]
        [DataMember]
		public string ConfirmMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 确认状态（1-已确认，3-已删除 ）参考排课
		/// </summary>
		[ORFieldMapping("ConfirmStatus")]
        [DataMember]
		public string ConfirmStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 确认时间
		/// </summary>
		[ORFieldMapping("ConfirmTime")]
        [DataMember]
		public DateTime ConfirmTime
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人ID
		/// </summary>
		[ORFieldMapping("ConfirmerID")]
        [DataMember]
		public string ConfirmerID
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人姓名
		/// </summary>
		[ORFieldMapping("ConfirmerName")]
        [DataMember]
		public string ConfirmerName
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人岗位ID
		/// </summary>
		[ORFieldMapping("ConfirmerJobID")]
        [DataMember]
		public string ConfirmerJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人岗位名称
		/// </summary>
		[ORFieldMapping("ConfirmerJobName")]
        [DataMember]
		public string ConfirmerJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理状态（参考订购）
		/// </summary>
		[ORFieldMapping("ProcessStatus")]
        [DataMember]
		public string ProcessStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理时间
		/// </summary>
		[ORFieldMapping("ProcessTime")]
        [DataMember]
		public DateTime ProcessTime
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理说明
		/// </summary>
		[ORFieldMapping("ProcessMemo")]
        [DataMember]
		public string ProcessMemo
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
		/// 咨询师岗位名称
		/// </summary>
		[ORFieldMapping("ConsultantJobName")]
        [DataMember]
		public string ConsultantJobName
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
		/// 学管师姓名
		/// </summary>
		[ORFieldMapping("EducatorJobName")]
        [DataMember]
		public string EducatorJobName
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
    public class AssetConfirmCollection : EditableDataObjectCollectionBase<AssetConfirm>
    {
    }
}