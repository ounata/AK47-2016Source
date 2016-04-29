using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{
    public class OrderItemViewCriteriaModel
    {

        [ConditionMapping("CustomerName")]
        public string StuName { set; get; }

        [ConditionMapping("ParentName")]
        public string ParentName { set; get; }


        [ConditionMapping("CustomerCode")]
        public string StuCode { set; get; }

        [ConditionMapping("OrderNo")]
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
        public string[] selectedOrderStatus { set; get; }

        [InConditionMapping("CustomerGrade")]
        public string[] selectedStuGrades { set; get; }

        [InConditionMapping("ProductUnit")]
        public string[] selectedUnits { set; get; }

        /// <summary>
        /// 操作人岗位
        /// </summary>
        [InConditionMapping("SubmitterJobType")]
        public string[] selectedPosts { set; get; }

        //剩余课时
        [ConditionMapping("Amount", Operation = ">=")]
        public string RemainLessonStart { set; get; }

        //剩余课时
        [ConditionMapping("Amount", Operation = "<=")]
        public string RemainLessonEnd { set; get; }

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
    }

}