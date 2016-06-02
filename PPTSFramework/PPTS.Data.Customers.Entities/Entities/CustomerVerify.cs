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
    /// This object represents the properties and methods of a CustomerVerify.
    /// 客户上门确认表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerVerifies")]
    [DataContract]
    public class CustomerVerify : IEntityWithCreator
    {
        public CustomerVerify()
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
        /// 上门确认ID
        /// </summary>
        [ORFieldMapping("VerifyID", PrimaryKey = true)]
        [DataMember]
        public string VerifyID
        {
            get;
            set;
        }

        /// <summary>
        /// 上门确认时间
        /// </summary>
        [ORFieldMapping("VerifyTime")]
        [DataMember]
        public DateTime VerifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 上门确认人ID
        /// </summary>
        [ORFieldMapping("VerifierID")]
        [DataMember]
        public string VerifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 上门确认人姓名
        /// </summary>
        [ORFieldMapping("VerifierName")]
        [DataMember]
        public string VerifierName
        {
            get;
            set;
        }

        /// <summary>
        /// 上门确认人岗位ID
        /// </summary>
        [ORFieldMapping("VerifierJobID")]
        [DataMember]
        public string VerifierJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 上门确认人岗位名称
        /// </summary>
        [ORFieldMapping("VerifierJobName")]
        [DataMember]
        public string VerifierJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 实际上门人数代码
        /// </summary>
        [ORFieldMapping("VerifyPeoples")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Customer_CRM_RealCallPersonNum")]
        public string VerifyPeoples
        {
            get;
            set;
        }

        /// <summary>
        /// 上门人员关系代码
        /// </summary>
        [ORFieldMapping("VerifyRelations")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_RealCallPersonRelation")]
        public string VerifyRelations
        {
            get;
            set;
        }

        /// <summary>
        /// 是否邀约（根据是否存在跟进记录判定）
        /// </summary>
        [ORFieldMapping("IsInvited")]
        [DataMember]
        public int IsInvited
        {
            get;
            set;
        }

        /// <summary>
        /// 是否签约（缴费单收款后插入该记录）
        /// </summary>
        [ORFieldMapping("IsSigned")]
        [DataMember]
        public bool IsSigned

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
    }

    [Serializable]
    [DataContract]
    public class CustomerVerifyCollection : EditableDataObjectCollectionBase<CustomerVerify>
    {
    }
}