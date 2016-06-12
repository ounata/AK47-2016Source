using System;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class PotentialCustomerQueryCriteriaModel
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        [InConditionMapping("OrgID")]
        public string[] OrgIds { get; set; }

        /// <summary>
        /// 学员姓名、编号、家长姓名、联系方式全文检索
        /// </summary>
        [NoMapping]
        public string Keyword { get; set; }

        /// <summary>
        /// 入学年级
        /// </summary>
        [InConditionMapping("EntranceGrade")]
        public int[] EntranceGrades { get; set; }

        /// <summary>
        /// 建档日期起
        /// </summary>
        [ConditionMapping("CreateTime", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime CreateTimeStart { get; set; }

        /// <summary>
        /// 建档日期止
        /// </summary>
        [ConditionMapping("CreateTime", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime CreateTimeEnd { get; set; }

        /// <summary>
        /// 跟进阶段
        /// </summary>
        [InConditionMapping("FollowStage")]
        public int[] FollowStages { get; set; }

        /// <summary>
        /// 客户级别
        /// </summary>
        [InConditionMapping("CustomerLevel")]
        public int[] CustomerLevels { get; set; }

        /// <summary>
        /// 最后一次跟进时间
        /// </summary>
        [ConditionMapping("FollowTime", UtcTimeToLocal = true, Operation = "<")]
        public DateTime FollowTime { get; set; }

        /// <summary>
        /// 在读学校
        /// </summary>
        [NoMapping]
        public string SchoolName { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        [NoMapping]
        public string AddressDetail { get; set; }

        /// <summary>
        /// 信息来源一级
        /// </summary>
        [InConditionMapping("SourceMainType")]
        public int[] SourceMainType { get; set; }

        /// <summary>
        /// 信息来源二级
        /// </summary>
        [InConditionMapping("SourceSubType")]
        public int[] SourceSubType { get; set; }

        /// <summary>
        /// 归属坐席是否分配
        /// </summary>
        [NoMapping]
        public string IsAssignCallcenter { get; set; }

        /// <summary>
        /// 归属坐席是否分配
        /// </summary>
        [NoMapping]
        public string IsAssignCallcenterStatus { get { return IsAssignCallcenter == "-1" ? "" : IsAssignCallcenter; } set { } }

        /// <summary>
        /// 咨询师是否分配
        /// </summary>
        [NoMapping]
        public string IsAssignConsultant { get; set; }

        /// <summary>
        /// 咨询师是否分配
        /// </summary>
        [NoMapping]
        public string IsAssignConsultantStatus { get { return IsAssignConsultant == "-1" ? "" : IsAssignConsultant; } set { } }

        /// <summary>
        /// 市场专员是否分配
        /// </summary>
        [NoMapping]
        public string IsAssignMarket { get; set; }

        /// <summary>
        /// 咨询师是否分配
        /// </summary>
        [NoMapping]
        public string IsAssignMarketStatus { get { return IsAssignMarket == "-1" ? "" : IsAssignMarket; } set { } }

        /// <summary>
        /// 坐席姓名
        /// </summary>
        [NoMapping]
        public string CallcenterName { get; set; }

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [NoMapping]
        public string ConsultantName { get; set; }

        /// <summary>
        /// 市场专员姓名
        /// </summary>
        [NoMapping]
        public string MarketName { get; set; }

        /// <summary>
        /// 建档人姓名
        /// </summary>
        [ConditionMapping("CreatorName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CreatorName { get; set; }

        /// <summary>
        /// 有效/无效客户
        /// </summary>
        [NoMapping]
        public string IsValids { get; set; }

        /// <summary>
        /// 客户状态
        /// </summary>
        [NoMapping]
        public string CustomerStatus { get { return IsValids == "-1" ? "" : IsValids; } set { } }

        [NoMapping]
        public PageRequestParams PageParams
        {
            get;
            set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get;
            set;
        }
    }
}