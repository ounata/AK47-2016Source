using System.Linq;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerMeetings;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using MCS.Library.Principal;
using System;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Common;
using MCS.Web.MVC.Library.Models;
using System.Net.Http;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.ModelBinder;
using System.Web.Http.ModelBinding;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Office.OpenXml.Excel;
using System.Data;
using PPTS.Data.Common.Entities;
using System.Collections.Generic;
using PPTS.Web.MVC.Library.Filters;

namespace PPTS.WebAPI.Customers.Controllers
{
    /// <summary>
    /// 教学服务会Controller
    /// </summary>
    [ApiPassportAuthentication]
    public class CustomerMeetingsController : ApiController
    {

        #region api/customermeetings/getallcustomermeetings
        /// <summary>
        /// 教学服务会默认查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
//        [PPTSJobFunctionAuthorize(@"PPTS:教学服务会管理（学员视图-教学服务会、详情查看）,
//教学服务会管理（学员视图-教学服务会、详情查看）-本部门,
//教学服务会管理（学员视图-教学服务会、详情查看）-本校区,
//教学服务会管理（学员视图-教学服务会、详情查看）-本分公司,
//教学服务会管理（学员视图-教学服务会、详情查看）-全国")]
        [HttpPost]
        public CustomerMeetingQueryResult GetAllCustomerMeetings(CustomerMeetingCriteriaModel criteria)
        {
            return new CustomerMeetingQueryResult
            {
                QueryResult = CustomerMeetingDataSource.Instance.GetCustomerMeetingsList(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerMeetingQueryModel), typeof(Parent))
            };
        }
        #endregion

