using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.ExtraGift
{
    public class PresentsQueryCriteriaModel
    {
        #region [PM].[Presents]条件
        [ConditionMapping("StartDate", Operation = ">=")]
        public DateTime StartDate { get; set; }

        [ConditionMapping("StartDate", Operation = "<=")]
        public DateTime EndDate { get; set; }

        [ConditionMapping("PresentStatus")]
        public PresentStatusDefine PresentStatus { get; set; }

        public static object PresentsAdjustConditionValueDelegate(string propertyName, object propertyValue, ref bool ignored)
        {
            object returnValue = propertyValue;

            switch (propertyName)
            {
                case "StartDate":
                case "EndDate":
                case "PresentStatus":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return returnValue;
        }
        #endregion

        #region [PM].[v_PresentPermissions_Current] or [PM].[PresentPermissionApplies]
        [InConditionMapping("pp.UseOrgID")]
        public string[] UseOrgIDs { get; set; }

        public string CampusStatus { get; set; }

        public static object PresentPermissionsAdjustConditionValueDelegate(string propertyName, object propertyValue, ref bool ignored)
        {
            object returnValue = propertyValue;

            switch (propertyName)
            {
                case "UseOrgIDs":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return returnValue;
        }

        public bool CheckPresentPermissionsAdjustCondition()
        {
            return UseOrgIDs!=null && UseOrgIDs.Length>0;
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