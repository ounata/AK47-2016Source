using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Products.ViewModels.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Products.ViewModels.Discounts.Tests
{
    [TestClass()]
    public class CreateDiscountModelTests
    {
        [TestMethod()]
        public void CheckDiscountPermissionsApplieCollectionTest()
        {
            CreateDiscountModel model = new CreateDiscountModel() { DiscountPermissionsApplieCollection = new Data.Products.Entities.DiscountPermissionsApplieCollection() {  } };
            model.DiscountPermissionsApplieCollection.Add(new Data.Products.Entities.DiscountPermissionsApply() { CampusID = "18" });

            model.CheckDiscountPermissionsApplieCollection();
        }
    }
}