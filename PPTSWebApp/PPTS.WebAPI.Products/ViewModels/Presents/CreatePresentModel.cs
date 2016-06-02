using MCS.Library.OGUPermission;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Products.ViewModels.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Presents
{
    public class CreatePresentModel
    {
        public Present Present { get; set; }

        public PresentPermissionsApplieCollection PresentPermissionsApplieCollection { get; set; }

        public PresentItemCollection PresentItemCollection { get; set; }

        public CheckResultModel CheckCreatePresent()
        {
            CheckResultModel result = new CheckResultModel();

            if (PresentPermissionsApplieCollection == null || PresentPermissionsApplieCollection.Count == 0)
            {
                result.SetErrorMsg("买赠表主要内容不得为空，请填写后再保存！");
                return result;
            }

            if (PresentItemCollection == null || PresentItemCollection.Count == 0)
            {
                result.SetErrorMsg("买赠表主要内容不得为空，请填写后再保存！");
                return result;
            }

            result = CheckDiscountPermissionsApplieCollection();
            if (!result.Sucess)
                return result;

            return result;
        }

        public CheckResultModel CheckDiscountPermissionsApplieCollection()
        {
            CheckResultModel result = new CheckResultModel();
            if (PresentPermissionsApplieCollection == null || PresentPermissionsApplieCollection.Count == 0)
            {
                result.SetErrorMsg("买赠表主要内容不得为空，请填写后再保存！");
                return result;
            }

            string orgIDs = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var v in PresentPermissionsApplieCollection)
                sb.AppendFormat(",'{0}'", v.CampusID);
            orgIDs = string.Format("({0})", sb.ToString().Substring(1));
            PresentPermissionsApplieCollection coll = PresentPermissionsApplyAdapter.Instance.LoadApprovingCollectionByOrgIDs(orgIDs);
            if (coll != null && coll.Count > 0)
            {
                string[] dpIDs = new string[coll.Count];
                for (int i = 0; i < coll.Count; i++)
                {
                    dpIDs[i] = coll[i].CampusID;
                }
                OguObjectCollection<IOrganization> orgList = PPTS.Data.Common.OGUExtensions.GetOrganizationByIDs(dpIDs);
                StringBuilder sb_ORG = new StringBuilder();
                foreach (var item in orgList)
                {
                    sb_ORG.AppendFormat(",{0}", item.DisplayName);
                }
                result.SetErrorMsg(string.Format("{0}已有一个待审批的买赠表，请勿重复提交", sb.ToString().Substring(1)));
                return result;
            }

            return result;
        }
    }
}