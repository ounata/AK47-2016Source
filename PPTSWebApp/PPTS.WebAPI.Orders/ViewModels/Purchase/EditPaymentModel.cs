using MCS.Library.Validation;
using MCS.Library.Core;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using MCS.Library.Configuration;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Common;
using PPTS.Contracts.Customers.Operations;
using PPTS.Contracts.Orders.Operations;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products.Entities;
using PPTS.Data.Customers.Entities;


namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{
    public class EditPaymentModel
    {
        public string OrderID { set; get; }
        public string ChargeApplyID { set; get; }

        public Order Order { set; get; }

        public EditPaymentModel FillOrder()
        {
            Order.IsNull(() =>
            {
                Validate();
                Order = new Order() { OrderID = OrderID, ChargeApplyID = ChargeApplyID };
                FillUser(Order);
            });

            return this;

        }



        public MCS.Library.OGUPermission.IUser CurrentUser { set; get; }

        private void FillUser(object info)
        {
            if (CurrentUser != null)
            {
                if (info is IEntityWithCreator)
                {
                    CurrentUser.FillCreatorInfo(info as IEntityWithCreator);
                }
                if (info is IEntityWithModifier)
                {
                    CurrentUser.FillModifierInfo(info as IEntityWithModifier);
                }

            }

        }


        private void Validate()
        {
            OrderID.CheckStringIsNullOrEmpty("OrderID");
            ChargeApplyID.CheckStringIsNullOrEmpty("ChargeApplyID");
        }

    }
}