﻿using System;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    public class CustomerFollowQueryCriteriaModel
    {
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

        [InConditionMapping("FollowStage")]
        public int[] FollowStages { get; set; }

        [InConditionMapping("PurchaseIntension")]
        public int[] PurchaseIntensions { get; set; }

        //[InConditionMapping("SourceMainType")]
        //public int[] SourceMainTypes { get; set; }

        //[ConditionMapping("CreatorName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        //public string CreatorName { get; set; }

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