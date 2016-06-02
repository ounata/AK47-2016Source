using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a Asset.
	/// 订购资产表（需要快照）
	/// </summary>
	[Serializable]
    [ORTableMapping("OM.Assets", "OM.Assets_Current")]
    [DataContract]
	public class Asset : IEntityWithCreator, IEntityWithModifier, IVersionDataObjectWithoutID
    {		
		public Asset()
		{
		}

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
        public string AssetType
        {
            get;
            set;
        }

        /// <summary>
        /// 资产来源（0-订单）
        /// </summary>
        [ORFieldMapping("AssetRefType")]
        [DataMember]
        public string AssetRefType
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
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        [ORFieldMapping("CustomerCode")]
        [DataMember]
        public string CustomerCode { get; set; }

        [ORFieldMapping("CustomerName")]
        [DataMember]
        public string CustomerName { get; set; }

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
        /// 产品分类代码
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
        /// 产品类型代码
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
        /// 课次时长名称
        /// </summary>
        [ORFieldMapping("LessonDurationValue")]
        [DataMember]
        public decimal LessonDurationValue
        {
            get;
            set;
        }

        /// <summary>
        /// 订购的原始单价
        /// </summary>
        [ORFieldMapping("OrderPrice")]
        [DataMember]
        public decimal OrderPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 订购的数量
        /// </summary>
        [ORFieldMapping("OrderAmount")]
        [DataMember]
        public decimal OrderAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 赠送的数量
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
        /// 折扣类型（参考订购明细）
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
        /// 订购的实际单价
        /// </summary>
        [ORFieldMapping("RealPrice")]
        [DataMember]
        public decimal RealPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 订购的实际数量
        /// </summary>
        [ORFieldMapping("RealAmount")]
        [DataMember]
        public decimal RealAmount
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
        /// 累计已排数量（课程资产用，排课+，取消-，确认-）
        /// </summary>
        [ORFieldMapping("AssignedAmount")]
        [DataMember]
        public decimal AssignedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 累计确认数量（即已上数量，课程资产用，确认+，删除-）
        /// </summary>
        [ORFieldMapping("ConfirmedAmount")]
        [DataMember]
        public decimal ConfirmedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 累计已兑换数量（课程资产用）
        /// </summary>
        [ORFieldMapping("ExchangedAmount")]
        [DataMember]
        public decimal ExchangedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 累计已退数量（课程资产用，退订+）
        /// </summary>
        [ORFieldMapping("DebookedAmount")]
        [DataMember]
        public decimal DebookedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 累计已确认金额（课程资产与非课程资产用，确认+，删除-）
        /// </summary>
        [ORFieldMapping("ConfirmedMoney")]
        [DataMember]
        public decimal ConfirmedMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 累计返还金额（课程资产用，买赠退订时使用）
        /// </summary>
        [ORFieldMapping("ReturnedMoney")]
        [DataMember]
        public decimal ReturnedMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余资产数量（未上的数量，确认-，删除+）
        /// </summary>
        [ORFieldMapping("Amount")]
        [DataMember]
        public decimal Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余资产单价（针对课时资产由于退订可能与订购时不同）
        /// </summary>
        [ORFieldMapping("Price")]
        [DataMember]
        public decimal Price
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
		[ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()")]
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
		[ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
		public DateTime ModifyTime
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


        /// <summary>
        /// 版本开始时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionStartTime", PrimaryKey = true, UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 版本结束时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionEndTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionEndTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class AssetCollection : EditableDataObjectCollectionBase<Asset>
    {
    }
}