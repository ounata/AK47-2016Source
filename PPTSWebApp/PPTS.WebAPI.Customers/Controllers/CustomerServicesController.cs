using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using MCS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.ViewModels.CustomerServiceItems;
using System.Net.Http;
using System.Web.Http.ModelBinding;
using MCS.Web.MVC.Library.ModelBinder;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Web.MVC.Library.ApiCore;
using System.Data;
using System.Linq;
using System;

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

        #region api/Present/exportcustomerservice
        [HttpPost]
        public HttpResponseMessage ExportCustomerService([ModelBinder(typeof(FormBinder))]CustomerServiceQueryCriteriaModel criteria)
        {
            var wb = WorkBook.CreateNew();
            var sheet = wb.Sheets["sheet1"];
            var tableDesp = new TableDescription("学大客服");
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            var pageData = CustomerServiceDataSource.Instance.LoadCustomerService(criteria.PageParams, criteria, criteria.OrderBy);
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长姓名", typeof(string))) { PropertyName = "ParentName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("当前年级", typeof(string))) { PropertyName = "Grade" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("受理时间", typeof(string))) { PropertyName = "AcceptTime", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("服务类型", typeof(string))) { PropertyName = "ServiceType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("客服受理人", typeof(string))) { PropertyName = "AccepterName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("当前受理状态", typeof(string))) { PropertyName = "ServiceStatus" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("当前受理人", typeof(string))) { PropertyName = "HandlerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("投诉次数", typeof(string))) { PropertyName = "ComplaintTimes" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("校区反馈", typeof(string))) { PropertyName = "SchoolMemo" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("是否升级", typeof(string))) { PropertyName = "IsUpgradeHandle" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("通话录音", typeof(string))) { PropertyName = "VoiceID" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("录音状态", typeof(string))) { PropertyName = "VoiceStatus" });
            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerServiceModel), typeof(PotentialCustomer));

            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {
                switch (param.ColumnDescription.PropertyName)
                {
                    case "Grade":
                        var g = dictionaries["c_codE_ABBR_CUSTOMER_GRADE"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != g ? (null == g.FirstOrDefault() ? null : g.FirstOrDefault().Value) : null;
                        break;
                    case "ServiceType":
                        var st = dictionaries["Customer_ServiceType"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != st ? (null == st.FirstOrDefault() ? null : st.FirstOrDefault().Value) : null;
                        break;
                    case "ServiceStatus":
                        var ss = dictionaries["Customer_ServiceStatus"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != ss ? (null == ss.FirstOrDefault() ? null : ss.FirstOrDefault().Value) : null;
                        break;
                    case "ComplaintTimes":
                        var ct = dictionaries["Customer_AcceptLimit"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != ct ? (null == ct.FirstOrDefault() ? null : ct.FirstOrDefault().Value) : null;
                        break;
                    case "IsUpgradeHandle":
                        cell.Value = (int)param.PropertyValue == 1 ? "是" : "否";
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("学大客服.xlsx");
        }

        #endregion
    }
}