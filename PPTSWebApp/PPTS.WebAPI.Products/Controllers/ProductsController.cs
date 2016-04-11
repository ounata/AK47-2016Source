using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.DataSources;
using PPTS.WebAPI.Product.Executors;
using PPTS.WebAPI.Product.ViewModels.Products;
using System.Collections.Generic;
using System.Web.Http;
using MCS.Library.Core;

using Entities = PPTS.Data.Products.Entities;
using System;
using PPTS.Data.Products;

namespace PPTS.WebAPI.Product.Controllers
{
    /// <summary>
    /// 产品管理
    /// </summary>
    public class ProductsController : ApiController
    {
        #region api/Products/getallProducts

        [HttpGet]
        public ProductQueryCriteriaModel Test()
        {
            //return new ProductQueryResult() {  QueryResult=new PagedQueryResult<Data.Products.Entities.Product, Data.Products.Entities.ProductCollection>()};
            return new ProductQueryCriteriaModel() { PageParams = new PageRequestParams() };
        }


        /// <summary>
        /// 查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        public ProductQueryResult GetAllProducts(ProductQueryCriteriaModel criteria)
        {
            return new ProductQueryResult
            {
                QueryResult = GenericProductViewDataSource<Entities.ProductView, Entities.ProductViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Entities.ProductView)),
            };
        }

        /// <summary>
        /// 查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        public PagedQueryResult<Entities.ProductView, Entities.ProductViewCollection> GetPagedProducts(ProductQueryCriteriaModel criteria)
        {
            return GenericProductViewDataSource<Entities.ProductView, Entities.ProductViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        [HttpGet]
        public ProductViewModel GetProduct(string id)
        {

            var result = new ProductViewModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(
                    typeof(Entities.Product),
                    typeof(Entities.ProductExOfCourse),
                    typeof(Entities.ProductSalaryRule)
                )
            };

            Data.Products.Executors.PPTSProductExecutor getProductViewExecutor = new Data.Products.Executors.PPTSProductExecutor("GetProductView") { ProductId=id };
            getProductViewExecutor.FillProductView = new Action<Entities.ProductView,
                Entities.ProductExOfCourse,
                Entities.ProductSalaryRuleCollection>(
                (productView, exOfCourse, salaryRules) =>
                {
                    result.Product = productView;
                    result.ExOfCourse = exOfCourse;
                    result.SalaryRules = salaryRules;
                }
                );
            getProductViewExecutor.Execute();

            return result;
        }

        #endregion

        #region api/Products/createProduct-CopyProduct

        [HttpGet]
        public ProductModel CreateProduct(int type = 1)
        {
            var categoryType = (CategoryType)type;

            var model = ProductModel.ToCreateableProductModel(categoryType);
            model.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(
                    typeof(Entities.Product),
                    typeof(Entities.ProductExOfCourse),
                    typeof(Entities.ProductSalaryRule)
                );
            model.Catalogs = CategoryCatalogAdapter.Instance.LoadByCategoryType(categoryType);
            return model;
        }
        
        [HttpPost]
        public ProductModel SubmitProduct(ProductModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Product.ProductID))
            {
                model.Product.ProductID = Guid.NewGuid().ToString();
            }
            if (model.CategoryType == CategoryType.OneToOne)
            {
                model.Product.StartDate = model.Product.EndDate = DateTime.Parse("3000-12-31");
            }
            ProductExecutor executor = new ProductExecutor(model);
            executor.Execute();

            return model;
        }

        [HttpGet]
        public ProductViewModel CopyProduct(string id)
        {
            //var categoryType = (CategoryType)type;

            var result = new ProductViewModel() { };

            var getProductViewExecutor = new Data.Products.Executors.PPTSProductExecutor("GetProductView"){ ProductId=id };
            getProductViewExecutor.FillProductView = new Action<Entities.ProductView,
                Entities.ProductExOfCourse,
                Entities.ProductSalaryRuleCollection>(
                (productView, exOfCourse, salaryRules) =>
                {
                    result.CategoryType = (CategoryType)Convert.ToInt32(productView.CategoryType);
                    result.Product = productView;
                    result.ExOfCourse = exOfCourse;
                    result.SalaryRules = salaryRules;
                }
                );
            getProductViewExecutor.Execute();

            result.Product.ProductID = string.Empty;
            result.Catalogs = CategoryCatalogAdapter.Instance.LoadByCategoryType((CategoryType)Convert.ToInt32(result.Product.CategoryType));
            return result;
        }

        #endregion

        [HttpGet]
        public bool StopSellProduct(string id)
        {
            var executor = new Data.Products.Executors.PPTSProductExecutor("StopSell") { ProductId = id };

            return (bool)executor.Execute();
        }

        [HttpPost]
        public string StartSellProduct(string id,DateTime startDate)
        {
            var executor = new Data.Products.Executors.PPTSProductExecutor("StartSell") { ProductId = id , StartDate=startDate};
            if ((bool)executor.Execute()) { return DateTime.Now.ToString("yyyy-MM-dd"); }
            return string.Empty;
        }

        [HttpPost]
        public bool DelProduct(string []ids)
        {
            var executor = new Data.Products.Executors.PPTSProductExecutor("DelProduct") { ProductId = string.Join( ",", ids) };
            return (bool)executor.Execute();
        }


    }

}