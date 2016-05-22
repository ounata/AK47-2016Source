using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using System;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 潜在客户表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.PotentialCustomers", "CM.PotentialCustomers_Current")]
    [DataContract]
    public class PotentialCustomer : CustomerBase
    {
        public PotentialCustomer()
        {
        }

        /// <summary>
        /// 归属组织机构类型
        /// </summary>
        [ORFieldMapping("OrgType")]
        [DataMember]
        public OrgTypeDefine OrgType
        {
            get;
            set;
        }

        /// <summary>
        /// 无效客户理由代码（参考跟进）
        /// </summary>
        [ORFieldMapping("InvalidReason")]
        [DataMember]
        public string InvalidReason
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class PotentialCustomerCollection : EditableDataObjectCollectionBase<PotentialCustomer>
    {
    }
}