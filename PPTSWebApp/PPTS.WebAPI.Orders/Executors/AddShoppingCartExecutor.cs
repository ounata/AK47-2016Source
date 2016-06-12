using MCS.Library.Core;
using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Data;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("加入购物车")]
    public class AddShoppingCartExecutor : PPTSEditPurchaseExecutorBase<ShoppingCartCollection>
    {
        
        public AddShoppingCartExecutor(ShoppingCartCollection model) : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            
            this.Model.ForEach(m => {
                m.CartID = UuidHelper.NewUuidString();
                ShoppingCartAdapter.Instance.UpdateInContext(m);
            });
            base.PrepareData(context);
        }

        


    }
}