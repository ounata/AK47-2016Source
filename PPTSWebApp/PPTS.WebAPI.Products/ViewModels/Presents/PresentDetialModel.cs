using MCS.Library.Data.DataObjects;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Presents
{
    public class PresentDetialModel
    {
        public PresentViewModel Present { get; set; }

        public PresentItemCollectionViewModel PresentItemCollection { get; set; }

        public PresentPermissionCollectionViewModel PresentPermissionCollection { get; set; }

        public PresentPermissionsApplieCollectionViewModel PresentPermissionsApplieCollection { get; set; }
    }
    public class PresentViewModel : Present { }

    public class PresentCollectionViewModel : EditableDataObjectCollectionBase<PresentViewModel> { }

    public class PresentItemViewModel : PresentItem { }

    public class PresentItemCollectionViewModel : EditableDataObjectCollectionBase<PresentItemViewModel> { }

    public class PresentPermissionViewModel : PresentPermission
    {
        [DataMember]
        public string CampusName { get; set; }

        [DataMember]
        public string CampusUseStatus { get; set; }

        [DataMember]
        public string EndDateView { get; set; }
    }

    public class PresentPermissionCollectionViewModel : EditableDataObjectCollectionBase<PresentPermissionViewModel>
    {
        public PresentPermissionCollectionViewModel AddCampusName()
        {
            string[] CampusIDs = new string[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                CampusIDs[i] = this[i].CampusID;
            }
            OguObjectCollection<IOrganization> orgList = PPTS.Data.Common.OGUExtensions.GetOrganizationByIDs(CampusIDs);
            foreach (var item in this)
            {
                item.CampusName = orgList.Find(o => o.ID == item.CampusID).GetShowShortName();
            }
            return this;
        }

        public PresentPermissionCollectionViewModel ConvertEndDate(string PresentID)
        {
            Data.Products.Entities.PresentPermissionViewCollection dpv = PresentPermissionViewAdapter.Instance.Load(builder => builder.AppendItem("PresentID", PresentID));
            foreach (var item in this)
            {
                var dpv_ = dpv.Find(dp => dp.CampusID == item.CampusID && dp.PresentID == item.PresentID);
                if (dpv_ != null)
                {
                    item.CampusUseStatus = "使用中";
                    item.EndDateView = "-";
                }
                else {
                    item.CampusUseStatus = "已停用";
                    item.EndDateView = item.EndDate.ToShortDateString();
                }
            }
            return this;
        }
    }

    public class PresentPermissionsApplyViewModel : PresentPermissionsApply
    {
        [DataMember]
        public string CampusName { get; set; }

        [DataMember]
        public string CampusUseStatus { get { return "-"; } }

        [DataMember]
        public string EndDateView { get { return "-"; } }
    }

    public class PresentPermissionsApplieCollectionViewModel : EditableDataObjectCollectionBase<PresentPermissionsApplyViewModel>
    {
        public PresentPermissionsApplieCollectionViewModel AddCampusName()
        {
            string[] CampusIDs = new string[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                CampusIDs[i] = this[i].CampusID;
            }
            OguObjectCollection<IOrganization> orgList = PPTS.Data.Common.OGUExtensions.GetOrganizationByIDs(CampusIDs);

            foreach (var item in this)
            {
                item.CampusName = orgList.Find(o => o.ID == item.CampusID).GetShowShortName();
            }
            return this;
        }
    }
}