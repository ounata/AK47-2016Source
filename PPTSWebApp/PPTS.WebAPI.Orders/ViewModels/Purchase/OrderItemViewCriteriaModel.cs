using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using MCS.Library.Core;

using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Principal;
using PPTS.Data.Common;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{
    public class OrderItemViewCriteriaModel
    {
        

        public OrderItemViewCriteriaModel() { campusidList = new List<string>(); }
        

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
            get {
                return campusidList.ToArray();
            }
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

        [ConditionMapping("ProcessStatus")]
        public string ProcessStatus = ((int)ProcessStatusDefine.Processed).ToString();

        [ConditionMapping("CustomerName")]
        public string StuName { set; get; }

        [ConditionMapping("ParentName")]
        public string ParentName { set; get; }

        [ConditionMapping("CustomerID")]
        public string stuId { set; get; }

        [ConditionMapping("CustomerCode")]
        public string StuCode { set; get; }

        [ConditionMapping("ItemNo")]
        public string OrderCode { set; get; }

        [ConditionMapping("Subject")]
        public string Subject { set; get; }

        [ConditionMapping("CourseLevel")]
        public string CourseLevel { set; get; }

        [ConditionMapping("LessonDuration")]
        public string Duration { set; get; }

        [InConditionMapping("CategoryType")]
        public string[] selectedCategoryTypes { set; get; }

        [InConditionMapping("OrderStatus")]
        public string[] SelectedOrderStatus { set; get; }

        [InConditionMapping("CustomerGrade")]
        public string[] SelectedStuGrades { set; get; }

        [InConditionMapping("Grade")]
        public string[] SelectedProductGrades { set; get; }

        [InConditionMapping("CategoryType")]
        public string[] SelectedCategories { set; get; }


        [InConditionMapping("ProductUnit")]
        public string[] SelectedUnits { set; get; }

        /// <summary>
        /// 操作人岗位
        /// </summary>
        [InConditionMapping("SubmitterJobType")]
        public string[] SelectedPosts { set; get; }

        //剩余课时
        [ConditionMapping("Amount", Operation = ">=")]
        public string RemainLessonStart { set; get; }

        //剩余课时
        [ConditionMapping("Amount", Operation = "<=")]
        public string RemainLessonEnd { set; get; }

        //订购日期
        [ConditionMapping("SubmitTime", Operation = ">=")]
        public DateTime StartDate { set; get; }

        //订购日期
        [ConditionMapping("SubmitTime", Operation = "<=")]
        public DateTime endDate { set; get; }

        //

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

    public class OrderItemViewQueryResult
    {
        public PagedQueryResult<OrderItemView, OrderItemViewCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public Data.Products.Entities.CategoryEntityCollection Categories { set; get; }
    }

}