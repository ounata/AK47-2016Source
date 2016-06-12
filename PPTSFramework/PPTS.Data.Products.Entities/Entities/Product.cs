using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Runtime.Serialization;

namespace PPTS.Data.Products.Entities
{
    /// <summary>
    /// 产品
    /// </summary>
    [Serializable]
    [ORTableMapping("PM.Products")]
    [DataContract]
    public class Product: IEntityWithCreator, IEntityWithModifier
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [ORFieldMapping("ProductID", PrimaryKey = true)]
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
        public string ProductCode { set; get; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [ORFieldMapping("ProductName")]
        [DataMember]
        public string ProductName { set; get; }


        /// <summary>
        /// 产品描述
        /// </summary>
        [ORFieldMapping("ProductMemo")]
        [DataMember]
        public string ProductMemo { set; get; }

        /// <summary>
        /// 状态（草稿，审批中，已完成，已拒绝）
        /// </summary>
        [ORFieldMapping("ProductStatus")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Product_Status")]
        public ProductStatus ProductStatus { set; get; }

        /// <summary>
        /// 单价
        /// </summary>
        [ORFieldMapping("ProductPrice")]
        [DataMember]
        public decimal ProductPrice { set; get; }

        /// <summary>
        /// 成本
        /// </summary>
        [ORFieldMapping("ProductCost")]
        [DataMember]
        public decimal ProductCost { set; get; }

        /// <summary>
        /// 颗粒度
        /// </summary>
        [ORFieldMapping("ProductUnit")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_ProductUnit")]
        public ProductUnit ProductUnit { set; get; }

        /// <summary>
        /// 目标价格
        /// </summary>
        [ORFieldMapping("TargetPrice")]
        [DataMember]
        public decimal TargetPrice { set; get; }

        /// <summary>
        /// 目标价格描述
        /// </summary>
        [ORFieldMapping("TargetPriceMemo")]
        [DataMember]
        public string TargetPriceMemo { set; get; }


        /// <summary>
        /// 分类代码
        /// </summary>
        [ORFieldMapping("Catalog")]
        [DataMember]
        public string Catalog { set; get; }

        /// <summary>
        /// 科目代码
        /// </summary>
        [ORFieldMapping("Subject")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_BO_Product_TeacherSubject")]
        public string Subject { set; get; }

        /// <summary>
        /// 年级代码
        /// </summary>
        [ORFieldMapping("Grade")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_CUSTOMER_GRADE")]
        public string Grade { set; get; }

        /// <summary>
        /// 季度代码
        /// </summary>
        [ORFieldMapping("Season")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_Season")]
        public string Season { set; get; }

        /// <summary>
        /// 启售日期
        /// </summary>
        [ORFieldMapping("StartDate", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags =ClauseBindingFlags.All)]
        [DataMember]
        public DateTime StartDate { set; get; }

        /// <summary>
        /// 停售日期
        /// </summary>
        [ORFieldMapping("EndDate", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All)]
        [DataMember]
        public DateTime EndDate { set; get; }

        /// <summary>
        /// 给合作方分成比率
        /// </summary>
        [ORFieldMapping("PartnerRatio")]
        [DataMember]
        public decimal PartnerRatio { set; get; }

        /// <summary>
        /// 合作方ID
        /// </summary>
        [ORFieldMapping("PartnerID")]
        [DataMember]
        public string PartnerID { set; get; }

        /// <summary>
        /// 合作方名称
        /// </summary>
        [ORFieldMapping("PartnerName")]
        [DataMember]
        public string PartnerName { set; get; }

        /// <summary>
        /// 是否允许拓路折扣
        /// </summary>
        [ORFieldMapping("SpecialAllowed")]
        [DataMember]
        public int SpecialAllowed { set; get; }

        /// <summary>
        /// 是否允许买赠折扣
        /// </summary>
        [ORFieldMapping("TunlandAllowed")]
        [DataMember]
        public int TunlandAllowed { set; get; }

        /// <summary>
        /// 是否允许促销优惠
        /// </summary>
        [ORFieldMapping("PresentAllowed")]
        [DataMember]
        public int PresentAllowed { set; get; }

        /// <summary>
        /// 促销优惠限额
        /// </summary>
        [ORFieldMapping("PromotionAllowed")]
        [DataMember]
        public int PromotionAllowed { set; get; }

        /// <summary>
        /// 所有者组织ID
        /// </summary>
        [ORFieldMapping("PromotionQuota")]
        [DataMember]
        public decimal PromotionQuota { set; get; }


        /// <summary>
        /// 收入确认开始时间
        /// </summary>
        [ORFieldMapping("ConfirmStartDate", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime ConfirmStartDate { set; get; }

        /// <summary>
        /// 收入确认结束时间
        /// </summary>
        [ORFieldMapping("ConfirmEndDate", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime ConfirmEndDate { set; get; }



        /// <summary>
        /// 提交人ID
        /// </summary>
        [ORFieldMapping("SubmitterID")]
        [DataMember]
        public string SubmitterID { set; get; }

        /// <summary>
        /// 提交人姓名
        /// </summary>
        [ORFieldMapping("SubmitterName")]
        [DataMember]
        public string SubmitterName { set; get; }

        /// <summary>
        /// 提交人岗位ID
        /// </summary>
        [ORFieldMapping("SubmitterJobID")]
        [DataMember]
        public string SubmitterJobID { set; get; }

        /// <summary>
        /// 提交人岗位名称
        /// </summary>
        [ORFieldMapping("SubmitterJobName")]
        [DataMember]
        public string SubmitterJobName { set; get; }

        /// <summary>
        /// 提交时间
        /// </summary>
        [ORFieldMapping("SubmitTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where, DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime SubmitTime { set; get; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorID { set; get; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorName { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [ORFieldMapping("ModifierID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string ModifierID { set; get; }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string ModifierName { set; get; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [DataMember]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        public DateTime ModifyTime { set; get; }
    }

    [Serializable]
    [DataContract]
    public class ProductCollection : EditableDataObjectCollectionBase<Product>
    {
    }
}
