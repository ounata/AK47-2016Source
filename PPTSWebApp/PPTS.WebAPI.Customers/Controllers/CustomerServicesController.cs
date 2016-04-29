using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.DataSources;

namespace PPTS.WebAPI.Customers.Controllers
{
    public class CustomerServicesController : ApiController
    {
        #region api/customerservices/getallcustomerservices

        /// <summary>
        /// 潜客查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        //[ApiPassportAuthentication]
        //[PPTSJobFunctionAuthorize("PPTS:潜在客户列表查看,jf2,jf3")]
        public CustomerServiceListResult GetAllCustomerServices(CustomerServiceQueryCriteriaModel criteria)
        {
            return new CustomerServiceListResult
            {
                QueryResult = CustomerServiceDataSource.Instance.LoadCustomerService(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerService), typeof(PotentialCustomer))
            };
        }

        /// <summary>
        /// 潜客查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        ///Class1.cs <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        //[ApiPassportAuthentication]
        //[PPTSFunctionAuthorize("PPTS:f1,f2,f3")]
        //[PPTSJobFunctionAuthorize("PPTS:潜在客户列表查看,jf2,jf3")]
        public PagedQueryResult<CustomerServiceModel, CustomerServiceModelCollection> GetPagedCustomerServices(CustomerServiceQueryCriteriaModel criteria)
        {
            return CustomerServiceDataSource.Instance.LoadCustomerService(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion
    }
}