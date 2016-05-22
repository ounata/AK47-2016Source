using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Models
{
    /// <summary>
    /// 是否发生课时模型
    /// </summary>
    [DataContract]
    public class HasPeriodCourseQueryResult
    {
        /// <summary>
        /// 期限内是否发生课时
        /// </summary>
        [DataMember]
        public bool HasPeriodInside
        { get; set; }

        /// <summary>
        /// 期限外是否发生课时
        /// </summary>
        [DataMember]
        public bool HasPeriodOutside { get; set; }
    }
}
