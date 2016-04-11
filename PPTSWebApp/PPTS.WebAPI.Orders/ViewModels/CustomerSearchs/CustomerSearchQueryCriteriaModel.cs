using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Orders.ViewModels.CustomerSearchs
{
    public class CustomerSearchQueryCriteriaModel
    {
        [ConditionMapping("CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerName { get; set; }

        [ConditionMapping("CustomerCode", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerCode { get; set; }

        [ConditionMapping("SchoolName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string SchoolName { get; set; }

        [ConditionMapping("EducatorName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string EducatorName { get; set; }

        //[ConditionMapping("CustomerCode")]
        //public string CustomerCode { get; set; }

        //[InConditionMapping("EntranceGrade")]
        //public int[] EntranceGrades { get; set; }

        //[ConditionMapping("CreateTime", Operation = ">=")]
        //public DateTime CreateTimeStart { get; set; }

        [ConditionMapping("Grade")]
        public string Grade { get; set; }
        /// <summary>
        /// 一对一剩余课次数
        /// </summary>
        [ConditionMapping("RemainOne2Ones", Operation = ">")]
        public int RemainOne2Ones
        {
            get;set;
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

        public CustomerSearchQueryCriteriaModel()
        {
           // this.RemainOne2Ones = 0;
        }


    }
}