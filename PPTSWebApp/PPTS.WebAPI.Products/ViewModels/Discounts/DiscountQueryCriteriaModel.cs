using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Discounts
{
    public class DiscountQueryCriteriaModel
    {
        #region Discounts
        [ConditionMapping("StartDate", Operation = ">=")]
        public DateTime StartDate { get; set; }

        [ConditionMapping("StartDate", Operation = "<=")]
        public DateTime EndDate { get; set; }

        [ConditionMapping("DiscountStatus")]
        public int DiscountStatus { get; set; }
        public static object DiscountsAdjustConditionValueDelegate(string propertyName, object propertyValue, ref bool ignored)
        {
            object returnValue = propertyValue;

            switch (propertyName)
            {
                case "StartDate":
                case "EndDate":
                case "DiscountStatus":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return returnValue;
        }
        #endregion

        #region v_DiscountPermissions_Current  or DiscountPermissions
        [InConditionMapping("dp.CampusID")]
        public string[] CampusIDs { get; set; }

        public CampusUseInfoDefine CampusStatus { get; set; }
        public static object DiscountPermissionsAdjustConditionValueDelegate(string propertyName, object propertyValue, ref bool ignored)
        {
            object returnValue = propertyValue;

            switch (propertyName)
            {
                case "CampusIDs":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return returnValue;
        }

        public bool CheckPresentPermissionsAdjustCondition()
        {
            return CampusIDs != null && CampusIDs.Length > 0;
        }
        #endregion

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