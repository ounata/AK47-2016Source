using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Models
{
    [DataContract]
    public class CustomerTearcherRelationQueryModel
    {
        public CustomerTearcherRelationQueryModel()
        {
            IsContainCustomerInfo = false;
        }
        /// <summary>
        /// 学员ID检索条件
        /// </summary>
        [ConditionMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        { get; set; }
        /// <summary>
        /// 学科检索条件
        /// </summary>
        [ConditionMapping("Subject")]
        [DataMember]
        public string Subject
        { get; set; }

        /// <summary>
        /// 年级检索条件
        /// </summary>
        [ConditionMapping("Grade")]
        [DataMember]
        public string Grade
        { get; set; }

        /// <summary>
        ///是否获取客户信息 ,默认值false
        /// </summary>
        [NoMapping]
        [DataMember]
        public bool IsContainCustomerInfo { get; set; }
    }
}
