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
        [ConditionMapping("", Template = "CONTAINS(c.*, ${Data}$)")]
        public string Keyword { get; set; }
        /// <summary>
        /// 校区名称
        /// </summary>
        [ConditionMapping("b.campusName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CampusName { get; set; }

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [ConditionMapping("StaffName")]
        public string StaffName { get; set; }

        /// <summary>
        /// 建档人
        /// </summary>
        [InConditionMapping("b.CreatorName")]
        public int CreatorName { get; set; }

        /// <summary>
        /// 归属组织机构ID
        /// </summary>
        [InConditionMapping("a.CampusID")]
        public string[] OrgIds { get; set; }

        #region 预计上门时间

        /// <summary>
        /// 预计上门开始时间
        /// </summary>
        [ConditionMapping("planTime", Operation = ">=")]
        public DateTime PlanVerifyStartTime { get; set; }

        /// <summary>
        /// 预计上门结束时间
        /// </summary>
        [ConditionMapping("planTime", Operation = "<", AdjustDays = 1)]
        public DateTime PlanVerifyEndTime { get; set; }

        #endregion

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
        /// 建档开始时间
        /// </summary>
        [ConditionMapping("b.CreateTime", Operation = ">=")]
        public DateTime CustomerCreateStartTime { get; set; }

        /// <summary>
        /// 建档结束时间
        /// </summary>
        [ConditionMapping("b.CreateTime", Operation = "<", AdjustDays = 1)]
        public DateTime CustomerCreateEndTime { get; set; }

        #endregion

        /// <summary>
        /// 是否邀约
        /// </summary>
        [ConditionMapping("a.IsInvited")]
        public int IsInvited
        {
            get;
            set;
        }

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
