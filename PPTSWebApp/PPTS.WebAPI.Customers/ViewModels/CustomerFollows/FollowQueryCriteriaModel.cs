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
        [ConditionMapping("a.FollowerName")]
        public string FollowerName { get; set; }

        /// <summary>
        /// 建档人
        /// </summary>
        [ConditionMapping("a.CreatorName")]
        public string CreatorName { get; set; }

        /// <summary>
        /// 建档人岗位类型
        /// </summary>
        [NoMapping]
        public string[] CreatorJobTypes { get; set; }

        /// <summary>
        /// 查询部门
        /// </summary>
        [NoMapping]
        public string[] QueryDepts { get; set; }

        /// <summary>
        /// 查询部门ID
        /// </summary>
        [NoMapping]
        public string QueryDeptID { get; set; }

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

        #region 建档日期

        /// <summary>
        /// 建档日期起
        /// </summary>
        [ConditionMapping("a.CreateTime", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime CreateTimeStart { get; set; }

        /// <summary>
        /// 建档日期止
        /// </summary>
        [ConditionMapping("a.CreateTime", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime CreateTimeEnd { get; set; }

        #endregion

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

        /// <summary>
        /// 是否在其他机构辅导
        /// </summary>
        [NoMapping]
        public string IsStudyThere { get; set; }

        /// <summary>
        /// 是否在其他机构辅导
        /// </summary>
        [ConditionMapping("a.IsStudyThere")]
        public string _IsStudyThere { get { return IsStudyThere == "-1" ? "" : IsStudyThere; } set { } }

        /// <summary>
        /// 记录人岗位
        /// </summary>
        [InConditionMapping("a.FollowerJobID")]
        public string[] FollowerJobIDs { get; set; }

        /// <summary>
        /// 记录人姓名
        /// </summary>
        [ConditionMapping("a.FollowerJobName")]
        public string FollowerJobName { get; set; }

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
        /// 校区ID
        /// </summary>
        [NoMapping]
        public string CampusIDs { get; set; }

        /// <summary>
        /// 分公司ID
        /// </summary>
        [NoMapping]
        public string BranchIDs { get; set; }

        //[ConditionMapping("", Template = "CONTAINS(c.*, ${Data}$)")]
        [NoMapping]
        public string Keyword { get; set; }

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