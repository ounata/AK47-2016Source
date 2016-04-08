using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customer.ViewModels.PotentialCustomers
{
    [Serializable]
    public class PotentialCustomerModel : PotentialCustomer, IContactPhoneNumbers
    {
        /// <summary>
        /// 主要联系方式
        /// </summary>
        [NoMapping]
        public string PrimaryPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 辅助联系方式
        /// </summary>
        [NoMapping]
        public string SecondaryPhone
        {
            get;
            set;
        }
    }
}