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
	/// This object represents the properties and methods of a ClassLessonItem.
	/// 班组班级课次明细表
	/// </summary>
	[Serializable]
    [ORTableMapping("OM.ClassLessonItems")]
    [DataContract]
	public class ClassLessonItem : IEntityWithCreator, IEntityWithModifier
    {		
		public ClassLessonItem()
		{
		}		

		/// <summary>
		/// 课次ID
		/// </summary>
		[ORFieldMapping("LessonID")]
        [DataMember]
		public string LessonID
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
		/// 排课ID
		/// </summary>
		[ORFieldMapping("AssignID", PrimaryKey=true)]
        [DataMember]
		public string AssignID
		{
			get;
            set;
		}

		/// <summary>
		/// 排定时间
		/// </summary>
		[ORFieldMapping("AssignTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()")]
        [DataMember]
		public DateTime AssignTime
		{
			get;
            set;
		}

		/// <summary>
		/// 排定状态（参考排课表）
		/// </summary>
		[ORFieldMapping("AssignStatus")]
        [DataMember]
		public AssignStatusDefine AssignStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 确认状态（参考排课表）
		/// </summary>
		[ORFieldMapping("ConfirmStatus")]
        [DataMember]
		public ConfirmStatusDefine ConfirmStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 确认时间
		/// </summary>
		[ORFieldMapping("ConfirmTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()")]
        [DataMember]
		public DateTime ConfirmTime
		{
			get;
            set;
		}

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
		/// 学员所在校区ID
		/// </summary>
		[ORFieldMapping("CustomerCampusID")]
        [DataMember]
		public string CustomerCampusID
		{
			get;
            set;
		}

		/// <summary>
		/// 学员所在校区名称
		/// </summary>
		[ORFieldMapping("CustomerCampusName")]
        [DataMember]
		public string CustomerCampusName
		{
			get;
            set;
		}

        [ORFieldMapping("CustomerGrade")]
        [DataMember]
        public string CustomerGrade { get; set; }

        [ORFieldMapping("CustomerGradeName")]
        [DataMember]
        public string CustomerGradeName { get; set; }

        /// <summary>
        /// 是否插班
        /// </summary>
        [ORFieldMapping("IsJoinClass")]

        [DataMember]
		public int IsJoinClass
		{
			get;
            set;
		}

		/// <summary>
		/// 是否外校
		/// </summary>
		[ORFieldMapping("IsOuterCampus")]
        [DataMember]
		public int IsOuterCampus
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
		/// 创建人ID
		/// </summary>
		[ORFieldMapping("CreatorID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
		public DateTime CreateTime
		{
			get;
            set;
		}

		/// <summary>
		/// 最后修改人
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
	}

    [Serializable]
    [DataContract]
    public class ClassLessonItemCollection : EditableDataObjectCollectionBase<ClassLessonItem>
    {
    }
}