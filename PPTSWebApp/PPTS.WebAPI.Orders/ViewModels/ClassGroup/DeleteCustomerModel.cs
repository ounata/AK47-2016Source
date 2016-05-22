using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class DeleteCustomerModel
    {
        public string[] CustomerIDs { get; set; }

        public string ClassID { get; set; }

    }
}