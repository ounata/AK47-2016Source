using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Authorization;

namespace PPTS.Data.Orders.Entities
{

    /// <summary>
    /// 退订明细视图
    /// </summary>
    [Serializable]
    [ORTableMapping("OM.v_DebookOrderItems")]
    [DataContract]

    #region 数据范围权限(存入识别)
    [EntityAuth(RecordType = RecordType.Order)]
    #endregion 

    #region 数据范围权限(数据读取权限)

    [CustomerRelationScope(Name = "退订记录查看(客户关系)", Functions = "退订管理列表", RecordType = CustomerRecordType.Customer)]
    [OwnerRelationScope(Name = "退订记录查看", Functions = "退订管理列表", RecordType = RecordType.Order)]

    [RecordOrgScope(Name = "退订记录查看-本部门", Functions = "退订管理列表-本部门", OrgType = OrgType.Department, RecordType = RecordType.Order)]
    [RecordOrgScope(Name = "退订记录查看-本校区", Functions = "退订管理列表-本校区", OrgType = OrgType.Campus, RecordType = RecordType.Order)]
    [RecordOrgScope(Name = "退订记录查看-本分公司", Functions = "退订管理列表-本分公司", OrgType = OrgType.Branch, RecordType = RecordType.Order)]
    [RecordOrgScope(Name = "退订记录查看-全国", Functions = "退订管理列表-全国", OrgType = OrgType.HQ, RecordType = RecordType.Order)]

    [CustomerRelationScope(Name = "退订记录编辑(客户关系)", Functions = "退订", RecordType = CustomerRecordType.Customer)]
    [OwnerRelationScope(Name = "退订记录编辑", Functions = "退订", RecordType = RecordType.Order)]

    #endregion
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
        [CustomerFieldMapping("CustomerID")]
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
        /// 产品类型
        /// </summary>
        [ORFieldMapping("CategoryType")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_CategoryType")]
        public string CategoryType
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
        /// 订购数量
        /// </summary>
        [ORFieldMapping("OrderAmount")]
        [DataMember]
        public decimal OrderAmount
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
        /// 资产编码
        /// </summary>
        [ORFieldMapping("AssetCode")]
        [DataMember]
        public string AssetCode
        {
            get;
            set;
        }

        #endregion


        /// <summary>
        /// 订购金额
        /// </summary>
        [NoMapping]
        public decimal BookMoney { get { return RealPrice * RealAmount; } }

        /// <summary>
        /// 退订数量(赠送)
        /// </summary>
        [NoMapping]
        public string DebookAmountAndPreset { get { return string.Format("{0}({1})",(int)DebookAmount, (int)PresentAmountOfDebook); } }
    }

    [Serializable]
    [DataContract]
    public class DebookOrderItemViewCollection : EditableDataObjectCollectionBase<DebookOrderItemView>
    {
    }
}
