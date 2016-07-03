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
using PPTS.Web.MVC.Library.Filters;
using MCS.Library.Principal;
using System.Collections.Generic;
using PPTS.Data.Common;
using MCS.Web.MVC.Library.Models;
using PPTS.Data.Customers.Adapters;
using MCS.Web.MVC.Library.Models.Workflow;

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
        [PPTSJobFunctionAuthorize("PPTS:录入/客服管理列表（客服详情）,客服管理列表（客服详情）-本部门,客服管理列表（客服详情）-本校区,客服管理列表（客服详情）-本分公司,客服管理列表（客服详情）-全国")]
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
        [PPTSJobFunctionAuthorize("PPTS:录入/客服管理列表（客服详情）,客服管理列表（客服详情）-本部门,客服管理列表（客服详情）-本校区,客服管理列表（客服详情）-本分公司,客服管理列表（客服详情）-全国")]
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
        [PPTSJobFunctionAuthorize("PPTS:新增客服记录")]
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

        [PPTSJobFunctionAuthorize("PPTS:新增客服记录")]
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

        #region 工作流相关
        public void AccessProcess(EditableCustomerServiceModel model)
        {
            AccessProcessModel modelWF = new AccessProcessModel();

            modelWF.ApplyName = DeluxeIdentity.CurrentUser.Name;
            modelWF.CampusName = model.CustomerService.CampusName;
            modelWF.CustomerCode = model.PCustomer.CustomerCode;
            modelWF.CustomerID = model.CustomerService.CustomerID;
            modelWF.CustomerName = model.PCustomer.CustomerName;
            modelWF.ServiceID = model.CustomerService.ServiceID;
            modelWF.ProcessID = model.ProcessID;
            modelWF.ActivityID = model.ActivityID;
            modelWF.ActivityName = model.CustomerService.HandlerJobName;

            Dictionary<string, object> p = new Dictionary<string, object>();
            p["AccessProcessModel"] = modelWF;

          

            string wfName = WorkflowNames.CustomerServiceProcss;
            WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);

            bool firstPersonFlg = false;
            List<CustomerServiceItem> items = EditableCustomerServiceModel.LoadByServiceID(modelWF.ServiceID);
            if (items.Count == 0)
            {
                firstPersonFlg = true;
            }
            

            if (model.CustomerService.HandlerJobName == "")
            {
                throw new Exception("先分配下一处理人的岗位");
            }

            EditableStudentSverviceExecutor executor = new EditableStudentSverviceExecutor(model);

            executor.Execute();

            if (firstPersonFlg)
            {
                modelWF.ResourceID = model.CustomerService.ServiceID;

                FirstPersonProcess(p, modelWF, wfHelper, model);
            }
            else
            {
                modelWF.ResourceID = model.ResourceID;

                NextPersonProcess(p, modelWF, wfHelper, model);
            }
        }
        /// <summary>
        /// 流程发起人调用
        /// </summary>
        /// <param name="p"></param>
        /// <param name="modelWF"></param>
        /// <param name="wfHelper"></param>
        public void FirstPersonProcess(Dictionary<string, object> p, AccessProcessModel modelWF, WorkflowHelper wfHelper, EditableCustomerServiceModel model)
        {
            WfClientDynamicProcessStartupParameter parameter = new WfClientDynamicProcessStartupParameter
            {
                Parameters = p,
                ResourceID = modelWF.ResourceID, //这里用真实的resource id代替
                ActivityName = modelWF.ActivityName,
                TaskTitle = string.Format("客服({0})：{1}", model.CustomerService.ServiceType, model.PCustomer.CustomerName),  //这里需要改成实际需求
                TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/custservice/nextProcess/" + model.CustomerService.ServiceID,    //这个GUIID从哪里取得，请改成动态的
                RuntimeProcessName = "客服(" + model.CustomerService.ServiceType + ")",
            };

            wfHelper.StartupDynamicWorkflow(parameter);
        }

        /// <summary>
        /// 流程中转人调用
        /// </summary>
        public void NextPersonProcess(Dictionary<string, object> p, AccessProcessModel modelWF, WorkflowHelper wfHelper, EditableCustomerServiceModel model)
        {
            WfClientDynamicProcessMovetoParameter parameter = new WfClientDynamicProcessMovetoParameter
            {
                Parameters = p,
                ResourceID = modelWF.ResourceID, //这里用真实的resource id代替
                ProcessID = modelWF.ProcessID,
                ActivityID = modelWF.ActivityID,
                Comment = modelWF.ProcessMemo,
                ActivityName = modelWF.ActivityName
            };

            wfHelper.MovetoDynamicWorkflow(parameter);
        }

        #endregion

        #region api/Present/exportcustomerservice
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:客服记录导出")]
        public HttpResponseMessage ExportCustomerService([ModelBinder(typeof(FormBinder))]CustomerServiceQueryCriteriaModel criteria)
        {
            var wb = WorkBook.CreateNew();
            var sheet = wb.Sheets["sheet1"];
            var tableDesp = new TableDescription("学大客服");
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            var pageData = CustomerServiceDataSource.Instance.LoadCustomerService(criteria.PageParams, criteria, criteria.OrderBy);
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("分公司", typeof(string))) { PropertyName = "OrgName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("校区", typeof(string))) { PropertyName = "CampusName" });
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
                        var ct = dictionaries["Customer_ComplaintTimes"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
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