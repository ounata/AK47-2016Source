using System.Collections.Generic;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using MCS.Library.Core;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class CreatableCustomerTransferResourceModel
    {
        public IList<CustomerTransferResource> TransferResources { get; set; }
        public IList<BaseConstantEntity> Branches { get; set; }
        public IList<BaseConstantEntity> Campuses { get; set; }

        public CreatableCustomerTransferResourceModel(string[] customerIds)
        {
            this.TransferResources = new List<CustomerTransferResource>();
            foreach (var customerId in customerIds)
            {
                this.TransferResources.Add(new CustomerTransferResource
                {
                    TransferID = UuidHelper.NewUuidString(),
                    CustomerID = customerId
                });
            }
        }
    }
}