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
	/// This object represents the properties and methods of a DebookOrder.
	/// 退订表
	/// </summary>
	[Serializable]
    [ORTableMapping("OM.DebookOrders")]
    [DataContract]
	public class DebookOrder : IEntityWithCreator, IEntityWithModifier
    {		
		public DebookOrder()
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
        /// 家长ID
        /// </summary>
        [ORFieldMapping("ParentID")]
        [DataMember]
        public string ParentID
        {
            get;
            set;
        }

        /// <summary>
        /// 家长姓名
        /// </summary>
        [ORFieldMapping("ParentName")]
        [DataMember]
        public string ParentName
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
		/// 学员名称
		/// </summary>
		[ORFieldMapping("CustomerName")]
        [DataMember]
		public string CustomerName
		{
			get;
            set;
		}

		/// <summary>
		/// 退订单ID
		/// </summary>
		[ORFieldMapping("DebookID", PrimaryKey=true)]
        [DataMember]
		public string DebookID
		{
			get;
            set;
		}

		/// <summary>
		/// 退订单号
		/// </summary>
		[ORFieldMapping("DebookNo")]
        [DataMember]
		public string DebookNo
		{
			get;
            set;
		}

		/// <summary>
		/// 退订时间
		/// </summary>
		[ORFieldMapping("DebookTime", UtcTimeToLocal = true)]
        [DataMember]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        public DateTime DebookTime
		{
			get;
            set;
		}

		/// <summary>
		/// 退订状态（审批中，已完成，已拒绝）
		/// </summary>
		[ORFieldMapping("DebookStatus")]
        [DataMember]
		public string DebookStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 退订原因
		/// </summary>
		[ORFieldMapping("DebookMemo")]
        [DataMember]
		public string DebookMemo
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
		/// 家长联系方式
		/// </summary>
		[ORFieldMapping("ContactTel")]
        [DataMember]
		public string ContactTel
		{
			get;
            set;
		}

		/// <summary>
		/// 家长姓名
		/// </summary>
		[ORFieldMapping("Contacter")]
        [DataMember]
		public string Contacter
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
		[ORFieldMapping("SubmitTime", UtcTimeToLocal = true)]
        [DataMember]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        public DateTime SubmitTime
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
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
		[ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [DataMember]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
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
    public class DebookOrderCollection : EditableDataObjectCollectionBase<DebookOrder>
    {
    }
}