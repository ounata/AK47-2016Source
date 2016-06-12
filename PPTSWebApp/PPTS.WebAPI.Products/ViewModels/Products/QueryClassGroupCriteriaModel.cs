using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;

using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Products
{
    public class QueryClassGroupCriteriaModel
    {

        [NoMapping]
        public string[] CampusIDs { get; set; }

        [ConditionMapping("ProductName")]
        public string ProductName { set; get; }

        [ConditionMapping("ClassName")]
        public string ClassName { set; get; }

        [ConditionMapping("Grade")]
        public string Grade { set; get; }

        [ConditionMapping("Subject")]
        public string Subject { set; get; }

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

    public class QueryClassGroupQueryResult {
        public PagedQueryResult<OrderClassGroupProductView, OrderClassGroupProductViewCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}