using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customer.ViewModels.PotentialCustomers
{
    [Serializable]
    public class ParentModel : Parent, IContactPhoneNumbers
    {
        [NoMapping]
        public string PrimaryPhone
        {
            get;
            set;
        }

        [NoMapping]
        public string SecondaryPhone
        {
            get;
            set;
        }
    }
}