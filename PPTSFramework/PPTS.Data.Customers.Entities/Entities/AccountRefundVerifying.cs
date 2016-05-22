using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a AccountRefundVerifying.
	/// 账户退费确认表
	/// </summary>
	[Serializable]
    [ORTableMapping("CM.AccountRefundVerifyings")]
    [DataContract]
	public class AccountRefundVerifying : IEntityWithCreator
    {		
		public AccountRefundVerifying()
		{
		}		

		/// <summary>
		/// 申请单ID
		/// </summary>
		[ORFieldMapping("ApplyID")]
        [DataMember]
		public string ApplyID
		{
			get;
            set;
		}

		/// <summary>
		/// 确认ID
		/// </summary>
		[ORFieldMapping("VerifyID", PrimaryKey=true)]
        [DataMember]
		public string VerifyID
		{
			get;
            set;
		}

		/// <summary>
		/// 确认时间
		/// </summary>
		[ORFieldMapping("VerifyTime")]
        [DataMember]
		public DateTime VerifyTime
		{
			get;
            set;
		}

		/// <summary>
		/// 确认操作（1-分出纳确认，2-分财务确认，3-分区域确认）
		/// </summary>
		[ORFieldMapping("VerifyAction")]
        [ConstantCategory("Account_RefundVerifyAction")]
        [DataMember]
		public RefundVerifyAction VerifyAction
		{
			get;
            set;
		}

		/// <summary>
		/// 确认说明
		/// </summary>
		[ORFieldMapping("VerifyMemo")]
        [DataMember]
		public string VerifyMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人ID
		/// </summary>
		[ORFieldMapping("VerifierID")]
        [DataMember]
		public string VerifierID
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人姓名
		/// </summary>
		[ORFieldMapping("VerifierName")]
        [DataMember]
		public string VerifierName
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人岗位ID
		/// </summary>
		[ORFieldMapping("VerifierJobID")]
        [DataMember]
		public string VerifierJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人岗位名称
		/// </summary>
		[ORFieldMapping("VerifierJobName")]
        [DataMember]
		public string VerifierJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人组织机构ID
		/// </summary>
		[ORFieldMapping("VerifierOrgID")]
        [DataMember]
		public string VerifierOrgID
		{
			get;
            set;
		}

		/// <summary>
		/// 确认人组织机构名称
		/// </summary>
		[ORFieldMapping("VerifierOrgName")]
        [DataMember]
		public string VerifierOrgName
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
    public class AccountRefundVerifyingCollection : EditableDataObjectCollectionBase<AccountRefundVerifying>
    {
    }
}