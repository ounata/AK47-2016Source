using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common;
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
    public class ProductView:Product
    {
        

        /// <summary>
        /// 合作价格
        /// </summary>
        [ORFieldMapping("CooperationPrice")]
        [DataMember]
        public decimal CooperationPrice { set; get; }

        

        /// <summary>
        /// 合作关系
        /// </summary>
        [ORFieldMapping("HasPartner")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_HasPartner")]
        public string HasPartner { set; get; }

        /// <summary>
        /// 是否 课程管理
        /// </summary>
        [ORFieldMapping("HasCourse")]
        [DataMember]
        public int HasCourse { set; get; }

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
        /// 产品分类（二级）
        /// </summary>
        [ORFieldMapping("Category")]
        [DataMember]
        public string Category
        {
            get;
            set;
        }

        /// <summary>
        /// 产品分类（二级）
        /// </summary>
        [ORFieldMapping("CategoryName")]
        [DataMember]
        public string CategoryName
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
        /// 前端展示使用
        /// </summary>
        [NoMapping]
        [DataMember]
        public bool Highlight { get { return TunlandAllowed == 0; } }
    }


    [Serializable]
    [DataContract]
    public class ProductViewCollection : EditableDataObjectCollectionBase<ProductView>
    {

    }


}
