using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Orders.Entities
{
    [Serializable]
    [ORTableMapping("OM.v_OrderItems")]
    [DataContract]
    public class OrderItemView
    {
        #region OrderItem

        /// <summary>
		/// 顺序号
        /// </summary>		
		[ORFieldMapping("SortNo")]
        [DataMember]
        public int SortNo
        {
            get;
            set;
        }
        /// <summary>
        /// 明细ID
        /// </summary>		
        [ORFieldMapping("ItemID")]
        [DataMember]
        public string ItemID
        {
            get;
            set;
        }
        /// <summary>
        /// 明细编号=（OrderNo+SortNo)=资产编码
        /// </summary>		
        [ORFieldMapping("ItemNo")]
        [DataMember]
        public string ItemNo
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
        /// 颗粒度代码
        /// </summary>		
        [ORFieldMapping("ProductUnit")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_ProductUnit")]
        public string ProductUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 颗粒度名称
        /// </summary>		
        [ORFieldMapping("ProductUnitName")]
        [DataMember]
        public string ProductUnitName
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
        /// 产品年级代码
        /// </summary>		
        [ORFieldMapping("Grade")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_CUSTOMER_GRADE")]
        public string Grade
        {
            get;
            set;
        }
        /// <summary>
        /// 产品年级名称
        /// </summary>		
        [ORFieldMapping("GradeName")]
        [DataMember]
        public string GradeName
        {
            get;
            set;
        }
        /// <summary>
        /// 产品科目代码
        /// </summary>		
        [ORFieldMapping("Subject")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_BO_Product_TeacherSubject")]
        public string Subject
        {
            get;
            set;
        }
        /// <summary>
        /// 产品科目名称
        /// </summary>		
        [ORFieldMapping("SubjectName")]
        [DataMember]
        public string SubjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 产品分类代码（三级）
        /// </summary>		
        [ORFieldMapping("Catalog")]
        [DataMember]
        public string Catalog
        {
            get;
            set;
        }
        /// <summary>
        /// 产品分类名称
        /// </summary>		
        [ORFieldMapping("CatalogName")]
        [DataMember]
        public string CatalogName
        {
            get;
            set;
        }
        /// <summary>
        /// 产品类型代码（一级）
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
        /// 产品类型名称
        /// </summary>		
        [ORFieldMapping("CategoryTypeName")]
        [DataMember]
        public string CategoryTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 课程级别代码
        /// </summary>		
        [ORFieldMapping("CourseLevel")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_CourseLevel")]
        public string CourseLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 课程级别名称
        /// </summary>		
        [ORFieldMapping("CourseLevelName")]
        [DataMember]
        public string CourseLevelName
        {
            get;
            set;
        }
        /// <summary>
        /// 课次时长代码
        /// </summary>		
        [ORFieldMapping("LessonDuration")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_BO_ProductDuration")]
        public string LessonDuration
        {
            get;
            set;
        }
        /// <summary>
        /// 课次时长值
        /// </summary>		
        [ORFieldMapping("LessonDurationValue")]
        [DataMember]
        public decimal LessonDurationValue
        {
            get;
            set;
        }
        /// <summary>
        /// 原始价格
        /// </summary>		
        [ORFieldMapping("OrderPrice")]
        [DataMember]
        public decimal OrderPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 原始数量
        /// </summary>		
        [ORFieldMapping("OrderAmount")]
        [DataMember]
        public decimal OrderAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 买赠ID
        /// </summary>		
        [ORFieldMapping("PresentID")]
        [DataMember]
        public string PresentID
        {
            get;
            set;
        }
        /// <summary>
        /// 买赠表配额
        /// </summary>		
        [ORFieldMapping("PresentQuato")]
        [DataMember]
        public decimal PresentQuato
        {
            get;
            set;
        }
        /// <summary>
        /// 实际赠送数量
        /// </summary>		
        [ORFieldMapping("PresentAmount")]
        [DataMember]
        public decimal PresentAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 拓路折扣率
        /// </summary>		
        [ORFieldMapping("TunlandRate")]
        [DataMember]
        public decimal TunlandRate
        {
            get;
            set;
        }
        /// <summary>
        /// 特殊折扣率
        /// </summary>		
        [ORFieldMapping("SpecialRate")]
        [DataMember]
        public decimal SpecialRate
        {
            get;
            set;
        }
        /// <summary>
        /// 折扣类型（无折扣，拓路折口，特殊折扣，买赠折扣，其它）
        /// </summary>		
        [ORFieldMapping("DiscountType")]
        [DataMember]
        public string DiscountType
        {
            get;
            set;
        }
        /// <summary>
        /// 折扣率
        /// </summary>		
        [ORFieldMapping("DiscountRate")]
        [DataMember]
        public decimal DiscountRate
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
        /// 优惠限额
        /// </summary>		
        [ORFieldMapping("PromotionQuota")]
        [DataMember]
        public decimal PromotionQuota
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
        /// 插班班级ID
        /// </summary>		
        [ORFieldMapping("JoinedClassID")]
        [DataMember]
        public string JoinedClassID
        {
            get;
            set;
        }
        /// <summary>
        /// 兑换关联的源资产ID
        /// </summary>		
        [ORFieldMapping("RelatedAssetID")]
        [DataMember]
        public string RelatedAssetID
        {
            get;
            set;
        }
        /// <summary>
        /// 兑换关联的源资产编码
        /// </summary>		
        [ORFieldMapping("RelatedAssetCode")]
        [DataMember]
        public string RelatedAssetCode
        {
            get;
            set;
        }

        #endregion

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
        /// 订单ID
        /// </summary>		
        [ORFieldMapping("OrderID")]
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

        /// <summary>
        /// 最后审批人ID
        /// </summary>		
        [ORFieldMapping("ApproverID")]
        [DataMember]
        public string ApproverID
        {
            get;
            set;
        }
        /// <summary>
        /// 最后审批人姓名
        /// </summary>		
        [ORFieldMapping("ApproverName")]
        [DataMember]
        public string ApproverName
        {
            get;
            set;
        }
        /// <summary>
        /// 最后审批人岗位ID
        /// </summary>		
        [ORFieldMapping("ApproverJobID")]
        [DataMember]
        public string ApproverJobID
        {
            get;
            set;
        }
        /// <summary>
        /// 最后审批人岗位名称
        /// </summary>		
        [ORFieldMapping("ApproverJobName")]
        [DataMember]
        public string ApproverJobName
        {
            get;
            set;
        }
        /// <summary>
        /// 最后审批时间
        /// </summary>		
        [ORFieldMapping("ApproveTime")]
        [DataMember]
        public DateTime ApproveTime
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

        

        #endregion

    }

    [Serializable]
    [DataContract]
    public class OrderItemViewCollection : EditableDataObjectCollectionBase<OrderItemView>
    {
    }

}
