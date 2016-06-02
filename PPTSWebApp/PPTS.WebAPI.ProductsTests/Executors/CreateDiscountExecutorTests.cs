using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Products.Executors;
using PPTS.WebAPI.Products.ViewModels.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Products.Executors.Tests
{
    [TestClass()]
    public class CreateDiscountExecutorTests
    {
        [TestMethod()]
        public void CreateDiscountExecutorTest()
        {
            CreateDiscountModel model = new CreateDiscountModel();
            model.Discount = new Data.Products.Entities.Discount();
            model.DiscountItemCollection = new Data.Products.Entities.DiscountItemCollection();
            model.DiscountPermissionsApplieCollection = new Data.Products.Entities.DiscountPermissionsApplieCollection();


            model.Discount.StartDate = new DateTime(2016,5,30);

            //model.DiscountItemCollection.Add(new Data.Products.Entities.DiscountItem() { DiscountStandard=1, DiscountValue=0.9 });

            CreateDiscountExecutor executor = new CreateDiscountExecutor(model);
            executor.Execute();
        }
    }
}