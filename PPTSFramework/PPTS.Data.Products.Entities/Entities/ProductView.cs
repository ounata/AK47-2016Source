using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Entities
{
    /// <summary>
    /// 产品
    /// </summary>
    [Serializable]
    [ORTableMapping("PM.v_Products")]
    [DataContract]
    public class ProductView
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
        [ConstantCategory("c_codE_ABBR_Product_ProductStatus")]
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
        public string ProductUnit { set; get; }

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
        /// 合作价格
        /// </summary>
        [ORFieldMapping("CooperationPrice")]
        [DataMember]
        public decimal CooperationPrice { set; get; }

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
        /// 合作关系
        /// </summary>
        [ORFieldMapping("HasPartner")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_HasPartner")]
        public string HasPartner { set; get; }


        /// <summary>
        /// 启售日期
        /// </summary>
        [ORFieldMapping("StartDate", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime StartDate { set; get; }

        /// <summary>
        /// 停售日期
        /// </summary>
        [ORFieldMapping("EndDate", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime SubmitTime { set; get; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [DataMember]
        public string CreatorID { set; get; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
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
        [DataMember]
        public string ModifierID { set; get; }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [DataMember]
        public string ModifierName { set; get; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime ModifyTime { set; get; }




        /// <summary>
		/// 课次数量（班组用）
		/// </summary>
		[ORFieldMapping("LessonCount")]
        [DataMember]
        public int LessonCount
        {
            get;
            set;
        }

        /// <summary>
        /// 课次时长代码（班组用）
        /// </summary>
        [ORFieldMapping("LessonDuration")]
        [DataMember]
        public string LessonDuration
        {
            get;
            set;
        }

        /// <summary>
        /// 课次时长（班组用）
        /// </summary>
        [ORFieldMapping("LessonDurationValue")]
        [DataMember]
        public decimal LessonDurationValue
        {
            get;
            set;
        }

        /// <summary>
        /// 课时时长代码 （通用）
        /// </summary>
        [ORFieldMapping("PeriodDuration")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_BO_ProductDuration")]
        public string PeriodDuration
        {
            get;
            set;
        }

        /// <summary>
        /// 课时时长 （通用）
        /// </summary>
        [ORFieldMapping("PeriodDurationValue")]
        [DataMember]
        public decimal PeriodDurationValue
        {
            get;
            set;
        }

        /// <summary>
        /// 课次课时数 （班组用）
        /// </summary>
        [ORFieldMapping("PeriodsOfLesson")]
        [DataMember]
        public decimal PeriodsOfLesson
        {
            get;
            set;
        }

        /// <summary>
        /// 课程级别代码（普通、1A、3A）  【班组和一对一不一样，考虑拉平】（通用）
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
        /// 辅导类型代码（常规，自主招生）（班组用）
        /// </summary>
        [ORFieldMapping("CoachType")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_CoachType")]
        public string CoachType
        {
            get;
            set;
        }

        /// <summary>
        /// 班组类型代码（长期，阶段性）（班组用）
        /// </summary>
        [ORFieldMapping("GroupType")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_GroupType")]
        public string GroupType
        {
            get;
            set;
        }

        /// <summary>
        /// 班级类型代码（大班，小班）（班组用）
        /// </summary>
        [ORFieldMapping("ClassType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Product_GroupClassType")]
        public string ClassType
        {
            get;
            set;
        }

        /// <summary>
        /// 开班人数（班组用）
        /// </summary>
        [ORFieldMapping("MinPeoples")]
        [DataMember]
        public int MinPeoples
        {
            get;
            set;
        }

        /// <summary>
        /// 满班人数（班组用）
        /// </summary>
        [ORFieldMapping("MaxPeoples")]
        [DataMember]
        public int MaxPeoples
        {
            get;
            set;
        }

        /// <summary>
        /// 跨校区收入归属（C表示学员，T表示老师）（班组用）
        /// </summary>
        [ORFieldMapping("IncomeBelonging")]
        [DataMember]
        public string IncomeBelonging
        {
            get;
            set;
        }

        /// <summary>
        /// 是否跨校区班组（班组用）
        /// </summary>
        [ORFieldMapping("IsCrossCampus")]
        [DataMember]
        public int IsCrossCampus
        {
            get;
            set;
        }

        [ORFieldMapping("CanInput")]
        [DataMember]
        public int CanInput { set; get; }


        //-----------------------------------
        /// <summary>
        /// 产品类型
        /// </summary>
        [ORFieldMapping("CategoryType")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_CategoryType")]
        public CategoryType CategoryType
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




    }


    [Serializable]
    [DataContract]
    public class ProductViewCollection : EditableDataObjectCollectionBase<ProductView>
    {

    }


}
