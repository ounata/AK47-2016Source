using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.ModelBinder;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace PPTS.WebAPI.Customers.Controllers
{
    /// <summary>
    /// 学大反馈Controller
    /// </summary>
    [ApiPassportAuthentication]
    public class FeedbackController : ApiController
    {
        #region api/feedback/getcustomerrepliesList

        /// <summary>
        /// 只获取数据词典(特殊需要)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CustomerRepliesQueryResult LoadDictionaries()
        {
            return new CustomerRepliesQueryResult
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerRepliesQueryModel), typeof(Parent))
            };
        }
        /// <summary>
        /// 学大反馈默认查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
//        [PPTSJobFunctionAuthorize(@"PPTS:学大反馈管理（学员视图-家校互动）,
//学大反馈管理（学员视图-家校互动）-本部门,
//学大反馈管理（学员视图-家校互动）-本校区,
//学大反馈管理（学员视图-家校互动）-本分公司,
//学大反馈管理（学员视图-家校互动）-全国")]
        public CustomerRepliesQueryResult GetCustomerRepliesList(CustomerRepliesCriteriaModel criteria)
        {
            return new CustomerRepliesQueryResult
            {
                QueryResult = CustomerRepliesDataSource.Instance.GetCustomerRepliesList(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerRepliesQueryModel), typeof(Parent))
            };
        }
        #endregion

        #region api/feedback/getpagedcustomerreplieslist
        /// <summary>
        ///学大反馈分页查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
//        [PPTSJobFunctionAuthorize(@"PPTS:学大反馈管理（学员视图-家校互动）,
//学大反馈管理（学员视图-家校互动）-本部门,
//学大反馈管理（学员视图-家校互动）-本校区,
//学大反馈管理（学员视图-家校互动）-本分公司,
//学大反馈管理（学员视图-家校互动）-全国")]
        public PagedQueryResult<CustomerRepliesQueryModel, CustomerRepliesQueryCollection> GetPagedCustomerRepliesList(CustomerRepliesCriteriaModel criteria)
        {
            return CustomerRepliesDataSource.Instance.GetCustomerRepliesList(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/feedback/createcustomerreplies
        [HttpPost]
        //[PPTSJobFunctionAuthorize(@"PPTS:联系家长")]
        public void CreateCustomerReplies(EditCustomerRepliesModel model)
        {
            CustomerRepliesExecutor crExecutor = new CustomerRepliesExecutor(model);
            crExecutor.Execute();
        }
        #endregion

        #region api/Present/exportcustomerreplies
        [HttpPost]
        //[PPTSJobFunctionAuthorize(@"PPTS:导出反馈")]
        public HttpResponseMessage ExportCustomerReplies([ModelBinder(typeof(FormBinder))]CustomerRepliesCriteriaModel criteria)
        {
            
            var wb = WorkBook.CreateNew();
            var sheet = wb.Sheets["sheet1"];
            var tableDesp = new TableDescription("学大反馈");
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            var pageData = CustomerRepliesDataSource.Instance.GetCustomerRepliesList(criteria.PageParams, criteria, criteria.OrderBy);
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("反馈时间", typeof(string))) { PropertyName = "ReplyTime", Format = "yyyy-MM-dd HH:mm" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("所属校区", typeof(string))) { PropertyName = "CampusName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长姓名", typeof(string))) { PropertyName = "ParentName"});
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("当前年级", typeof(string))) { PropertyName = "Grade"});
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("员工姓名", typeof(string))) { PropertyName = "ReplierName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("反馈类型", typeof(string))) { PropertyName = "ReplyType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("互动岗位", typeof(string))) { PropertyName = "ReplyObject1"});
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("反馈内容", typeof(string))) { PropertyName = "ReplyContent"});
            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerRepliesQueryModel), typeof(Parent));
          
            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {
                switch (param.ColumnDescription.PropertyName)
                {
                    case "Grade":
                        var g = dictionaries["c_codE_ABBR_CUSTOMER_GRADE"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value =null!=g?(null== g.FirstOrDefault()?null: g.FirstOrDefault().Value) :null;
                        break;
                    case "ReplyType":
                        var rt = dictionaries["c_codE_ABBR_Customer_ReplyType"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != rt ? (null == rt.FirstOrDefault() ? null : rt.FirstOrDefault().Value) : null;
                        break;
                    case "ReplyObject1":
                        var ro=dictionaries["c_codE_ABBR_Customer_ReplyObject"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != ro ? (null == ro.FirstOrDefault() ? null : ro.FirstOrDefault().Value) : null;
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("学大反馈.xlsx");
        }
        
        #endregion
    }
}
