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

        [ConditionMapping("CREATE_TIME", Operation = ">=")]
        public DateTime StartTime
        {
            get;
            set;
        }

        [ConditionMapping("CREATE_TIME", Operation = "<", AdjustDays = 1)]
        public DateTime EndTime
        {
            get;
            set;
        }

        [ConditionMapping("AGE_1", Operation = ">", DefaultValueUsage = DefaultValueUsageType.UseDefaultValue)]
        public int AgeWithDefaultValue
        {
            get;
            set;
        }

        [ConditionMapping("AGE_2", Operation = ">")]
        public int AgeIgnoreDefaultValue
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
