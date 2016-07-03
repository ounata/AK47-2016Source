using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Search.Models
{
    /// <summary>
    /// 客户信息查询大表模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class CustomerSearchUpdateModel
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        [DataMember]
        public string CustomerID
        { get; set; }

        /// <summary>
        /// 请求执行对象ID
        /// </summary>
        [DataMember]
        public string ObjectID
        { get; set; }

        /// <summary>
        /// 执行类型
        /// </summary>
        [DataMember]
        public CustomerSearchUpdateType Type { get; set; }

        public CustomerSearchUpdateModel()
        {

        }

        public CustomerSearchUpdateModel(string customerID, CustomerSearchUpdateType type)
        {
            this.CustomerID = customerID;
            this.Type = type;
        }

        public CustomerSearchUpdateModel(string customerID, CustomerSearchUpdateType type, string objectID)
            : this(customerID, type)
        {
            this.ObjectID = objectID;
        }
    }
}
