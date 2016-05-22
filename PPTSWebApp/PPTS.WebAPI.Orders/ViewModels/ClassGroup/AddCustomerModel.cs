using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class AddCustomerModel
    {
        public string ClassID { get; set; }

        public Asset[] Assets { get; set; }

        private CustomerCollectionQueryResult _customerCollection = null;

        /// <summary>
        /// 学生信息列表
        /// </summary>
        public CustomerCollectionQueryResult CustomerCollection
        {
            get
            {
                if (_customerCollection == null && Assets != null && Assets.Length > 0)
                {
                    string[] customers = new string[Assets.Length];
                    for (int i = 0; i < Assets.Length; i++)
                    {
                        customers[i] = Assets[i].CustomerID;
                    }
                    _customerCollection = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerCollectionByCustomerIDs(customers);
                }
                return _customerCollection;
            }
        }
    }
}