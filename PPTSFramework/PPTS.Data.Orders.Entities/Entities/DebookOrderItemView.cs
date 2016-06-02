using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;


namespace PPTS.Data.Orders.Entities
{
    /// <summary>
    /// 退订明细视图
    /// </summary>
    [Serializable]
    [ORTableMapping("OM.v_DebookOrderItems")]
    [DataContract]
    public class DebookOrderItemView : DebookOrderItem
    {
        #region DebookOrder

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
        /// 家长名称
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
		/// 退订时间
		/// </summary>
		[ORFieldMapping("DebookTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime DebookTime
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

        #endregion

        #region v_OrderItems

        /// <summary>
        /// 订单ID
        /// </summary>
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
		/// 订购类型
		/// </summary>
		[ORFieldMapping("OrderType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Order_OrderType")]
        public OrderType OrderType
        {
            get;
            set;
        }

        /// <summary>
        /// 订购明细单ID
        /// </summary>
        [ORFieldMapping("OrderItemID")]
        [DataMember]
        public string OrderItemID
        {
            set;
            get;
        }

        /// <summary>
        /// 订购明细单号
        /// </summary>
        [ORFieldMapping("OrderItemNo")]
        [DataMember]
        public string OrderItemNo
        {
            set;
            get;
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
		/// 实际价格
		/// </summary>
		[ORFieldMapping("RealPrice")]
        [DataMember]
        public decimal RealPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 实际数量
        /// </summary>
        [ORFieldMapping("RealAmount")]
        [DataMember]
        public decimal RealAmount
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
        /// 已确认金额（课程资产与非课程资产用）
        /// </summary>
        [ORFieldMapping("ConfirmedMoney")]
        [DataMember]
        public decimal ConfirmedMoney
        {
            get;
            set;
        }

        #endregion
    }

    [Serializable]
    [DataContract]
    public class DebookOrderItemViewCollection : EditableDataObjectCollectionBase<DebookOrderItemView>
    {
    }
}
