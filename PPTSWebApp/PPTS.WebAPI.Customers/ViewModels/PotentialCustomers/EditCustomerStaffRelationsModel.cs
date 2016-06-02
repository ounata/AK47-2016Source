using MCS.Library.Core;
using MCS.Library.Validation;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class EditCustomerStaffRelationsModel
    {
        [ObjectValidator]
        public IList<CustomerStaffRelation> CustomerStaffRelations { set; get; }

        public void InitCustomerStaffRelation()
        {
            this.CustomerStaffRelations.ToList().ForEach(action => {
                var csr = CustomerStaffRelationAdapter.Instance.GetCustomerStaffRelation(action.CustomerID, ((int)action.RelationType).ToString());
                csr.IsNotNull(a => { action.VersionStartTime = a.VersionStartTime;action.VersionEndTime = a.VersionEndTime; });
            });
        }
    }
}