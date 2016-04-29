using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Customers.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a AccountRefundAllot.
	/// 退费责任分配表
	/// </summary>
	[Serializable]
    [ORTableMapping("CM.AccountRefundAllots")]
    [DataContract]
	public class AccountRefundAllot
	{		
		public AccountRefundAllot()
		{
		}

        /// <summary>
        /// 申请ID
        /// </summary>
        [ORFieldMapping("ApplyID")]
        [DataMember]
        public string ApplyID
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
        /// 责任分配ID
        /// </summary>
        [ORFieldMapping("AllotID", PrimaryKey = true)]
        [DataMember]
        public string AllotID
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
        /// 教师类型（全职，兼职）
        /// </summary>
        [ORFieldMapping("TeacherType")]
        [ConstantCategory("Common_TeacherType")]
        [DataMember]
        public string TeacherType
        {
            get;
            set;
        }

        /// <summary>
        /// 教师OA编码
        /// </summary>
        [ORFieldMapping("TeacherOACode")]
        [DataMember]
        public string TeacherOACode
        {
            get;
            set;
        }

        /// <summary>
        /// 科目
        /// </summary>
        [ORFieldMapping("Subject")]
        [ConstantCategory("c_codE_ABBR_BO_Product_TeacherSubject")]
        [DataMember]
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 产品类型（一对一，班组...）
        /// </summary>
        [ORFieldMapping("CategoryType")]
        [ConstantCategory("c_codE_ABBR_Product_CategoryType")]
        [DataMember]
        public string CategoryType
        {
            get;
            set;
        }

        /// <summary>
        /// 课时
        /// </summary>
        [ORFieldMapping("AllotAmount")]
        [DataMember]
        public decimal AllotAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 金额
        /// </summary>
        [ORFieldMapping("AllotMoney")]
        [DataMember]
        public decimal AllotMoney
        {
            get;
            set;
        }
    }


    [Serializable]
    [DataContract]
    public class AccountRefundAllotCollection : EditableDataObjectCollectionBase<AccountRefundAllot>
    {
    }
}