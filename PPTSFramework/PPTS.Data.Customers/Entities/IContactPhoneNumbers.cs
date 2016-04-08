using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 联系方式接口
    /// </summary>
    public interface IContactPhoneNumbers
    {
        /// <summary>
        /// 主要联系方式
        /// </summary>
        string PrimaryPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 辅助联系方式
        /// </summary>
        string SecondaryPhone
        {
            get;
            set;
        }
    }
}
