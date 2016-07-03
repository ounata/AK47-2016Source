using PPTS.Contracts.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.Services.Orders.Service
{
    public class CustomerService
    {

        /// <summary>
        /// 获取学员 咨询、学管 师
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static CustomerQueryResult GetCustomerStaffRelationByCustomerId(string customerId)
        {
            return PPTS.Contracts.Proxies.PPTSCustomerQueryServiceProxy.Instance.QueryCustomerCollectionByCustomerIDs(customerId).CustomerCollection[0];
        }

    }
}