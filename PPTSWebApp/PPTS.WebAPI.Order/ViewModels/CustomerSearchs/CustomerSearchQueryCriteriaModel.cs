using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Order.ViewModels.CustomerSearchs
{
    public class CustomerSearchQueryCriteriaModel
    {
        //[ConditionMapping("CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        //public string Name { get; set; }

        //[ConditionMapping("CustomerCode")]
        //public string CustomerCode { get; set; }

        //[InConditionMapping("EntranceGrade")]
        //public int[] EntranceGrades { get; set; }

        //[ConditionMapping("CreateTime", Operation = ">=")]
        //public DateTime CreateTimeStart { get; set; }


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