using MCS.Library.Data;
using MCS.Library.Core;
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
        //[InConditionMapping("CampusID")]
        //public string [] CampusIDs { set; get; }

        public DebookOrderCriteriaModel() { campusidList = new List<string>(); }

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

        [InConditionMapping("CampusID")]
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

        [ConditionMapping("CustomerName")]
        public string StuName { set; get; }

        [ConditionMapping("CustomerCode")]
        public string StuCode { set; get; }

        //[ConditionMapping("DebookNo")]
        [ConditionMapping("AssetCode")]
        public string OrderCode { set; get; }

        [ConditionMapping("Subject")]
        public string Subject { set; get; }

        [ConditionMapping("CourseLevel")]
        public string CourseLevel { set; get; }

        [ConditionMapping("LessonDuration")]
        public string Duration { set; get; }

        [ConditionMapping("ParentName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string ParentName { set; get; }

        [InConditionMapping("CategoryType")]
        public string[] SelectedCategories { set; get; }
        

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
        public PagedQueryResult<DebookOrderItemView, DebookOrderItemViewCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
        public Data.Products.Entities.CategoryEntityCollection Categories { set; get; }
    }
}