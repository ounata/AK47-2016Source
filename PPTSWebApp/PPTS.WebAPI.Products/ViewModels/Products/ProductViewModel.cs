using PPTS.Data.Common.Adapters;
using PPTS.Data.Products;
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
        public Data.Products.Entities.CategoryCatalogCollection Catalogs { set; get; }
        public CategoryType CategoryType { set; get; }
        public Data.Products.Entities.ProductView Product { set; get; }

        public Data.Products.Entities.ProductSalaryRuleCollection SalaryRules
        {
            get;
            set;
        }
        
        public Data.Products.Entities.ProductExOfCourse ExOfCourse
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<Data.Common.Entities.BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }


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
            executor.FillProductView = new Action<ProductView,
                ProductExOfCourse,
                ProductSalaryRuleCollection>(
                (productView, exOfCourse, salaryRules) =>
                {
                    result.CategoryType = (CategoryType)Convert.ToInt32(productView.CategoryType);
                    result.Product = productView;
                    result.ExOfCourse = exOfCourse;
                    result.SalaryRules = salaryRules;
                }
                );
            executor.Execute();
            return result;
        }

    }


    public class ProductViewCollectionModel
    {
        
        public Data.Products.Entities.ProductViewCollection Products { set; get; }

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