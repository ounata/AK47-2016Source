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
using System.Net.Http;
using System.Web.Http.ModelBinding;
using MCS.Web.MVC.Library.ModelBinder;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Web.MVC.Library.ApiCore;
using System.Data;
using System.Linq;
using System;
using PPTS.Web.MVC.Library.Filters;

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
        [PPTSJobFunctionAuthorize("PPTS:回访管理列表（学员视图-回访、回访详情）,回访管理列表（学员视图-回访、回访详情）-本部门,回访管理列表（学员视图-回访、回访详情）-本校区,回访管理列表（学员视图-回访、回访详情）-本分公司,回访管理列表（学员视图-回访、回访详情）全国")]
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
        [PPTSJobFunctionAuthorize("PPTS:回访管理列表（学员视图-回访、回访详情）,回访管理列表（学员视图-回访、回访详情）-本部门,回访管理列表（学员视图-回访、回访详情）-本校区,回访管理列表（学员视图-回访、回访详情）-本分公司,回访管理列表（学员视图-回访、回访详情）全国")]
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
        [PPTSJobFunctionAuthorize("PPTS:录入/编辑回访")]
        public void CreateCustomerVisit(CreatableCustomerVisitModel model)
        {
            AddCustomerVisitExecutor executor = new AddCustomerVisitExecutor(model);

            executor.Execute();
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:录入/编辑回访")]
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

        [PPTSJobFunctionAuthorize("PPTS:录入/编辑回访")]
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

        #region api/customerservices/exportcustVisits

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:回访管理列表（学员视图-回访、回访详情）,回访管理列表（学员视图-回访、回访详情）-本部门,回访管理列表（学员视图-回访、回访详情）-本校区,回访管理列表（学员视图-回访、回访详情）-本分公司,回访管理列表（学员视图-回访、回访详情）全国")]
        public HttpResponseMessage ExportCustomerVisit([ModelBinder(typeof(FormBinder))]CustomerVisitQueryCriteriaModel criteria)
        {
            var wb = WorkBook.CreateNew();
            var sheet = wb.Sheets["sheet1"];
            var tableDesp = new TableDescription("回访记录");
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            var pageData = CustomerVisitDataSource.Instance.LoadCustomerVisit(criteria.PageParams, criteria, criteria.OrderBy);
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员机构", typeof(string))) { PropertyName = "CampusName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长姓名", typeof(string))) { PropertyName = "ParentName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("回访时间", typeof(string))) { PropertyName = "VisitTime", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("回访方式", typeof(string))) { PropertyName = "VisitWay" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("回访类型", typeof(string))) { PropertyName = "VisitType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("回访人", typeof(string))) { PropertyName = "VisitorName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长满意度", typeof(string))) { PropertyName = "Satisficing" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("回访内容", typeof(string))) { PropertyName = "VisitContent" });
            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerVisitModel), typeof(PotentialCustomer));

            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {
                switch (param.ColumnDescription.PropertyName)
                {
                    case "VisitWay":
                        var vw = dictionaries["C_CODE_ABBR_Customer_CRM_ReturnWay"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != vw ? (null == vw.FirstOrDefault() ? null : vw.FirstOrDefault().Value) : null;
                        break;
                    case "VisitType":
                        var vt = dictionaries["C_CODE_ABBR_BO_Customer_ReturnInfoType"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != vt ? (null == vt.FirstOrDefault() ? null : vt.FirstOrDefault().Value) : null;
                        break;
                    case "Satisficing":
                        var ss = dictionaries["C_CODE_ABBR_BO_Customer_Satisfaction"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != ss ? (null == ss.FirstOrDefault() ? null : ss.FirstOrDefault().Value) : null;
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("回访记录.xlsx");
        }

        #endregion
    }
}