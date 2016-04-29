using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a OrderItem.
	/// 订购明细表
	/// </summary>
	[Serializable]
    [ORTableMapping("OM.OrderItems")]
    [DataContract]
	public class OrderItem
	{		
		public OrderItem()
		{
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
		[ORFieldMapping("ItemID", PrimaryKey=true)]
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
    public class OrderItemCollection : EditableDataObjectCollectionBase<OrderItem>
    {
    }
}