        #region api/customermeetings/getpagedcustomermeetings
        /// <summary>
        /// 教学服务会分页查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
//        [PPTSJobFunctionAuthorize(@"PPTS:教学服务会管理（学员视图-教学服务会、详情查看）,
//教学服务会管理（学员视图-教学服务会、详情查看）-本部门,
//教学服务会管理（学员视图-教学服务会、详情查看）-本校区,
//教学服务会管理（学员视图-教学服务会、详情查看）-本分公司,
//教学服务会管理（学员视图-教学服务会、详情查看）-全国")]
        [HttpPost]
        public PagedQueryResult<CustomerMeetingQueryModel, CustomerMeetingQueryCollection> GetPagedCustomerMeetings(CustomerMeetingCriteriaModel criteria)
        {
            return CustomerMeetingDataSource.Instance.GetCustomerMeetingsList(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/customermeetings/savecustomermeetings
        /// <summary>
        /// 新增/编辑会议
        /// </summary>
        /// <param name="criteria"></param>
        [HttpPost]
        //[PPTSJobFunctionAuthorize(@"PPTS:新增/编辑教学服务会")]
        public void SaveCustomerMeetings(EditCustomerMeetingModel model)
        {
            if (string.IsNullOrEmpty(model.CustomerMeeting.ResourceId))
            {
                #region 写入权限验证
                PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerMeeting>
                   .GetInstance(Data.Customers.ConnectionDefine.PPTSCustomerConnectionName).CheckEditAuth(model.CustomerMeeting.MeetingID, model.CustomerMeeting.CustomerID);
                #endregion
            }
            CustomerMeetingExecutor cmExcutor = new CustomerMeetingExecutor(model);
            cmExcutor.Execute();
        }

        //[HttpPost]
        ////[ApiPassportAuthentication]
        //public void SaveCustomerMeetings(MaterialModel model)
        //{
        //    var m = model;
        //}

        #endregion


        #region api/customermeetings/loadcustomermeetingsdictionaries
        /// <summary>
        /// 加载数据词典
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public CustomerMeetingQueryResult LoadCustomerMeetingsDictionaries(CustomerMeetingCriteriaModel criteria)
        {
            return new CustomerMeetingQueryResult
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerMeetingQueryModel), typeof(CustomerMeetingItem), typeof(Parent)),
                CustomerName=CustomerAdapter.Instance.Load(criteria.CustomerId).CustomerName
            };
        }
        #endregion

        #region api/customermeetings/getCustomerMeeting
        /// <summary>
        /// 根据会议ID获取获取会议
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public EditCustomerMeetingModel GetCustomerMeeting(string id)
        {
            var model = new EditCustomerMeetingModel
            {
                CustomerMeeting = CustomerMeetingAdapter.Instance.Load(id),
                Items = CustomerMeetingItemAdapter.Instance.LoadItems(id),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerMeetingQueryModel), typeof(CustomerMeetingItem), typeof(Parent))
            };
            model.CustomerMeeting.Materials = MaterialModelHelper.GetInstance(CustomerMeetingAdapter.Instance.ConnectionName).LoadByResourceID(id);
            model.CustomerMeeting.CustomerName = CustomerAdapter.Instance.Load(model.CustomerMeeting.CustomerID).CustomerName;
            return model;
        }
        #endregion

        #region api/customermeetings/uploadMaterial--上传文件相关
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public MaterialModelCollection UploadMaterial(HttpRequestMessage request)
        {
            return request.ProcessMaterialUpload();
        }
        [HttpPost]
        public HttpResponseMessage DownloadMaterial([ModelBinder(typeof(FormBinder))] MaterialModel material)
        {
            return material.ProcessMaterialDownload();
        }
        #endregion

        #region api/customermeetings/exportCustomerMeetings
        [HttpPost]
        //[PPTSJobFunctionAuthorize(@"PPTS:导出教学服务会")]
        public HttpResponseMessage ExportCustomerMeetings([ModelBinder(typeof(FormBinder))]CustomerMeetingCriteriaModel criteria)
        {
            var wb = WorkBook.CreateNew();
            var sheet = wb.Sheets["sheet1"];
            var tableDesp = new TableDescription("教学服务会");
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            var pageData = CustomerMeetingDataSource.Instance.GetCustomerMeetingsList(criteria.PageParams, criteria, criteria.OrderBy);
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("会议时间", typeof(string))) { PropertyName = "MeetingTime", Format = "yyyy-MM-dd HH:mm" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("所属校区", typeof(string))) { PropertyName = "CampusName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长姓名", typeof(string))) { PropertyName = "ParentName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("当前年级", typeof(string))) { PropertyName = "Grade" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("会议类型", typeof(string))) { PropertyName = "MeetingType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("会议组织人", typeof(string))) { PropertyName = "OrganizerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长满意度", typeof(string))) { PropertyName = "Satisficing" });
            //tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("附件", typeof(string))) { PropertyName = "ReplyContent" });
            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerMeetingQueryModel), typeof(Parent));

            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {
                switch (param.ColumnDescription.PropertyName)
                {
                    case "Grade":
                        //var g = dictionaries["c_codE_ABBR_CUSTOMER_GRADE"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        //cell.Value = null != g ? (null == g.FirstOrDefault() ? null : g.FirstOrDefault().Value) : null;
                        cell.Value = CalCeelValue(dictionaries["c_codE_ABBR_CUSTOMER_GRADE"], param.PropertyValue);
                        break;
                    case "MeetingType":
                        //var rt = dictionaries["c_codE_ABBR_Customer_CRM_MainServiceMeeting"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        //cell.Value = null != rt ? (null == rt.FirstOrDefault() ? null : rt.FirstOrDefault().Value) : null;
                        cell.Value = CalCeelValue(dictionaries["C_CODE_ABBR_Customer_CRM_MainServiceMeeting"], param.PropertyValue);
                        break;
                    case "Satisficing":
                        cell.Value = CalCeelValue(dictionaries["c_codE_Abbr_BO_Customer_Satisfaction"], param.PropertyValue);
                        //var ro = dictionaries["c_codE_Abbr_BO_Customer_Satisfaction"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        //cell.Value = null != ro ? (null == ro.FirstOrDefault() ? null : ro.FirstOrDefault().Value) : null;
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("教学服务会.xlsx");
        }
        private object CalCeelValue(IEnumerable<BaseConstantEntity> dictionaries, object pValue)
        {
            var g = dictionaries.Where(c => c.Key.Equals(Convert.ToString(pValue)));
            return null != g ? (null == g.FirstOrDefault() ? null : g.FirstOrDefault().Value) : null;
        }
        #endregion
    }
}