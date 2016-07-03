using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVerifies
{
    /// <summary>
    /// 客户上门记录实体类
    /// </summary>
    public class CustomerVerifyQueryCriteriaModel
    {
        /// <summary>
        /// 校区ID
        /// </summary>
        [InConditionMapping("a.CampusID")]
        public string[] CampusIDs { get; set; }

        /// <summary>
        /// 建档人
        /// </summary>
        [ConditionMapping("a.CreatorName")]
        public string CreatorName { get; set; }

        /// <summary>
        /// 是否邀约
        /// </summary>
        [NoMapping]
        public string IsInvited
        {
            get;
            set;
        }

        [ConditionMapping("a.IsInvited")]
        public string _IsInvited
        {
            get
            {
                return IsInvited == "-1" ? "" : IsInvited;
            }
        }
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

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [ConditionMapping("a.CreatorName")]
        public string StaffName { get; set; }

        #region 实际上门时间

        /// <summary>
        /// 实际上门开始时间
        /// </summary>
        [ConditionMapping("a.CreateTime", Operation = ">=")]
        public DateTime CreateStartTime { get; set; }

        /// <summary>
        /// 实际上门结束时间
        /// </summary>
        [ConditionMapping("a.CreateTime", Operation = "<", AdjustDays = 1)]
        public DateTime CreateEndTime { get; set; }

        #endregion

        #region 建档时间

        /// <summary>
        /// 建档日期
        /// </summary>
        [NoMapping]
        public DateTime CreateTimeStart { get; set; }
        [NoMapping]
        public DateTime CreateTimeEnd { get; set; }

        #endregion

        /// <summary>
        /// 建档人岗位类型
        /// </summary>
        [NoMapping]
        public string[] CreatorJobTypes { get; set; }

        /// <summary>
        /// 学员名称/家长名称/电话/学员编号
        /// </summary>
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
