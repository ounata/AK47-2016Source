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
using MCS.Library.Validation;

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
            
            this.Model.ForEach(m => { m.CartID = UuidHelper.NewUuidString(); });

            base.PrepareData(context);
        }


        protected override void DoValidate(ValidationResults validationResults)
        {
            Model.Any(m => m.ProductCampusID.IsNullOrWhiteSpace()).TrueThrow("提交数据有误，没有进行校区！");

            var productCampusIds = new List<string>();
            var productIds = new List<string>();

            Model.ForEach(m => {
                productIds.Add(m.ProductID);
                productCampusIds.Add(m.ProductCampusID);
            });

            Service.ProductService.IsExistsCampusInProduct(productCampusIds.ToArray(), productIds.ToArray()).FalseThrow("提交数据有误，产品所属校区有误！");


            base.DoValidate(validationResults);
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            ShoppingCartAdapter.Instance.UpdateCollectionInContext(Model);

            return base.DoOperation(context);
        }

    }
}