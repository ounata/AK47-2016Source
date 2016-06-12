using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    public class ConditionObject
    {
        public ConditionObject()
        {
            this.ThreeStateWithDefault = BooleanState.Unknown;
        }

        [ConditionMapping("SUBJECT", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string Subject
        {
            get;
            set;
        }

        [ConditionMapping("SEARCH_CONTENT", Template = "CONTAINS(${DataField}$, ${Data}$)")]
        public string FullTextTerm
        {
            get;
            set;
        }

        [ConditionMapping("GENDER")]
        public GenderType Gender
        {
            get;
            set;
        }

        [ConditionMapping("GENDER_WITH_DEFAULT")]
        public GenderType GenderWithDefault
        {
            get;
            set;
        }

        [ConditionMapping("CREATE_TIME", Operation = ">=")]
        public DateTime StartTime
        {
            get;
            set;
        }

        [ConditionMapping("CREATE_TIME", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime EndTime
        {
            get;
            set;
        }

        [ConditionMapping("AgeWithDefaultValue", Operation = ">", DefaultValueUsage = DefaultValueUsageType.UseDefaultValue)]
        public int AgeWithDefaultValue
        {
            get;
            set;
        }

        [ConditionMapping("AgeIgnoreDefaultValue", Operation = ">")]
        public int AgeIgnoreDefaultValue
        {
            get;
            set;
        }

        [ConditionMapping("AgeWithDefaultExpression", DefaultExpression = "AgeWithDefaultExpression = '40'", Operation = "<>")]
        public int AgeWithDefaultExpression
        {
            get;
            set;
        }

        [ConditionMapping("ThreeState")]
        public BooleanState ThreeState
        {
            get;
            set;
        }

        [ConditionMapping("ThreeStateWithDefault")]
        public BooleanState ThreeStateWithDefault
        {
            get;
            set;
        }

        [ConditionMapping("IgnoreProperty")]
        public string IgnoreProperty
        {
            get;
            set;
        }
    }
}
