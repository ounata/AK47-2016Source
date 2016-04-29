using MCS.Library.Validation;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Products
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
                CategoryType = categoryType,
                Product = new Product(),
                ExOfCourse = new ProductExOfCourse() { IncomeBelonging = "0", IsCrossCampus = 0 },
                SalaryRules = new ProductSalaryRuleCollection()
            };

            switch (categoryType)
            {
                case CategoryType.OneToOne: model.Product.ProductUnit = ProductUnit.Period; break;
            }

            model.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(
                    typeof(Product),
                    typeof(ProductExOfCourse),
                    typeof(ProductSalaryRule)
                );
            model.Catalogs = CategoryCatalogAdapter.Instance.LoadByCategoryType(categoryType);
            model.Product.Catalog = model.Catalogs.First().Catalog;


            return model;
        }

    }
}