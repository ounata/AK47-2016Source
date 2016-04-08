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
	/// This object represents the properties and methods of a Assign.
	/// 排课表
	/// </summary>
	[Serializable]
    [ORTableMapping("Assigns")]
    [DataContract]
	public class Assign
	{		
		public Assign()
		{
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
		[ORFieldMapping("AssignTime")]
        [DataMember]
		public DateTime AssignTime
		{
			get;
            set;
		}

		/// <summary>
		/// 排课状态（排定，已上，异常，无效）
		/// </summary>
		[ORFieldMapping("AssignStatus")]
        [DataMember]
		public string AssignStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 排课来源（自动【班组】，手工【一对一】，补录）
		/// </summary>
		[ORFieldMapping("AssignSource")]
        [DataMember]
		public string AssignSource
		{
			get;
            set;
		}

		/// <summary>
		/// 异常原因
		/// </summary>
		[ORFieldMapping("AssignMemo")]
        [DataMember]
		public string AssignMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 是否允许复制
		/// </summary>
		[ORFieldMapping("CopyAllowed")]
        [DataMember]
		public int CopyAllowed
		{
			get;
            set;
		}

		/// <summary>
		/// 确认状态（0-未确认，1-已确认，3-已删除，4-部分确认）
		/// </summary>
		[ORFieldMapping("ConfirmStatus")]
        [DataMember]
		public string ConfirmStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 确认时间
		/// </summary>
		[ORFieldMapping("ConfirmTime")]
        [DataMember]
		public DateTime ConfirmTime
		{
			get;
            set;
		}

		/// <summary>
		/// 确认ID
		/// </summary>
		[ORFieldMapping("ConfirmID")]
        [DataMember]
		public string ConfirmID
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
		/// 教室ID
		/// </summary>
		[ORFieldMapping("RoomID")]
        [DataMember]
		public string RoomID
		{
			get;
            set;
		}

		/// <summary>
		/// 教室编码
		/// </summary>
		[ORFieldMapping("RoomCode")]
        [DataMember]
		public string RoomCode
		{
			get;
            set;
		}

		/// <summary>
		/// 教室名称
		/// </summary>
		[ORFieldMapping("RoomName")]
        [DataMember]
		public string RoomName
		{
			get;
            set;
		}

		/// <summary>
		/// 教师ID
		/// </summary>
		[ORFieldMapping("TeacherID")]
        [DataMember]
		public string TeacherID
		{
			get;
            set;
		}

		/// <summary>
		/// 教师编码
		/// </summary>
		[ORFieldMapping("TeacherCode")]
        [DataMember]
		public string TeacherCode
		{
			get;
            set;
		}

		/// <summary>
		/// 教师姓名
		/// </summary>
		[ORFieldMapping("TeacherName")]
        [DataMember]
		public string TeacherName
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
		/// 年级代码
		/// </summary>
		[ORFieldMapping("Grade")]
        [DataMember]
		public string Grade
		{
			get;
            set;
		}

		/// <summary>
		/// 年级名称
		/// </summary>
		[ORFieldMapping("GradeName")]
        [DataMember]
		public string GradeName
		{
			get;
            set;
		}

		/// <summary>
		/// 科目代码
		/// </summary>
		[ORFieldMapping("Subject")]
        [DataMember]
		public string Subject
		{
			get;
            set;
		}

		/// <summary>
		/// 科目名称
		/// </summary>
		[ORFieldMapping("SubjectName")]
        [DataMember]
		public string SubjectName
		{
			get;
            set;
		}

		/// <summary>
		/// 时长（分钟）【班组就是课次时长，一对一就是课时时常】
		/// </summary>
		[ORFieldMapping("DurationValue")]
        [DataMember]
		public decimal DurationValue
		{
			get;
            set;
		}

		/// <summary>
		/// 数量（实际时间除以时长）
		/// </summary>
		[ORFieldMapping("Amount")]
        [DataMember]
		public decimal Amount
		{
			get;
            set;
		}

		/// <summary>
		/// 单价
		/// </summary>
		[ORFieldMapping("Price")]
        [DataMember]
		public decimal Price
		{
			get;
            set;
		}

		/// <summary>
		/// 开始时间
		/// </summary>
		[ORFieldMapping("StartTime")]
        [DataMember]
		public DateTime StartTime
		{
			get;
            set;
		}

		/// <summary>
		/// 结束时间
		/// </summary>
		[ORFieldMapping("EndTime")]
        [DataMember]
		public DateTime EndTime
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
    public class AssignCollection : EditableDataObjectCollectionBase<Assign>
    {
    }
}