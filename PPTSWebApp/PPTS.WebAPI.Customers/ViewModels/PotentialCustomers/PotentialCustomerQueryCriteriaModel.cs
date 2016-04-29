using System;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class PotentialCustomerQueryCriteriaModel
    {
        [ConditionMapping("", Template = "CONTAINS(pcf.*, ${Data}$)")]
        //[ConditionMapping("ParentSearchContent", Template = "CONTAINS(${DataField}$, ${Data}$)")]
        public string Keyword { get; set; }

        [InConditionMapping("EntranceGrade")]
        public int[] EntranceGrades { get; set; }

        [ConditionMapping("CreateTime", Operation = ">=")]
        public DateTime CreateTimeStart { get; set; }

        [ConditionMapping("CreateTime", Operation = "<", AdjustDays = 1)]
        public DateTime CreateTimeEnd { get; set; }

        [InConditionMapping("FollowStage")]
        public int[] FollowStages { get; set; }

        [InConditionMapping("CustomerLevel")]
        public int[] CustomerLevels { get; set; }

        [InConditionMapping("SourceMainType")]
        public int[] SourceMainTypes { get; set; }

        [ConditionMapping("CreatorName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CreatorName { get; set; }

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