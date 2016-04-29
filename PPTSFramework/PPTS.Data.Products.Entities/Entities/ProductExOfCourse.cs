using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Runtime.Serialization;

namespace PPTS.Data.Products.Entities
{

    /// <summary>
    /// 产品班组属性
    /// </summary>
    [Serializable]
    [ORTableMapping("PM.ProductsExOfCourse")]
    [DataContract]
    public class ProductExOfCourse
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [ORFieldMapping("ProductID")]
        [DataMember]
        public string ProductID { set; get; }

        /// <summary>
        /// 课次数量（班组用）
        /// </summary>
        [ORFieldMapping("LessonCount")]
        [DataMember]
        public int LessonCount { set; get; }

        /// <summary>
        /// 课次时长代码（班组用）
        /// </summary>
        [ORFieldMapping("LessonDuration")]
        [DataMember]
        public int LessonDuration { set; get; }

        /// <summary>
        /// 课次时长（班组用）
        /// </summary>
        [ORFieldMapping("LessonDurationValue")]
        [DataMember]
        public Decimal LessonDurationValue { set; get; }

        /// <summary>
        /// 课时时长代码 （通用）
        /// </summary>
        [ORFieldMapping("PeriodDuration")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_BO_ProductDuration")]
        public string PeriodDuration { set; get; }

        /// <summary>
        /// 课时时长 （通用）
        /// </summary>
        [ORFieldMapping("PeriodDurationValue")]
        [DataMember]
        public int PeriodDurationValue { set; get; }

        /// <summary>
        /// 课次课时数 （班组用）
        /// </summary>
        [ORFieldMapping("PeriodsOfLesson")]
        [DataMember]
        public int PeriodsOfLesson { set; get; }

        /// <summary>
        /// 课程级别代码（普通、1A、3A）  【班组和一对一不一样，考虑拉平】（通用）
        /// </summary>
        [ORFieldMapping("CourseLevel")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_CourseLevel")]
        public string CourseLevel { set; get; }

        /// <summary>
        /// 辅导类型代码（常规，自主招生）（班组用）
        /// </summary>
        [ORFieldMapping("CoachType")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_CoachType")]
        public string CoachType { set; get; }

        /// <summary>
        /// 班组类型代码（长期，阶段性）（班组用）
        /// </summary>
        [ORFieldMapping("GroupType")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_GroupType")]
        public string GroupType { set; get; }

        /// <summary>
        /// 班级类型代码（大班，小班）（班组用）
        /// </summary>
        [ORFieldMapping("ClassType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Product_GroupClassType")]
        public string ClassType { set; get; }

        /// <summary>
        /// 开班人数（班组用）
        /// </summary>
        [ORFieldMapping("MinPeoples")]
        [DataMember]
        public int MinPeoples { set; get; }

        /// <summary>
        /// 满班人数（班组用）
        /// </summary>
        [ORFieldMapping("MaxPeoples")]
        [DataMember]
        public int MaxPeoples { set; get; }

        /// <summary>
        /// 跨校区收入归属（C表示学员，T表示老师）（班组用）
        /// </summary>
        [ORFieldMapping("IncomeBelonging")]
        [DataMember]
        public string IncomeBelonging { set; get; }

        /// <summary>
        /// 是否跨校区班组（班组用）
        /// </summary>
        [ORFieldMapping("IsCrossCampus")]
        [DataMember]
        public int IsCrossCampus { set; get; }
    }

    [Serializable]
    [DataContract]
    public class ProductExOfCourseCollection : EditableDataObjectCollectionBase<ProductExOfCourse>
    {
    }



}
