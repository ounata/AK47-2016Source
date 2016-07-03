using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Authorization;

namespace PPTS.Data.Orders.Entities
{


    #region 数据范围权限(存入识别)
    [EntityAuth(RecordType = RecordType.Order)]
    #endregion 

    #region 数据范围权限(数据读取权限)

    [CustomerRelationScope(Name = "订购记录查看(客户关系)", Functions = "学员视图-充值记录/订购历史/转让记录,订购管理列表（订购单详情）,打印订单凭证",  RecordType = CustomerRecordType.Customer)]
    [OwnerRelationScope(Name = "订购记录查看", Functions = "学员视图-充值记录/订购历史/转让记录,订购管理列表（订购单详情）,打印订单凭证", RecordType = RecordType.Order)]

    [RecordOrgScope(Name = "订购记录查看-本部门", Functions = "学员视图-充值记录/订购历史/转让记录-本部门,订购管理列表（订购单详情）-本部门", OrgType = OrgType.Department, RecordType = RecordType.Order)]
    [RecordOrgScope(Name = "订购记录查看-本校区", Functions = "学员视图-充值记录/订购历史/转让记录-本校区,订购管理列表（订购单详情）-本校区,打印订单凭证-本校区,打印订单凭证-本分公司", OrgType = OrgType.Campus, RecordType = RecordType.Order)]
    [RecordOrgScope(Name = "订购记录查看-本分公司", Functions = "学员视图-充值记录/订购历史/转让记录-本分公司,订购管理列表（订购单详情）-本分公司", OrgType = OrgType.Branch, RecordType = RecordType.Order)]
    [RecordOrgScope(Name = "订购记录查看-全国", Functions = "学员视图-充值记录/订购历史/转让记录-全国,订购管理列表（订购单详情）-全国", OrgType = OrgType.HQ, RecordType = RecordType.Order)]

    [CustomerRelationScope(Name = "订购记录编辑(客户关系)", Functions = "订购,资产兑换", RecordType = CustomerRecordType.Customer)]
    [OwnerRelationScope(Name = "订购记录编辑", Functions = "订购,资产兑换", RecordType = RecordType.Order)]
    [RecordOrgScope(Name = "订购记录编辑-本分公司", Functions = "编辑关联缴费单-本分公司", OrgType = OrgType.Branch, RecordType = RecordType.Order)]
    
    #endregion

    [Serializable]
    [ORTableMapping("OM.v_OrderItems")]
    [DataContract]
    public class OrderItemView : OrderItem
    {
        #region Order

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
        /// 学生年级
        /// </summary>		
        [ORFieldMapping("CustomerGrade")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_CUSTOMER_GRADE")]
        public string CustomerGrade
        {
            get;
            set;
        }
        /// <summary>
        /// 学员年级
        /// </summary>		
        [ORFieldMapping("CustomerGradeName")]
        [DataMember]
        public string CustomerGradeName
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
        [ConstantCategory("c_codE_ABBR_Order_OrderType")]
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
        [ConstantCategory("c_codE_ABBR_Order_OrderStatus")]
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
        /// 提交人岗位类型
        /// </summary>		
        [ORFieldMapping("SubmitterJobType")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Order_Post")]
        public string SubmitterJobType
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
        #endregion

        #region Asset

        /// <summary>
        /// 资产ID
        /// </summary>
        [ORFieldMapping("AssetID", PrimaryKey = true)]
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
        /// 资产名称（资产编号+产品名称）
        /// </summary>
        [ORFieldMapping("AssetName")]
        [DataMember]
        public string AssetName
        {
            get;
            set;
        }

        /// <summary>
        /// 资产类型（0-课程，1-非课程）
        /// </summary>
        [ORFieldMapping("AssetType")]
        [DataMember]
        public AssetTypeDefine AssetType
        {
            get;
            set;
        }

        /// <summary>
        /// 资产来源（0-订单）
        /// </summary>
        [ORFieldMapping("AssetRefType")]
        [DataMember]
        public AssetRefTypeDefine AssetRefType
        {
            get;
            set;
        }

        /// <summary>
        /// 资产来源PID（订单ID）
        /// </summary>
        [ORFieldMapping("AssetRefPID")]
        [DataMember]
        public string AssetRefPID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产来源ID（订单明细ID）
        /// </summary>
        [ORFieldMapping("AssetRefID")]
        [DataMember]
        public string AssetRefID
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
        /// 插班班级名称
        /// </summary>
        [ORFieldMapping("JoinedClassName")]
        [DataMember]
        public string JoinedClassName
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
        /// 剩余数量
        /// </summary>
        [NoMapping]
        public decimal RemainCount { get { return RealAmount - ConfirmedAmount; } }
    }

    [Serializable]
    [DataContract]
    public class OrderItemViewCollection : EditableDataObjectCollectionBase<OrderItemView>
    {

    }
}
