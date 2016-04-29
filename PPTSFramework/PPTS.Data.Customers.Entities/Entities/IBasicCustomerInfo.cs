using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 最基本的客户信息接口
    /// </summary>
    public interface IBasicCustomerInfo
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 客户编码
        /// </summary>
        string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        string CustomerName
        {
            get;
            set;
        }
    }
}
