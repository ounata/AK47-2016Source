using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using MCS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class CustomerVisitsController : ApiController
    {
        #region api/customerservices/getallCustomerVisits

        /// <summary>
        /// 回访列表，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        //[ApiPassportAuthentication]
        //[PPTSJobFunctionAuthorize(3")]
        public CustomerVisitQueryResult GetAllCustomerVisits(CustomerVisitQueryCriteriaModel criteria)
        {
            return new CustomerVisitQueryResult
            {
                QueryResult = CustomerVisitDataSource.Instance.LoadCustomerVisit(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerVisit))
            };
        }

        /// <summary>
        /// 回访查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        ///Class1.cs <returns>返回不带字典的客服数据列表</returns>
        [HttpPost]
        //[ApiPassportAuthentication]
        //[PPTSFunctionAuthorize("PPTS:f1,f2,f3")]
        //[PPTSJobFunctionAuthorize("PPTS:客服列表查看,jf2,jf3")]
        public PagedQueryResult<CustomerVisitModel, CustomerVisitModelCollection> GetPagedCustomerVisits(CustomerVisitQueryCriteriaModel criteria)
        {
            return CustomerVisitDataSource.Instance.LoadCustomerVisit(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/customervisit/createsustomervisit

        /// <summary>
        /// 初始化添加页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CreatableCustomerVisitModel CreateCustomerVisit()
        {
            return new CreatableCustomerVisitModel
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatableCustomerVisitModel), typeof(CustomerVisitModel))
            };
        }

        [HttpGet]
        public CreatableCustomerVisitModel CreateCustomerVisitByStudnetID(string studentID)
        {
            //return new CreatableCustomerVisitModel(studentID)
            //{
            //    Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatableCustomerVisitModel), typeof(CustomerVisitModel))
            //};

            return CreatableCustomerVisitModel.load(studentID);
             
        }

        [HttpPost]
        public void CreateCustomerVisit(CreatableCustomerVisitModel model)
        {
            AddCustomerVisitExecutor executor = new AddCustomerVisitExecutor(model);

            executor.Execute();
        }

        [HttpPost]
        public void AddVisitBatch(CreatableVisitBatchModel model)
        {
            AddCustomerVisitBatchExecutor executor = new AddCustomerVisitBatchExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/customervisits/updatecustomervisits

        [HttpGet]
        public EditableCustomerVisitModel UpdateCustomerVisit(string id)
        {
            return EditableCustomerVisitModel.Load(id);
        }

        public void UpdateCustomerVisit(EditableCustomerVisitModel model)
        {
            EditableStudentVisitExecutor executor = new EditableStudentVisitExecutor(model);

            executor.Execute();
        }


        public EditableCustomerVisitModel GetCustomerVisitInfo(string id)
        {
            return this.UpdateCustomerVisit(id);
        }

        #endregion
    }
}