using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;


namespace PPTS.Data.Orders.Entities
{
    /// <summary>
    /// 退订视图
    /// </summary>
    [Serializable]
    [ORTableMapping("OM.还未创建视图")]
    [DataContract]
    public class DebookOrderView
    {

        #region Order

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

        [ORFieldMapping("ParentID")]
        [DataMember]
        public string ParentID
        {
            get;
            set;
        }

        [ORFieldMapping("ParentName")]
        [DataMember]
        public string ParentName
        {
            get;
            set;
        }

        #endregion

        #region OrderItem

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

        #endregion

        #region Asset

        /// <summary>
		/// 资产ID
		/// </summary>
		[ORFieldMapping("AssetID")]
        [DataMember]
        public string AssetID
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
        


        #endregion

        #region DebookOrder

        /// <summary>
        /// 退订单ID
        /// </summary>
        [ORFieldMapping("DebookID")]
        [DataMember]
        public string DebookID
        {
            get;
            set;
        }

        /// <summary>
        /// 退订单号
        /// </summary>
        [ORFieldMapping("DebookNo")]
        [DataMember]
        public string DebookNo
        {
            get;
            set;
        }

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
        [SqlBehavior(BindingFlags =ClauseBindingFlags.Select | ClauseBindingFlags.Where )]
        public DateTime DebookTime
        {
            get;
            set;
        }




        #endregion

        #region DebookOrderItem
        

        /// <summary>
		/// 退订数量
		/// </summary>
		[ORFieldMapping("DebookAmount")]
        [DataMember]
        public decimal DebookAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 实际退订金额（买赠的，去掉了买赠返还的）
        /// </summary>
        [ORFieldMapping("DebookMoney")]
        [DataMember]
        public decimal DebookMoney
        {
            get;
            set;
        }

        #endregion

    }
    
    [Serializable]
    [DataContract]
    public class DebookOrderViewCollection : EditableDataObjectCollectionBase<DebookOrderView>
    {
    }

}
