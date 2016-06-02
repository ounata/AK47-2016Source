using MCS.Library.Data;
using PPTS.Data.Common.Security;
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
using MCS.Web.MVC.Library.Filters;
using MCS.Library.Principal;

namespace PPTS.WebAPI.Products.Controllers
{
    /// <summary>
    /// 产品管理
    /// </summary>
    [ApiPassportAuthentication]
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
            var dataSource = ProductViewDataSource.Instance;
            dataSource.CampusIDs = criteria.CampusIDs;
            return new ProductQueryResult
            {
                QueryResult = dataSource.Query(criteria.PageParams, criteria, criteria.OrderBy),
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
            var dataSource = ProductViewDataSource.Instance;
            dataSource.CampusIDs = criteria.CampusIDs;
            return dataSource.Query(criteria.PageParams, criteria, criteria.OrderBy);
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
        public IHttpActionResult SubmitProduct(ProductModel model)
        {

            model.Product.SubmitterID = model.CreatorID = DeluxeIdentity.CurrentUser.ID;
            model.Product.SubmitterName = model.CreatorName = DeluxeIdentity.CurrentUser.DisplayName;
            model.Product.SubmitterJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            model.Product.SubmitterJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;

            model.CreatorName = DeluxeIdentity.CurrentUser.DisplayName;

            return Ok( new ProductExecutor(model) { NeedValidation=true }.Execute());
            
        }

        [HttpGet]
        public ProductViewModel CopyProduct(string id)
        {
            return ProductViewModel.GetProducViewtById(id).FillCatalogs().Empty();
        }

        #endregion

        [HttpGet]
        public IHttpActionResult StopSellProduct(string id)
        {
            var result = new PPTSProductExecutor("StopSell") { ProductId = id }.Execute();
            return Ok(new {EndDate= result });
        }


        [HttpPost]
        public IHttpActionResult DelayProduct(dynamic param)
        {
            return Ok(new PPTSProductExecutor("DelaySell") { ProductId = param.id, Date = param.endDate }.Execute());
        }

        [HttpPost]
        public IHttpActionResult DelProduct(string []ids)
        {
            return Ok( new PPTSProductExecutor("DelProduct") { ProductId = string.Join( ",", ids) }.Execute());
        }

        

    }


}