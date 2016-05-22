using MCS.Library.Data.DataObjects;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class CustomerClassSearchModel:Class
    {
        public decimal RealAmount { get; set; }

        public decimal ConfirmedAmount { get; set; }
    }

    public class CustomerClassSearchModelCollection : EditableDataObjectCollectionBase<CustomerClassSearchModel> {
    }
}