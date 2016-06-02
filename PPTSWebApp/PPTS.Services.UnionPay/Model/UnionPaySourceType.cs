using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.Services.UnionPay.Model
{
    public enum UnionPaySourceType
    {
        /// <summary>
        /// 1--接口(实时接口)来源
        /// </summary>
        Sync = 1,

        /// <summary>
        /// 2--对账(异步接口)来源
        /// </summary>
        Async = 2
    }
}