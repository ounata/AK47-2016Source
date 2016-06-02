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
    /// This object represents the properties and methods of a CustomerTransferResource.
    /// 客户划转资源表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerTransferResources")]
    [DataContract]
    public class CustomerTransferResource : Common.Entities.IEntityWithCreator
    {
        public CustomerTransferResource()
        {
        }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        [ORFieldMapping("OrgID")]
        [DataMember]
        public string OrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        [ORFieldMapping("OrgName")]
        [DataMember]
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 组织机构类型
        /// </summary>
        [ORFieldMapping("OrgType")]
        [DataMember]
        public string OrgType
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
        /// 划转ID
        /// </summary>
        [ORFieldMapping("TransferID", PrimaryKey = true)]
        [DataMember]
        public string TransferID
        {
            get;
            set;
        }

        /// <summary>
        /// 划转时间
        /// </summary>
        [ORFieldMapping("TransferTime")]
        [DataMember]
        public DateTime TransferTime
        {
            get;
            set;
        }

        /// <summary>
        /// 划转原因
        /// </summary>
        [ORFieldMapping("TransferMemo")]
        [DataMember]
        public string TransferMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 划转人ID
        /// </summary>
        [ORFieldMapping("TransferorID")]
        [DataMember]
        public string TransferorID
        {
            get;
            set;
        }

        /// <summary>
        /// 划转人姓名
        /// </summary>
        [ORFieldMapping("TransferorName")]
        [DataMember]
        public string TransferorName
        {
            get;
            set;
        }

        /// <summary>
        /// 划转人岗位ID
        /// </summary>
        [ORFieldMapping("TransferorJobID")]
        [DataMember]
        public string TransferorJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 划转人岗位名称
        /// </summary>
        [ORFieldMapping("TransferorJobName")]
        [DataMember]
        public string TransferorJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 转至校区ID
        /// </summary>
        [ORFieldMapping("ToOrgID")]
        [DataMember]
        public string ToOrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 转至校区名称
        /// </summary>
        [ORFieldMapping("ToOrgName")]
        [DataMember]
        public string ToOrgName
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
    public class CustomerTransferResourceCollection : EditableDataObjectCollectionBase<CustomerTransferResource>
    {
    }
}