using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.DataObjects
{
    /// <summary>
    /// 三态的布尔状态
    /// </summary>
    public enum BooleanState
    {
        /// <summary>
        /// 未知状态
        /// </summary>
        [EnumItemDescription("未知")]
        Unknown = -1,

        /// <summary>
        /// 假
        /// </summary>
        [EnumItemDescription("假")]
        False = 0,

        /// <summary>
        /// 真
        /// </summary>
        [EnumItemDescription("真")]
        True = 1
    }
}

