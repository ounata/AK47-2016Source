using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Models
{
    [DataContract]
    public class CustomerCollectionQueryResult
    {
        /// <summary>
        /// 人员集合信息
        /// </summary>
        [DataMember]
        public List<CustomerQueryResult> CustomerCollection { get; set; }
    }
}
