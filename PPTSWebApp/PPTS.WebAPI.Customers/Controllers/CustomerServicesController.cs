using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using MCS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.ViewModels.CustomerServiceItems;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class CustomerServicesController : ApiController
    {
        #region api/customerservices/getallcustomerservices

        /// <summary>
        /// 客服，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        //[ApiPassportAuthentication]
        //[PPTSJobFunctionAuthorize(3")]
        public CustomerServiceListResult GetAllCustomerServices(CustomerServiceQueryCriteriaModel criteria)
        {
            return new CustomerServiceListResult
            {
                QueryResult = CustomerServiceDataSource.Instance.LoadCustomerService(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerService), typeof(PotentialCustomer))
            };
        }

        /// <summary>
        /// 客服查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        ///Class1.cs <returns>返回不带字典的客服数据列表</returns>
        [HttpPost]
        //[ApiPassportAuthentication]
        //[PPTSFunctionAuthorize("PPTS:f1,f2,f3")]
        //[PPTSJobFunctionAuthorize("PPTS:客服列表查看,jf2,jf3")]
        public PagedQueryResult<CustomerServiceModel, CustomerServiceModelCollection> GetPagedCustomerServices(CustomerServiceQueryCriteriaModel criteria)
        {
            return CustomerServiceDataSource.Instance.LoadCustomerService(criteria.PageParams, criteria, criteria.OrderBy);
        }

        [HttpPost]
        public CustomerServiceItemQueryResult GetCustomerServicesItems(CustomerServiceItemQueryCriteriaModel criteria)
        {
            return new CustomerServiceItemQueryResult
            {
                QueryResult = CustomerServiceItemDataSource.Instance.LoadCustomerServiceItems(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerService))
            };
        }

        [HttpPost]
        public CustomerServiceItemQueryResult GetCustomerServicesItemsAll(CustomerServiceItemQueryCriteriaModel criteria)
        {
            return new CustomerServiceItemQueryResult
            {
                QueryResult = CustomerServiceItemDataSource.Instance.LoadCustomerServiceItemsAll(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerService))
            };
        }

        #endregion

        #region api/customerservices/createsustomerservice
        /// <summary>
        /// 初始化添加页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CreatableCustomerServiceModel CreateCustomerService()
        {
            return new CreatableCustomerServiceModel
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatableCustomerServiceModel), typeof(CustomerServiceModel))
            };
        }

        [HttpPost]
        public void CreateCustomerService(CreatableCustomerServiceModel model)
        {
            AddCustomerServiceExecutor executor = new AddCustomerServiceExecutor(model);

            executor.Execute();
        }


        #endregion

        #region api/customerservices/updatecustomerservices

        [HttpGet]
        public EditableCustomerServiceModel UpdateCustomerService(string id)
        {
            return EditableCustomerServiceModel.Load(id);
        }

        public void UpdateCustomerService(EditableCustomerServiceModel model)
        {
            EditableStudentSverviceExecutor executor = new EditableStudentSverviceExecutor(model);

            executor.Execute();
        }

        #endregion


        [HttpGet]
        public EditableCustomerServiceModel GetCustomerServiceInfo(string id)
        {
            return this.UpdateCustomerService(id);
        }
    }
}