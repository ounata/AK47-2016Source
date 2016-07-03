using MCS.Library.Data;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using PPTS.WebAPI.Customers.ViewModels.CustomerVerifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class CustomerVerifiesController : ApiController
    {
        #region api/customerVerifies/getAllCustomerVerifies

        /// <summary>
        /// 上门确认记录查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的跟进记录列表</returns>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:上门管理,上门管理-本部门,上门管理-本校区,上门管理-本分公司,上门管理-全国")]
        public CustomerVerifyQueryResult GetAllCustomerVerifies(CustomerVerifyQueryCriteriaModel criteria)
        {
            return new CustomerVerifyQueryResult
            {
                QueryResult = CustomerVerifiesDataSource.Instance.LoadCustomerVerify(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerVerifyQueryModel))
            };
        }

        /// <s6ummary>
        /// 上门确认记录查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的跟进记录列表</returns>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:上门管理,上门管理-本部门,上门管理-本校区,上门管理-本分公司,上门管理-全国")]
        public PagedQueryResult<CustomerVerifyQueryModel, CustomerVerifiesQueryCollection> GetPagedCustomerVerifies(CustomerVerifyQueryCriteriaModel criteria)
        {
            return CustomerVerifiesDataSource.Instance.LoadCustomerVerify(criteria.PageParams, criteria, criteria.OrderBy);
        }



        #endregion

        #region api/customerfollows/createCustomerVerifies

        /// <summary>
        /// 新建上门记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CustomerVerifyResult CreateCustomerVerify()
        {
            return CustomerVerifyResult.CreateCustomerVerify();
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:新增上门记录")]
        public void SaveCustomerVerify(CustomerVerifyModel model)
        {
            #region 写入权限验证
            PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerVerify>
               .GetInstance(Data.Customers.ConnectionDefine.PPTSCustomerConnectionName).CheckEditAuth("", model.CustomerID);
            #endregion

            AddCustomerVerifyExecutor executor = new AddCustomerVerifyExecutor(model);
            executor.Execute();
        }

        #endregion
    }
}