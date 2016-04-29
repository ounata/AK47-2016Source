using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 客户跟进的汇总信息，该信息会更新到学员或潜客的主表当中
    /// </summary>
    [DataContract]
    public class CustomerFollowSummary : IVersionDataObjectWithoutID
    {
        [ORFieldMapping("CustomerID", PrimaryKey = true)]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        [ORFieldMapping("PurchaseIntention")]
        [DataMember]
        public PurchaseIntentionDefine PurchaseIntention
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进时间
        /// </summary>
        [ORFieldMapping("FollowTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime FollowTime
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进阶段代码
        /// </summary>
        [ORFieldMapping("FollowStage")]
        [DataMember]
        public SalesStageType FollowStage
        {
            get;
            set;
        }

        /// <summary>
        /// 预计下次跟进时间
        /// </summary>
        [ORFieldMapping("NextFollowTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime NextFollowTime
        {
            get;
            set;
        }

        /// <summary>
        /// 客户级别代码
        /// </summary>
        [ORFieldMapping("CustomerLevel")]
        [DataMember]
        public string CustomerLevel
        {
            get;
            set;
        }

        [ORFieldMapping("FollowedCount")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public int FollowedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 版本开始时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionStartTime", PrimaryKey = true, UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 版本结束时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionEndTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionEndTime
        {
            get;
            set;
        }
    }

    [DataContract]
    public class CustomerFollowSummaryCollection : EditableDataObjectCollectionBase<CustomerFollowSummary>
    {
    }
}
