using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    [Serializable]
    public class StudentQueryCriteriaModel
    {
        [InConditionMapping("Grade")]
        public int[] Grade { get; set; }

        [ConditionMapping("CreateTime", Operation = ">=")]
        public DateTime CreateTimeStart { get; set; }

        [ConditionMapping("CreateTime", Operation = "<", AdjustDays = 1)]
        public DateTime CreateTimeEnd { get; set; }

        [ConditionMapping("FirstSignTime", Operation = ">=")]
        public DateTime FirstSignTimeStart { get; set; }

        [ConditionMapping("FirstSignTime", Operation = "<", AdjustDays = 1)]
        public DateTime FirstSignTimeEnd { get; set; }

        [InConditionMapping("CustomerLevel")]
        public int[] CustomerLevels { get; set; }

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

    public class StudentQueryResult
    {
        public PagedQueryResult<Customer, CustomerCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}
