using MCS.Library.Validation;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Product.ViewModels.Products
{
    [Serializable]
    public class ProductModel
    {
        public Data.Products.Entities.CategoryCatalogCollection Catalogs { set; get; }
        public CategoryType CategoryType { set; get; }

        [ObjectValidator]
        public Data.Products.Entities.Product Product { set; get; }
        [ObjectValidator]
        public Data.Products.Entities.ProductExOfCourse ExOfCourse { set; get; }
        [ObjectValidator]
        public Data.Products.Entities.ProductSalaryRuleCollection SalaryRules { set; get; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }


        public static ProductModel ToCreateableProductModel(CategoryType categoryType)
        {
            var model = new ProductModel()
            {
                CategoryType = categoryType
                ,
                Product = new Data.Products.Entities.Product()
                {
                    Catalog = "1",
                    CreateTime = DateTime.Now,
                    SubmitTime = DateTime.Now,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    ModifyTime = DateTime.Now
                }
                ,
                ExOfCourse = new Data.Products.Entities.ProductExOfCourse()
                ,
                SalaryRules = new Data.Products.Entities.ProductSalaryRuleCollection()
            };
            return model;
        }

    }
}