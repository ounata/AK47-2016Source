using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Common.Security;
using MCS.Library.OGUPermission;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServiceItems
{
    [Serializable]
    public class CustomerServiceItemListMosel:CustomerServiceItem
    {
    }

    [Serializable]
    public class CustomerServiceItemListMoselCollection : EditableDataObjectCollectionBase<CustomerServiceItemListMosel>
    {
        public CustomerServiceItemListMoselCollection()
        {

        }
    }
}