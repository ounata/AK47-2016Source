using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// 带版本信息的数据实体（没有ID属性，ID由ORMapping时决定）
    /// </summary>
    public interface IVersionDataObjectWithoutID
    {
        /// <summary>
        /// 版本的开始时间
        /// </summary>
		DateTime VersionStartTime { get; }

        /// <summary>
        /// 版本的结束时间
        /// </summary>
		DateTime VersionEndTime { get; }
    }
}
