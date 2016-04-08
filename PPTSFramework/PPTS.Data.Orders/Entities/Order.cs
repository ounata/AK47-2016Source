using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
//using MCS.Library.Validation;
//using MCS.Library.Core;
//using System.Collections.Generic;
//using System.Linq;
//using PPTS.Data.Common;
//using PPTS.Data.Common.Entities;

namespace PPTS.Data.Orders.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a Order.
	/// 订购表
	/// </summary>
	[Serializable]
    [ORTableMapping("Orders")]
    [DataContract]    
    public class Order
	{		
		public Order()
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
		/// 账户编码
		/// </summary>
		[ORFieldMapping("AccountCode")]
        [DataMember]
		public string AccountCode
		{
			get;
            set;
		}

		/// <summary>
		/// 订单ID
		/// </summary>
		[ORFieldMapping("OrderID", PrimaryKey=true)]
        [DataMember]
		public string OrderID
		{
			get;
            set;
		}

		/// <summary>
		/// 订单号
		/// </summary>
		[ORFieldMapping("OrderNo")]
        [DataMember]
		public string OrderNo
		{
			get;
            set;
		}

		/// <summary>
		/// 订购时间
		/// </summary>
		[ORFieldMapping("OrderTime")]
        [DataMember]
		public DateTime OrderTime
		{
			get;
            set;
		}

		/// <summary>
		/// 订购种类（0-合同订单，1-拓路订单）
		/// </summary>
		[ORFieldMapping("OrderKind")]
        [DataMember]
		public string OrderKind
		{
			get;
            set;
		}

		/// <summary>
		/// 订购类型（0-常规订购，1-插班订购，2-买赠订购，3-补差兑换，4-不补差兑换）
		/// </summary>
		[ORFieldMapping("OrderType")]
        [DataMember]
		public string OrderType
		{
			get;
            set;
		}

		/// <summary>
		/// 订单状态（审批中，已完成，已拒绝）
		/// </summary>
		[ORFieldMapping("OrderStatus")]
        [DataMember]
		public string OrderStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理状态
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
		/// 异步处理描述
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
		/// 学管师岗位名称
		/// </summary>
		[ORFieldMapping("EducatorJobName")]
        [DataMember]
		public string EducatorJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人ID
		/// </summary>
		[ORFieldMapping("SubmitterID")]
        [DataMember]
		public string SubmitterID
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人姓名
		/// </summary>
		[ORFieldMapping("SubmitterName")]
        [DataMember]
		public string SubmitterName
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人岗位ID
		/// </summary>
		[ORFieldMapping("SubmitterJobID")]
        [DataMember]
		public string SubmitterJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人岗位名称
		/// </summary>
		[ORFieldMapping("SubmitterJobName")]
        [DataMember]
		public string SubmitterJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 提交时间
		/// </summary>
		[ORFieldMapping("SubmitTime")]
        [DataMember]
		public DateTime SubmitTime
		{
			get;
            set;
		}

		/// <summary>
		/// 关联缴费申请单ID
		/// </summary>
		[ORFieldMapping("ChargeApplyID")]
        [DataMember]
		public string ChargeApplyID
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
    public class OrderCollection : EditableDataObjectCollectionBase<Order>
    {
    }
}