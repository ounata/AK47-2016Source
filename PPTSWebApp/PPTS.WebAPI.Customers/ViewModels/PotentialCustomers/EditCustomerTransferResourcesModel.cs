using MCS.Library.Validation;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class EditCustomerTransferResourcesModel
    {
        [ObjectValidator]
        public IList<CustomerTransferResource> CustomerTransferResources { set; get; }
    }
}