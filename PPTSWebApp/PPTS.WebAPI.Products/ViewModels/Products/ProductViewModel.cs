using PPTS.Data.Common.Adapters;
using PPTS.Data.Products;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.Data.Products.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Products
{

    public class ProductViewModel
    {
        public CategoryCatalogCollection Catalogs { private set; get; }
        public CategoryType CategoryType { set; get; }
        public ProductView Product { private set; get; }

        public ProductSalaryRuleCollection SalaryRules { set; get; }

        public ProductExOfCourse ExOfCourse { set; get; }

        public ProductPermissionCollection Permissions { set; get; }

        public IDictionary<string, IEnumerable<Data.Common.Entities.BaseConstantEntity>> Dictionaries { set; get; }


        public static ProductViewModel GetProducViewtById(string id)
        {
            var result = new ProductViewModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(
                    typeof(ProductView),
                    typeof(ProductExOfCourse),
                    typeof(ProductSalaryRule)
                )
            };

            var executor = new PPTSProductExecutor("GetProductView") { ProductId = id };
            executor.FillProductView = (productView, exOfCourse, salaryRules,permissions) =>
            {
                result.CategoryType = (CategoryType)Convert.ToInt32(productView.CategoryType);
                result.Product = productView;
                result.ExOfCourse = exOfCourse;
                result.SalaryRules = salaryRules;
                result.Permissions = permissions;
            };
            executor.Execute();
            return result;
        }

        public ProductViewModel Empty()
        {
            Product.ProductID = Product.ProductCode = string.Empty;
            return this;
        }

        public ProductViewModel FillCatalogs() {
            Catalogs = CategoryCatalogAdapter.Instance.LoadByCategoryType((CategoryType)Convert.ToInt32(Product.CategoryType));
            return this;
        }
    }


    public class ProductViewCollectionModel
    {
        
        public ProductViewCollection Products { set; get; }

        public IDictionary<string, IEnumerable<Data.Common.Entities.BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public static ProductViewCollectionModel GetProductViewCollectionByIds(string[] ids)
        {
            var result = new ProductViewCollectionModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(
                    typeof(ProductView)
                )
            };

            var getProductViewExecutor = new Data.Products.Executors.PPTSProductExecutor("GetProducts") { ProductIds = ids };
            result.Products = (ProductViewCollection)getProductViewExecutor.Execute();

            return result;
        }

    }



}