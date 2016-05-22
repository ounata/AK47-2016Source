using System;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Products.ViewModels.Products
{
    public class ProductQueryCriteriaModel
    {
        [NoMapping]
        public string[] CampusIDs { get; set; }

        [ConditionMapping("CategoryType")]
        public string CategoryType { get; set; }

        [ConditionMapping("CreatorName")]
        public string CreatorName { get; set; }

        [ConditionMapping("HasPartner")]
        public string HasPartner { get; set; }

        [ConditionMapping("ProductStatus")]
        public string ProductStatus { get; set; }

        [ConditionMapping("PeriodDuration")]
        public string PeriodDuration { get; set; }

        [ConditionMapping("ProductName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string Name { get; set; }

        [ConditionMapping("ProductCode", Operation = "=")]
        public string ProductCode { get; set; }

        //创建时间
        [ConditionMapping("CreateTime", Operation = ">=")]
        public DateTime StartCreateDate { get; set; }

        [ConditionMapping("CreateTime", Operation = "<", AdjustDays = 1)]
        public DateTime EndCreateDate { get; set; }

        //启售时间
        [ConditionMapping("StartDate", Operation = ">=")]
        public DateTime StartStartDate { get; set; }

        [ConditionMapping("StartDate", Operation = "<", AdjustDays = 1)]
        public DateTime EndStartDate { get; set; }

        //止售时间
        [ConditionMapping("EndDate", Operation = ">=")]
        public DateTime StarEndtDate { get; set; }

        [ConditionMapping("EndDate", Operation = "<", AdjustDays = 1)]
        public DateTime EndEndDate { get; set; }

        

        //----------------------------

        [NoMapping]
        public string Grade
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (SelectedGrades != null)
                    {
                        SelectedGrades[SelectedGrades.Length] = value;
                    }
                    else {
                        SelectedGrades= new string[] { value };
                    }
                }
            }
        }

        [NoMapping]
        public string Subject
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (SelectedSubjects != null)
                    {
                        SelectedSubjects[SelectedSubjects.Length] = value;
                    }
                    else
                    {
                        SelectedSubjects = new string[] { value };
                    }
                }
            }
        }

        [NoMapping]
        public string CourseLevel
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (SelectedCourseLevels != null)
                    {
                        SelectedCourseLevels[SelectedCourseLevels.Length] = value;
                    }
                    else
                    {
                        SelectedCourseLevels = new string[] { value };
                    }
                }
            }
        }



        //----------------------------
        [InConditionMapping("Grade")]
        public string[] SelectedGrades { set; get; }

        [InConditionMapping("Subject")]
        public string[] SelectedSubjects { set; get; }

        [InConditionMapping("CourseLevel")]
        public string[] SelectedCourseLevels { set; get; }
        


        [ConditionMapping("MinPeoples", Operation = ">=")]
        public string[] MinPeoples { set; get; }

        [ConditionMapping("MaxPeoples", Operation = "<=")]
        public string[] MaxPeoples { set; get; }



        [ConditionMapping("PromotionQuota", Operation = ">=")]
        public decimal MinDiscountAmount { get; set; }

        [ConditionMapping("PromotionQuota", Operation = "<=")]
        public decimal MaxDiscountAmount { get; set; }

        //MinAmount MaxAmount

        [ConditionMapping("ProductPrice", Operation = ">=")]
        public decimal MinProductPrice { get; set; }

        [ConditionMapping("ProductPrice", Operation = "<=")]
        public decimal MaxProductPrice { get; set; }



        [ConditionMapping("TargetPrice", Operation = ">=")]
        public decimal MinTargetPrice { get; set; }

        [ConditionMapping("TargetPrice", Operation = "<=")]
        public decimal MaxTargetPrice { get; set; }




        [ConditionMapping("CooperationPrice", Operation = ">=")]
        public decimal MinCooperationPrice { get; set; }

        [ConditionMapping("CooperationPrice", Operation = "<=")]
        public decimal MaxCooperationPrice { get; set; }




        //----------------------------


        private PageRequestParams prp = new PageRequestParams() { PageIndex = 1, PageSize = 10, };

        [NoMapping]
        public PageRequestParams PageParams
        {
            get { return prp; }
            set
            {
                prp = value;
            }
        }

        private OrderByRequestItem[] obris = new OrderByRequestItem[] { new OrderByRequestItem() { DataField = "CreateTime" } };
        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get { return obris; }
            set { obris = value; }
        }
    }
}