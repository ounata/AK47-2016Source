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
    /// This object represents the properties and methods of a CustomerReply.
    /// 学大对学员反馈回复表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerReplies")]
    [DataContract]
    public class CustomerReply
    {
        public CustomerReply()
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
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }
        [ORFieldMapping("ParentPassportID")]
        [DataMember]
        public string ParentPassportID
        {
            set; get;
        }
        /// <summary>
		/// 学大沟通对象代码
        /// Inquiry--咨询师反馈(1);
        /// MngTeacher--学管师反馈(2);
        /// Teacher--教师反馈(3);
        /// School--校区反馈(4);
        /// WMng--周反馈(6)
		/// </summary>
		[ORFieldMapping("ReplyObject")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Customer_ReplyObject")]
        public string ReplyObject
        {
            get;
            set;
        }
        /// <summary>
        /// 回复ID
        /// </summary>
        [ORFieldMapping("ReplyID", PrimaryKey = true)]
        [DataMember]
        public string ReplyID
        {
            get;
            set;
        }

        /// <summary>
        /// 回复时间
        /// </summary>
        [ORFieldMapping("ReplyTime")]
        [DataMember]
        public DateTime ReplyTime
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
            set; get;

        }
        /// <summary>
        /// 学员姓名
        /// </summary>
        [ORFieldMapping("CustomerName")]
        [DataMember]
        public string CustomerName
        {
            set; get;

        }
        /// <summary>
        /// 学大响应人岗位ID
        /// </summary>
        [ORFieldMapping("ReplierJobID")]
        [DataMember]
        public string ReplierJobID
        {
            set; get;

        }
        ///// <summary>
        ///// Teacher;School;MngTeacher;Inquiry，Inquiry[咨询师反馈]--1 MngTeacher[学管师反馈]--2 Teacher[教师反馈]--3 School[校区反馈]--4 家长反馈信息--5 WMng[周反馈]--6
        ///// </summary>
        //[ORFieldMapping("ReplyType")]
        //      [DataMember]
        //      [ConstantCategory("c_codE_ABBR_Customer_ReplyType")]
        //public string ReplyType
        //{
        //	get;
        //          set;
        //}

        /// <summary>
        /// 回复内容
        /// </summary>
        [ORFieldMapping("ReplyContent")]
        [DataMember]
        public string ReplyContent
        {
            get;
            set;
        }

        /// <summary>
        /// 回复来源(iOS,Andriod,WebPPTS)
        /// </summary>
        [ORFieldMapping("ReplyFrom")]
        [DataMember]
        public string ReplyFrom
        {
            get;
            set;
        }

        /// <summary>
        /// 回复人ID
        /// </summary>
        [ORFieldMapping("ReplierID")]
        [DataMember]
        public string ReplierID
        {
            get;
            set;
        }

        /// <summary>
        /// 回复人姓名
        /// </summary>
        [ORFieldMapping("ReplierName")]
        [DataMember]
        public string ReplierName
        {
            get;
            set;
        }

        /// <summary>
        /// 家长ID
        /// </summary>
        [ORFieldMapping("ParentID")]
        [DataMember]
        public string ParentID
        {
            get;
            set;
        }

        /// <summary>
        /// 家长姓名
        /// </summary>
        [ORFieldMapping("ParentName")]
        [DataMember]
        public string ParentName
        {
            get;
            set;
        }

        /// <summary>
        /// 家长手机号
        /// </summary>
        [ORFieldMapping("PhoneNumber")]
        [DataMember]
        public string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 发言人（Xueda,Customer)
        /// </summary>
        [ORFieldMapping("Poster")]
        [DataMember]
        public string Poster
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
        /// 分公司ID
        /// </summary>
        [ORFieldMapping("BranchID")]
        [DataMember]
        public string BranchID
        {
            get;
            set;
        }
        /// <summary>
        /// 分公司名称
        /// </summary>
        [ORFieldMapping("BranchName")]
        [DataMember]
        public string BranchName
        {
            get;
            set;
        }

        /// <summary>
        /// delete--0;new--1;normal--2;
        /// </summary>
        [ORFieldMapping("Status")]
        [DataMember]
        public string Status
        {
            set; get;
        }

       
    }

    [Serializable]
    [DataContract]
    public class CustomerReplyCollection : EditableDataObjectCollectionBase<CustomerReply>
    {
    }
}