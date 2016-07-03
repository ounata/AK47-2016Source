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
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Products.Common;
using PPTS.Data.Common;
using MCS.Web.MVC.Library.Models.Workflow;
using MCS.Web.MVC.Library.ApiCore;

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

        [PPTSJobFunctionAuthorize("PPTS:订购")]
        [HttpPost]
        public QueryClassGroupQueryResult GetClassGroupProducts(QueryClassGroupCriteriaModel criteria)
        {
            var dataSource = ProductClassStatsViewDataSource.Instance;
            dataSource.CampusIDs = criteria.CampusIDs;

            return new QueryClassGroupQueryResult
            {
                QueryResult = dataSource.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderClassGroupProductView)),
            };
        }

        [PPTSJobFunctionAuthorize("PPTS:订购")]
        [HttpPost]
        public PagedQueryResult<OrderClassGroupProductView, OrderClassGroupProductViewCollection> GetPagedClassGroupProducts(QueryClassGroupCriteriaModel criteria)
        {
            var dataSource = ProductClassStatsViewDataSource.Instance;
            dataSource.CampusIDs = criteria.CampusIDs;
            return dataSource.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }


        #endregion

        #region api/Products/getallProducts


        [PPTSJobFunctionAuthorize("PPTS:资产兑换,订购,产品管理列表（产品详情）,产品管理列表（产品详情）-本校区,产品管理列表（产品详情）-本分公司,产品管理列表（产品详情）-全国")]
        [HttpPost]
        public ProductQueryResult GetAllProducts(ProductQueryCriteriaModel criteria)
        {
            var dataSource = ProductViewDataSource.Instance;
            dataSource.CampusIDs = DeluxeIdentity.CurrentUser.PermisstionFilter(criteria.CampusIDs);
            //dataSource.CampusIDs = criteria.CampusIDs;
            return new ProductQueryResult
            {
                QueryResult = dataSource.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ProductView)),
            };
        }


        [PPTSJobFunctionAuthorize("PPTS:资产兑换,订购,产品管理列表（产品详情）,产品管理列表（产品详情）-本校区,产品管理列表（产品详情）-本分公司,产品管理列表（产品详情）-全国")]
        [HttpPost]
        public PagedQueryResult<ProductView, ProductViewCollection> GetPagedProducts(ProductQueryCriteriaModel criteria)
        {
            var dataSource = ProductViewDataSource.Instance;
            dataSource.CampusIDs = DeluxeIdentity.CurrentUser.PermisstionFilter(criteria.CampusIDs);
            return dataSource.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        [PPTSJobFunctionAuthorize("PPTS:产品管理列表（产品详情）,产品管理列表（产品详情）-本校区,产品管理列表（产品详情）-本分公司,产品管理列表（产品详情）-全国")]
        [HttpGet]
        public ProductViewModel GetProduct(string id)
        {
            return ProductViewModel.GetProducViewtById(id, DeluxeIdentity.CurrentUser.PermisstionFilter());
        }


        [HttpPost]
        public dynamic GetProductByWorkflow(dynamic wfParams)
        {
            WfClientSearchParameters p = new WfClientSearchParameters();

            p.ResourceID = wfParams.resourceID;
            p.ActivityID = wfParams.activityID;
            p.ProcessID = wfParams.processID;

            var model = ProductViewModel.GetProducViewtById(p.ResourceID, null);
            return new { ClientProcess = WfClientProxy.GetClientProcess(p), Model = model };
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ProductViewCollectionModel GetProducts(string[] ids)
        //{
        //    return ProductViewCollectionModel.GetProductViewCollectionByIds(ids);
        //}


        #endregion

        #region api/Products/createProduct-CopyProduct

        [PPTSJobFunctionAuthorize("PPTS:新建/删除/止售/延期产品-本分公司")]
        [HttpGet]
        public ProductModel CreateProduct(int type = 1)
        {
            return ProductModel.ToCreateableProductModel((CategoryType)type); ;
        }

        [PPTSJobFunctionAuthorize("PPTS:新建/删除/止售/延期产品-本分公司")]
        [HttpPost]
        public IHttpActionResult SubmitProduct(ProductModel model)
        {
            model.CampusIDs = DeluxeIdentity.CurrentUser.PermisstionFilter(model.CampusIDs);
            model.CurrentUser = DeluxeIdentity.CurrentUser;
            return Ok(new AddProductExecutor(model) { NeedValidation = true }.Execute());

        }

        [PPTSJobFunctionAuthorize("PPTS:新建/删除/止售/延期产品-本分公司")]
        [HttpGet]
        public ProductViewModel CopyProduct(string id)
        {
            return ProductViewModel.GetProducViewtById(id, DeluxeIdentity.CurrentUser.PermisstionFilter()).FillCatalogs().Empty();
        }

        #endregion

        [PPTSJobFunctionAuthorize("PPTS:新建/删除/止售/延期产品-本分公司")]
        [HttpPost]
        public IHttpActionResult StopSellProduct(StopSellProductModel model)
        {
            model.CurrentUser = DeluxeIdentity.CurrentUser;
            var result = new PPTSProductExecutor("StopSell")
            {
                Model = model.FillProduct().Model,
                CampusIDs = DeluxeIdentity.CurrentUser.PermisstionFilter()
            }.Execute();
            return Ok(new { EndDate = result });
        }

        [PPTSJobFunctionAuthorize("PPTS:新建/删除/止售/延期产品-本分公司")]
        [HttpPost]
        public IHttpActionResult DelayProduct(DelayProductModel model)
        {
            model.CurrentUser = DeluxeIdentity.CurrentUser;
            return Ok(new PPTSProductExecutor("DelaySell")
            {
                Model = model.FillProduct().Model,
                CampusIDs = DeluxeIdentity.CurrentUser.PermisstionFilter()
            }.Execute());
        }

        [PPTSJobFunctionAuthorize("PPTS:新建/删除/止售/延期产品-本分公司")]
        [HttpPost]
        public IHttpActionResult DelProduct(string[] ids)
        {
            return Ok(new PPTSProductExecutor("DelProduct")
            {
                ProductIds = ids,
                CampusIDs = DeluxeIdentity.CurrentUser.PermisstionFilter()
            }.Execute());
        }



    }


}