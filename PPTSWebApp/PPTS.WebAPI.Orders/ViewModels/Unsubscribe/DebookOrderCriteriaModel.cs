using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Unsubscribe
{
    public class DebookOrderCriteriaModel
    {
        [InConditionMapping("CampusID")]
        public string [] CampusIDs { set; get; }

        [ConditionMapping("CustomerName")]
        public string StuName { set; get; }

        [ConditionMapping("CustomerCode")]
        public string StuCode { set; get; }

        [ConditionMapping("DebookNo")]
        public string OrderCode { set; get; }

        [ConditionMapping("Subject")]
        public string Subject { set; get; }

        [ConditionMapping("CourseLevel")]
        public string CourseLevel { set; get; }

        [ConditionMapping("LessonDuration")]
        public string Duration { set; get; }


        //----------------------------

        [NoMapping]
        public PageRequestParams PageParams
        {
            get; set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get; set;
        }
    }

    public class DebookOrderQueryResult {
        public PagedQueryResult<DebookOrder, DebookOrderCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}