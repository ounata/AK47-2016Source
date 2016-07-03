using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Products.Executors;
using PPTS.WebAPI.Products.ViewModels.Products;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Principal;
using System.Threading;

namespace PPTS.WebAPI.ProductsTests.Executors
{
    /// <summary>
    /// AddProductExecutorTests 的摘要说明
    /// </summary>
    [TestClass]
    public class AddProductExecutorTests
    {
        public AddProductExecutorTests()
        {

            var config = OguObjectSettings.GetConfig();
            var user = config.Objects["campus"].User;
            Thread.CurrentPrincipal = new DeluxePrincipal(new DeluxeIdentity(user));
        }


        [TestMethod]
        public void AddProductExecutorMethod()
        {
            var model = new ProductModel() { Product = new Data.Products.Entities.Product() { } };
            new AddProductExecutor(model) { NeedValidation = true }.Execute();
        }

        [TestMethod]
        public void AddProductExecutorMethod1()
        {
            var model = new ProductModel()
            {
                ProductCodePrefix = "A",
                Product = new Data.Products.Entities.Product()
                {
                    Catalog = "1",
                    Grade = "4",
                    ProductName = "幼儿园小班-数学-30分钟-1A-自主招生",
                    ProductMemo = "123123123123",
                    ProductPrice = 1,
                    ProductStatus = Data.Products.ProductStatus.PendingApproval,
                    ProductUnit = Data.Products.ProductUnit.Period,
                    Season = "1",
                    Subject = "2",
                    TargetPrice = 1,
                    TunlandAllowed = 1
                },
                CategoryType = Data.Common.CategoryType.OneToOne,
                ExOfCourse = new Data.Products.Entities.ProductExOfCourse()
                {
                    ClassType = "1",
                    CoachType = "2",
                    CourseLevel = "2",
                    IncomeBelonging = "0",
                    IsCrossCampus = 0,
                    LessonCount = 0,
                    LessonDuration = 0,
                    LessonDurationValue = 0,
                    MaxPeoples = 0,
                    MinPeoples = 0,
                    PeriodDuration = "1",
                    PeriodDurationValue = 0,
                    PeriodsOfLesson = 0,

                },
                SalaryRules = new Data.Products.Entities.ProductSalaryRuleCollection()
            };
            new AddProductExecutor(model) { NeedValidation = true }.Execute();
        }

    }
}
