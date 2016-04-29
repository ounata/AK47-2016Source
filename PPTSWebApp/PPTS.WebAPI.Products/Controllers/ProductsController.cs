using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.DataSources;
using PPTS.WebAPI.Products.Executors;
using PPTS.WebAPI.Products.ViewModels.Products;
using System.Collections.Generic;
using System.Web.Http;
using MCS.Library.Core;

using PPTS.Data.Products.Entities;
using PPTS.Data.Products;
using System;
using PPTS.Data.Products.Executors;

namespace PPTS.WebAPI.Products.Controllers
{
    /// <summary>
    /// 产品管理
    /// </summary>
    public class ProductsController : ApiController
    {
        public ProductsController() { }

        #region 班组订购

        /// <summary>
        /// 查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        public QueryClassGroupQueryResult GetClassGroupProducts(QueryClassGroupCriteriaModel criteria)
        {
            return new QueryClassGroupQueryResult
            {
                QueryResult = GenericProductDataSource<OrderClassGroupProduct, OrderClassGroupProductCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderClassGroupProduct)),
            };
        }

        /// <summary>
        /// 查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        public PagedQueryResult<OrderClassGroupProduct, OrderClassGroupProductCollection> GetPagedClassGroupProducts(QueryClassGroupCriteriaModel criteria)
        {
            return GenericProductDataSource<OrderClassGroupProduct, OrderClassGroupProductCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }


        #endregion

        #region api/Products/getallProducts


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
                QueryResult = GenericProductDataSource<ProductView, ProductViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ProductView)),
            };
        }



        /// <summary>
        /// 查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        public PagedQueryResult<ProductView, ProductViewCollection> GetPagedProducts(ProductQueryCriteriaModel criteria)
        {
            return GenericProductDataSource<ProductView, ProductViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        [HttpGet]
        public ProductViewModel GetProduct(string id)
        {
            return ProductViewModel.GetProducViewtById(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ProductViewCollectionModel GetProducts(string[] ids)
        {
            return ProductViewCollectionModel.GetProductViewCollectionByIds(ids);
        }


        #endregion

        #region api/Products/createProduct-CopyProduct

        [HttpGet]
        public ProductModel CreateProduct(int type = 1)
        {
            return ProductModel.ToCreateableProductModel((CategoryType)type); ;
        }
        
        [HttpPost]
        public ProductModel SubmitProduct(ProductModel model)
        {
            
            ProductExecutor executor = new ProductExecutor(model);
            executor.Execute();

            return model;
        }

        [HttpGet]
        public ProductViewModel CopyProduct(string id)
        {
            var result = ProductViewModel.GetProducViewtById(id);
            result.Product.ProductID = result.Product.ProductCode = string.Empty;
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
        public bool DelayProduct(dynamic param)
        {
            var executor = new Data.Products.Executors.PPTSProductExecutor("DelaySell") { ProductId = param.id, Date = param.endDate };
            return (bool)executor.Execute();
        }

        [HttpPost]
        public bool DelProduct(string []ids)
        {
            var executor = new Data.Products.Executors.PPTSProductExecutor("DelProduct") { ProductId = string.Join( ",", ids) };
            return (bool)executor.Execute();
        }

        
    }


}