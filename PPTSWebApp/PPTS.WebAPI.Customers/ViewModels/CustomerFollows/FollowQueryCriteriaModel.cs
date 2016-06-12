using System;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    /// <summary>
    /// 客户跟进记录实体类
    /// </summary>
    [Serializable]
    public class FollowQueryCriteriaModel
    {
        /// <summary>
        /// 跟进ID
        /// </summary>
        [ConditionMapping("a.FollowID")]
        public string FollowId { get; set; }

        /// <summary>
        /// 当前潜客或学员
        /// </summary>
        [ConditionMapping("a.CustomerID")]
        public string CustomerID { get; set; }

        /// <summary>
        /// 跟进对象
        /// </summary>
        [ConditionMapping("a.FollowObject")]
        public string FollowObject { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        [InConditionMapping("a.FollowerName")]
        public string FollowerName { get; set; }

        /// <summary>
        /// 建档人
        /// </summary>
        [InConditionMapping("a.CreatorName")]
        public string CreatorName { get; set; }

        /// <summary>
        /// 多个跟进阶段
        /// </summary>
        [InConditionMapping("a.FollowStage")]
        public string[] FollowStages { get; set; }

        /// <summary>
        /// 跟进阶段
        /// </summary>
        [ConditionMapping("a.FollowStage")]
        public string FollowStage { get; set; }

        /// <summary>
        /// 购买意愿
        /// </summary>
        [InConditionMapping("a.PurchaseIntention")]
        public string[] PurchaseIntentions { get; set; }

        /// <summary>
        /// 购买意愿
        /// </summary>
        [ConditionMapping("a.PurchaseIntention")]
        public string PurchaseIntention { get; set; }

        /// <summary>
        /// 客户级别
        /// </summary>
        [InConditionMapping("a.CustomerLevel")]
        public string[] CustomerLevels { get; set; }

        /// <summary>
        /// 多个跟进方式
        /// </summary>
        [InConditionMapping("a.FollowType")]
        public string[] FollowTypes { get; set; }

        /// <summary>
        /// 跟进方式
        /// </summary>
        [ConditionMapping("a.FollowType")]
        public string FollowType { get; set; }

        /// <summary>
        /// 归属组织机构ID
        /// </summary>
        [InConditionMapping("a.OrgID")]
        public string[] OrgIds { get; set; }

        #region 跟进时间

        /// <summary>
        /// 跟进开始时间
        /// </summary>
        [ConditionMapping("a.FollowTime", Operation = ">=")]
        public DateTime FollowStartTime { get; set; }

        /// <summary>
        /// 跟进结束时间
        /// </summary>
        [ConditionMapping("a.FollowTime", Operation = "<", AdjustDays = 1)]
        public DateTime FollowEndTime { get; set; }

        #endregion

        #region 下次沟通时间

        /// <summary>
        /// 下次沟通开始时间
        /// </summary>
        [ConditionMapping("a.NextFollowTime", Operation = ">=")]
        public DateTime NextTalkStartTime { get; set; }

        /// <summary>
        /// 下次沟通结束时间
        /// </summary>
        [ConditionMapping("a.NextFollowTime", Operation = "<", AdjustDays = 1)]
        public DateTime NextTalkEndTime { get; set; }

        #endregion

        [NoMapping]
        public int IsStudyThere { get; set; }

        /// <summary>
        /// 是否在本机构进行辅导
        /// </summary>
        [ConditionMapping("a.IsStudyThere")]
        public string NoIsStudyThere { get { return IsStudyThere == -1 ? "" : IsStudyThere.ToString(); } }

        #region 预计上门时间

        /// <summary>
        /// 预计上门开始时间
        /// </summary>
        [ConditionMapping("a.PlanVerifyTime", Operation = ">=")]
        public DateTime PlanVerifyStartTime { get; set; }

        /// <summary>
        /// 预计上门结束时间
        /// </summary>
        [ConditionMapping("a.PlanVerifyTime", Operation = "<", AdjustDays = 1)]
        public DateTime PlanVerifyEndTime { get; set; }

        #endregion

        #region 预计签约时间

        /// <summary>
        /// 预计签约开始时间
        /// </summary>
        [ConditionMapping("a.PlanSignDate", Operation = ">=")]
        public DateTime PlanSignStartTime { get; set; }

        /// <summary>
        /// 预计签约结束时间
        /// </summary>
        [ConditionMapping("a.PlanSignDate", Operation = "<", AdjustDays = 1)]
        public DateTime PlanSignEndTime { get; set; }

        #endregion

        /// <summary>
        /// 校区名称
        /// </summary>
        [ConditionMapping("b.campusName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CampusName { get; set; }

        [ConditionMapping("", Template = "CONTAINS(c.*, ${Data}$)")]
        public string Keyword { get; set; }

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [NoMapping]
        public string StaffName { get; set; }


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

        public static object AdjustConditionValueDelegate_Follow(string propertyName, object propertyValue, ref bool ignored)
        {
            object result = propertyValue;
            switch (propertyName)
            {
                case "IsStudyThere":
                    if (result.ToString() == "-1")
                        ignored = true;
                    break;
            }
            return result;
        }
    }
}