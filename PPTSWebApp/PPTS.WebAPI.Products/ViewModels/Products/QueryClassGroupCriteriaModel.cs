using MCS.Library.Data;
using MCS.Library.Core;
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


        public QueryClassGroupCriteriaModel() { campusidList = new List<string>(); }

        [NoMapping]
        private List<string> campusidList { set; get; }

        [NoMapping]
        public string CampusID
        {
            set
            {
                (value.IsNullOrWhiteSpace()).FalseAction(() =>
                {
                    campusidList.Contains(value).FalseAction(() =>
                    {
                        campusidList.Add(value);
                    });
                });
            }
        }

        [NoMapping]
        public string[] CampusIDs
        {
            get { return campusidList.ToArray(); }
            set
            {
                if (value != null)
                {
                    value.ForEach(s =>
                    {
                        campusidList.Contains(s).FalseAction(() => { campusidList.Add(s); });
                    });
                }
            }
        }


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

        [ConditionMapping("ProductStatus")]
        public string ProductStatus { get; set; }

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

    public class QueryClassGroupQueryResult
    {
        public PagedQueryResult<OrderClassGroupProductView, OrderClassGroupProductViewCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}