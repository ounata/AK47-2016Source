using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a CustomerTransferApply.
	/// 学员转学申请表
	/// </summary>
	[Serializable]
    [ORTableMapping("CustomerTransferApplys")]
    [DataContract]
	public class CustomerTransferApply
	{		
		public CustomerTransferApply()
		{
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
		/// 客户ID
		/// </summary>
		[ORFieldMapping("CustomerID")]
        [DataMember]
		public string CustomerID
		{
			get;
            set;
		}

		/// <summary>
		/// 申请ID
		/// </summary>
		[ORFieldMapping("ApplyID", PrimaryKey=true)]
        [DataMember]
		public string ApplyID
		{
			get;
            set;
		}

		/// <summary>
		/// 申请状态（审批中，已完成，已拒绝）
		/// </summary>
		[ORFieldMapping("ApplyStatus")]
        [DataMember]
		public string ApplyStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 申请时间
		/// </summary>
		[ORFieldMapping("ApplyTime")]
        [DataMember]
		public DateTime ApplyTime
		{
			get;
            set;
		}

		/// <summary>
		/// 申请人ID
		/// </summary>
		[ORFieldMapping("ApplyrID")]
        [DataMember]
		public string ApplyrID
		{
			get;
            set;
		}

		/// <summary>
		/// 申请人姓名
		/// </summary>
		[ORFieldMapping("ApplyrName")]
        [DataMember]
		public string ApplyrName
		{
			get;
            set;
		}

		/// <summary>
		/// 申请人岗位ID
		/// </summary>
		[ORFieldMapping("ApplyrJobID")]
        [DataMember]
		public string ApplyrJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 申请人岗位名称
		/// </summary>
		[ORFieldMapping("ApplyrJobName")]
        [DataMember]
		public string ApplyrJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理状态（参考订购）
		/// </summary>
		[ORFieldMapping("ProcessStatus")]
        [DataMember]
		public string ProcessStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理时间
		/// </summary>
		[ORFieldMapping("ProcessTime")]
        [DataMember]
		public DateTime ProcessTime
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理说明
		/// </summary>
		[ORFieldMapping("ProcessMemo")]
        [DataMember]
		public string ProcessMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 转学类型（跨校区转学，跨分公司转学）
		/// </summary>
		[ORFieldMapping("TransferType")]
        [DataMember]
		public string TransferType
		{
			get;
            set;
		}

		/// <summary>
		/// 转至校区ID
		/// </summary>
		[ORFieldMapping("ToCampusID")]
        [DataMember]
		public string ToCampusID
		{
			get;
            set;
		}

		/// <summary>
		/// 转至校区名称
		/// </summary>
		[ORFieldMapping("ToCampusName")]
        [DataMember]
		public string ToCampusName
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人ID
		/// </summary>
		[ORFieldMapping("SubmitterID")]
        [DataMember]
		public string SubmitterID
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人姓名
		/// </summary>
		[ORFieldMapping("SubmitterName")]
        [DataMember]
		public string SubmitterName
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人岗位ID
		/// </summary>
		[ORFieldMapping("SubmitterJobID")]
        [DataMember]
		public string SubmitterJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人岗位名称
		/// </summary>
		[ORFieldMapping("SubmitterJobName")]
        [DataMember]
		public string SubmitterJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 提交时间
		/// </summary>
		[ORFieldMapping("SubmitTime")]
        [DataMember]
		public DateTime SubmitTime
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批人ID
		/// </summary>
		[ORFieldMapping("ApproverID")]
        [DataMember]
		public string ApproverID
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批人姓名
		/// </summary>
		[ORFieldMapping("ApproverName")]
        [DataMember]
		public string ApproverName
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批人岗位ID
		/// </summary>
		[ORFieldMapping("ApproverJobID")]
        [DataMember]
		public string ApproverJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批人岗位名称
		/// </summary>
		[ORFieldMapping("ApproverJobName")]
        [DataMember]
		public string ApproverJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批时间
		/// </summary>
		[ORFieldMapping("ApproveTime")]
        [DataMember]
		public DateTime ApproveTime
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
    public class CustomerTransferApplyCollection : EditableDataObjectCollectionBase<CustomerTransferApply>
    {
    }
}