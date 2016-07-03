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
    [Serializable]
    [ORTableMapping("PM.v_ProductClassStats")]
    [DataContract]
    public class OrderClassGroupProductView
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
        /// 分类代码
        /// </summary>
        [ORFieldMapping("Catalog")]
        [DataMember]
        public string Catalog { set; get; }

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
        /// 颗粒度
        /// </summary>
        [ORFieldMapping("ProductUnit")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Product_ProductUnit")]
        public string ProductUnit { set; get; }


        /// <summary>
        /// 单价
        /// </summary>
        [ORFieldMapping("ProductPrice")]
        [DataMember]
        public decimal ProductPrice { set; get; }


        /// <summary>
        /// ClassCount
        /// </summary>		
        [ORFieldMapping("ClassCount")]
        [DataMember]
        public int ClassCount
        {
            get;
            set;
        }
        /// <summary>
        /// ValidClasses
        /// </summary>		
        [ORFieldMapping("ValidClasses")]
        [DataMember]
        public int ValidClasses
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许买赠折扣
        /// </summary>
        [ORFieldMapping("TunlandAllowed")]
        [DataMember]
        public int TunlandAllowed { set; get; }


        /// <summary>
        /// 前端展示使用
        /// </summary>
        [NoMapping]
        [DataMember]
        public bool Highlight { get { return TunlandAllowed == 0; } }

    }


    [Serializable]
    [DataContract]
    public class OrderClassGroupProductViewCollection : EditableDataObjectCollectionBase<OrderClassGroupProductView>
    {
    }
}
