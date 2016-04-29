using System;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    /// <summary>
    /// 客户跟进记录实体类
    /// </summary>
    public class FollowQueryCriteriaModel
    {
        /// <summary>
        /// 记录人
        /// </summary>
        [InConditionMapping("FollowerName")]
        public int FollowerName { get; set; }

        /// <summary>
        /// 建档人
        /// </summary>
        [InConditionMapping("CreatorName")]
        public int CreatorName { get; set; }

        /// <summary>
        /// 跟进阶段
        /// </summary>
        [InConditionMapping("FollowStage")]
        public int[] FollowStages { get; set; }

        /// <summary>
        /// 购买意愿
        /// </summary>
        [InConditionMapping("PurchaseIntension")]
        public int[] PurchaseIntensions { get; set; }

        /// <summary>
        /// 客户级别
        /// </summary>
        [InConditionMapping("CustomerLevel")]
        public int[] CustomerLevels { get; set; }

        /// <summary>
        /// 跟进方式
        /// </summary>
        [InConditionMapping("FollowType")]
        public int[] FollowTypes { get; set; }

        #region 跟进时间

        /// <summary>
        /// 跟进开始时间
        /// </summary>
        [ConditionMapping("FollowTime", Operation = ">=")]
        public DateTime FollowStartTime { get; set; }

        /// <summary>
        /// 跟进结束时间
        /// </summary>
        [ConditionMapping("FollowTime", Operation = "<", AdjustDays = 1)]
        public DateTime FollowEndTime { get; set; }

        #endregion

        #region 下次沟通时间

        /// <summary>
        /// 下次沟通开始时间
        /// </summary>
        [ConditionMapping("NextTalkTime", Operation = ">=")]
        public DateTime NextTalkStartTime { get; set; }

        /// <summary>
        /// 下次沟通结束时间
        /// </summary>
        [ConditionMapping("NextTalkTime", Operation = "<", AdjustDays = 1)]
        public DateTime NextTalkEndTime { get; set; }

        #endregion

        /// <summary>
        /// 是否在本机构进行辅导
        /// </summary>
        [InConditionMapping("IsStudyThere")]
        public int IsStudyThere { get; set; }

        #region 预计签约时间

        /// <summary>
        /// 预计签约开始时间
        /// </summary>
        [ConditionMapping("PlanSignDate", Operation = ">=")]
        public DateTime PlanSignStartTime { get; set; }

        /// <summary>
        /// 预计签约结束时间
        /// </summary>
        [ConditionMapping("PlanSignDate", Operation = "<", AdjustDays = 1)]
        public DateTime PlanSignEndTime { get; set; }

        #endregion

        /// <summary>
        /// 校区名称
        /// </summary>
        [ConditionMapping("campusName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CampusName { get; set; }

        /// <summary>
        /// 学员或家长姓名
        /// </summary>
        [ConditionMapping("CustomerSearchContent", Template = "CONTAINS(c.*, ${Data}$)")]
        public string CustomerOrParentName { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        [ConditionMapping("CustomerCode", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// 家长联系方式
        /// </summary>
        [ConditionMapping("ParentSearchContent", Template = "CONTAINS(${DataField}$, ${Data}$)")]
        public string ParentPhones { get; set; }

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [ConditionMapping("CustomerSearchContent", Template = "CONTAINS(${DataField}$, ${Data}$)")]
        public string StaffName { get; set; }

        #region 对应Table



        ///// <summary>
        ///// 学员姓名
        ///// </summary>
        //[NoMapping]
        //public string ParentName { get; set; }

        ///// <summary>
        ///// 跟进时间
        ///// </summary>
        //[NoMapping]
        //public string FollowTime { get; set; }

        ///// <summary>
        ///// 跟进方式
        ///// </summary>
        //[NoMapping]
        //public string FollowType { get; set; }

        ///// <summary>
        ///// 跟进对象
        ///// </summary>
        //[NoMapping]
        //public string FollowObject { get; set; }

        #endregion


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

        #region 旧版本注释的代码
        //[ConditionMapping("CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        //public string Name { get; set; }

        //[ConditionMapping("CustomerCode")]
        //public string CustomerCode { get; set; }

        //[InConditionMapping("EntranceGrade")]
        //public int[] EntranceGrades { get; set; }

        //[ConditionMapping("CreateTime", Operation = ">=")]
        //public DateTime CreateTimeStart { get; set; }

        //[ConditionMapping("CreateTime", Operation = "<", AdjustDays = 1)]
        //public DateTime CreateTimeEnd { get; set; }
        #endregion

        #region 旧版本注释的代码
        //[InConditionMapping("SourceMainType")]
        //public int[] SourceMainTypes { get; set; }

        //[ConditionMapping("CreatorName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        //public string CreatorName { get; set; }
        #endregion
    }
}