using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Discounts
{
    public class DiscountDetialModel
    {
        public DiscountViewModel Discount { get; set; }

        public DiscountItemCollectionViewModel DiscountItemCollection { get; set; }

        public DiscountPermissionCollectionViewModel DiscountPermissionCollection { get; set; }

        public DiscountPermissionsApplieCollectionViewModel DiscountPermissionsApplieCollection { get; set; }
    }

    public class DiscountViewModel : Discount { }

    public class DiscountCollectionViewModel : EditableDataObjectCollectionBase<DiscountViewModel>{ }

    public class DiscountItemViewModel : DiscountItem { }

    public class DiscountItemCollectionViewModel : EditableDataObjectCollectionBase<DiscountItemViewModel> {   }

    public class DiscountPermissionViewModel : DiscountPermission {
        [DataMember]
        public string CampusName { get; set; }

        [DataMember]
        public string CampusUseStatus { get; set; }

        [DataMember]
        public string EndDateView { get; set; }
    }

    public class DiscountPermissionCollectionViewModel : EditableDataObjectCollectionBase<DiscountPermissionViewModel> {
        public DiscountPermissionCollectionViewModel AddCampusName( ) {
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

        public DiscountPermissionCollectionViewModel ConvertEndDate(string discountID) {
            Data.Products.Entities.DiscountPermissionViewCollection dpv = DiscountPermissionViewAdapter.Instance.Load(builder => builder.AppendItem("DiscountID", discountID));
            foreach (var item in this)
            {
                var dpv_ = dpv.Find(dp => dp.CampusID == item.CampusID && dp.DiscountID == item.DiscountID);
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

    public class DiscountPermissionsApplyViewModel : DiscountPermissionsApply {
        [DataMember]
        public string CampusName { get; set; }

        [DataMember]
        public string CampusUseStatus { get { return "-"; } }

        [DataMember]
        public string EndDateView { get { return "-"; } }
    }

    public class DiscountPermissionsApplieCollectionViewModel : EditableDataObjectCollectionBase<DiscountPermissionsApplyViewModel> {
        public DiscountPermissionsApplieCollectionViewModel AddCampusName()
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