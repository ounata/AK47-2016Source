using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Passport
{
    /// <summary>
    /// 票据来源
    /// </summary>
    public enum TicketSource
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,

        /// <summary>
        /// 来源于Cookie
        /// </summary>
        FromCookie,

        /// <summary>
        /// 来源于Url
        /// </summary>
        FromUrl,

        /// <summary>
        /// 来源于Header
        /// </summary>
        FromHeader,

        /// <summary>
        /// 来源于表单
        /// </summary>
        FromForm
    }
}
