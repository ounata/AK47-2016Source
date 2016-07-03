using MCS.Library.Data;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.ModelBinder;
using MCS.Web.MVC.Library.Models.Workflow;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Products.Common;
using PPTS.WebAPI.Products.DataSources;
using PPTS.WebAPI.Products.Executors;
using PPTS.WebAPI.Products.ViewModels.Discounts;
using PPTS.WebAPI.Products.ViewModels.Presents;
using PPTS.WebAPI.Products.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace PPTS.WebAPI.Products.Controllers
{
    [ApiPassportAuthentication]
    public class PresentController : ApiController
    {
        private void Check(string PresentID) {
            string[] result = null;
            PresentPermissionsApplieCollection PresentPermissionsApplieCollection = GenericDiscountAdapter<PresentPermissionsApply, PresentPermissionsApplieCollection>.Instance.Load(builder => builder.AppendItem("PresentID", PresentID));
            if (PresentPermissionsApplieCollection != null && PresentPermissionsApplieCollection.Count > 0) {
                result = new string[PresentPermissionsApplieCollection.Count];
            }
            for (int i = 0; i < PresentPermissionsApplieCollection.Count; i++)
            {
                result[i] = PresentPermissionsApplieCollection[i].CampusID;
            }
            string[] cc = DeluxeIdentity.CurrentUser.PermisstionFilter(result);
            if (cc == null || cc.Length == 0)
                throw new Exception("改岗位没有操作此买赠表的数据权限！");
        }

        #region api/present/getCurrentBranchName
        public PresentDetialModel GetCurrentBranchName()
        {
            PresentDetialModel result = new PresentDetialModel() { Present = new PresentViewModel() { PresentName = string.Empty } };
            var branch = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Branch);
            if (branch != null)
                result.Present.PresentName = PPTS.Data.Products.Helper.GetPresentName(branch.Name);
            return result;
        }
        #endregion

        #region api/present/getAllPresents
        [PPTSJobFunctionAuthorize("PPTS:买赠表管理-本分公司,买赠表管理-全国")]
        [HttpPost]
        public PresentsQueryResultModel getAllPresents(PresentsQueryCriteriaModel criteria) {
            criteria.CampusIDs = DeluxeIdentity.CurrentUser.PermisstionFilter(criteria.CampusIDs);
            return new PresentsQueryResultModel()
            {
                QueryResult = PresentDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Present))
            };
        }

        [PPTSJobFunctionAuthorize("PPTS:买赠表管理-本分公司,买赠表管理-全国")]
        [HttpPost]
        public PagedQueryResult<Present, PresentCollection> GetPagedPresents(PresentsQueryCriteriaModel criteria)
        {
            criteria.CampusIDs = DeluxeIdentity.CurrentUser.PermisstionFilter(criteria.CampusIDs);
            return PresentDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/present/createPresent
        [PPTSJobFunctionAuthorize("PPTS:新增/编辑/删除/停用/打印买赠表-本分公司")]
        [HttpPost]
        public void CreatePresent(CreatePresentModel model)
        {
            string wfName = WorkflowNames.Present;
            WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);
            if (wfHelper.CheckWorkflow(true))
            {
                CreatePresentExecutor executor = new CreatePresentExecutor(model);
                executor.Execute();

                Dictionary<string, object> ps = new Dictionary<string, object>();
                ps["PresentID"] = model.Present.PresentID;
                wfHelper.StartupWorkflow(new WorkflowStartupParameter()
                {
                    Parameters = ps,
                    ResourceID = model.Present.PresentID,
                    TaskTitle = string.Format("买赠表审批：{0}", model.Present.PresentName),
                    TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/present/present-workflow"
                });
            }
        }
        #endregion

        #region api/present/getPresentWorkflowInfo
        [HttpPost]
        public PresentWorkflowResultModel getPresentWorkflowInfo(PresentWorkflowCriteriaModel criteria) {
            PresentWorkflowResultModel result = new PresentWorkflowResultModel();

            //查询流程实例中的业务参数
            WfClientSearchParameters searchParameters = new WfClientSearchParameters()
            {
                ProcessID = criteria.ProcessID,
                ActivityID = criteria.ActivityID,
                ResourceID = criteria.ResourceID
            };
            result.ClientProcess = WfClientProxy.GetClientProcess(searchParameters);
            result.PresentDetial = new PresentDetialModel() {
                Present = GenericDiscountAdapter<PresentViewModel, PresentCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", criteria.ResourceID)).FirstOrDefault(),
                PresentItemCollection = GenericDiscountAdapter<PresentItemViewModel, PresentItemCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", criteria.ResourceID)),
                PresentPermissionCollection = GenericDiscountAdapter<PresentPermissionViewModel, PresentPermissionCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", criteria.ResourceID)).AddCampusName().ConvertEndDate(criteria.ResourceID),
                PresentPermissionsApplieCollection = GenericDiscountAdapter<PresentPermissionsApplyViewModel, PresentPermissionsApplieCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", criteria.ResourceID)).AddCampusName()
            };

            return result;
        }
        #endregion

        #region api/present/checkCreatePresent
        [PPTSJobFunctionAuthorize("PPTS:新增/编辑/删除/停用/打印买赠表-本分公司")]
        [HttpPost]
        public CheckResultModel CheckCreatePresent(CreatePresentModel model)
        {
            return model.CheckCreatePresent();
        }
        #endregion

        #region api/present/getPresentDetial
        [PPTSJobFunctionAuthorize("PPTS:买赠表管理-本分公司,买赠表管理-全国")]
        [HttpGet]
        public PresentDetialModel GetPresentDetial(string PresentID)
        {
            Check(PresentID);
            return new PresentDetialModel()
            {
                Present = GenericDiscountAdapter<PresentViewModel, PresentCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", PresentID)).FirstOrDefault(),
                PresentItemCollection = GenericDiscountAdapter<PresentItemViewModel, PresentItemCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", PresentID)),
                PresentPermissionCollection = GenericDiscountAdapter<PresentPermissionViewModel, PresentPermissionCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", PresentID)).AddCampusName().ConvertEndDate(PresentID),
                PresentPermissionsApplieCollection = GenericDiscountAdapter<PresentPermissionsApplyViewModel, PresentPermissionsApplieCollectionViewModel>.Instance.Load(builder => builder.AppendItem("PresentID", PresentID)).AddCampusName()
            };
        }
        #endregion

        #region api/present/deletePresent
        [PPTSJobFunctionAuthorize("PPTS:新增/编辑/删除/停用/打印买赠表-本分公司")]
        [HttpPost]
        public void DeletePresent(dynamic data)
        {
            string PresentID = data.presentID;
            Check(PresentID);
            DeletePresentExecutor executor = new DeletePresentExecutor(PresentID);
            executor.Execute();
        }
        #endregion

        #region api/present/disablePresent
        [PPTSJobFunctionAuthorize("PPTS:新增/编辑/删除/停用/打印买赠表-本分公司")]
        [HttpPost]
        public void DisablePresent(dynamic data)
        {
            string PresentID = data.presentID;
            Check(PresentID);
            DisablePresentExecutor executor = new DisablePresentExecutor(PresentID);
            executor.Execute();
        }
        #endregion

        #region api/Present/exportAllPresents
        [PPTSJobFunctionAuthorize("PPTS:买赠表管理-本分公司,买赠表管理-全国")]
        [HttpPost]
        public HttpResponseMessage ExportAllPresents([ModelBinder(typeof(FormBinder))]PresentsQueryCriteriaModel criteria)
        {
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            criteria.CampusIDs = DeluxeIdentity.CurrentUser.PermisstionFilter(criteria.CampusIDs);
            PagedQueryResult<Present, PresentCollection> pageData = PresentDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);
            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];
            TableDescription tableDesp = new TableDescription("买赠表");
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("买赠表编码", typeof(string))) { PropertyName = "PresentCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("分公司", typeof(string))) { PropertyName = "BranchName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("启用时间", typeof(string))) { PropertyName = "StartDate" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("状态", typeof(string))) { PropertyName = "PresentStatus" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("提交人", typeof(string))) { PropertyName = "SubmitterName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("终审人", typeof(string))) { PropertyName = "ApproverName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("审批通过时间", typeof(string))) { PropertyName = "ApproveTime", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("创建日期", typeof(string))) { PropertyName = "CreateTime", Format = "yyyy-MM-dd" });
            Dictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Present));

            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {
                switch (param.ColumnDescription.PropertyName)
                {
                    case "StartDate":
                    case "ApproveTime":
                    case "CreateTime":
                        cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
                        break;
                    case "PresentStatus":
                        cell.Value = Dictionaries["C_CODE_ABBR_BO_Infra_PresentStatus"].Where(c => c.Key == param.PropertyValue.GetHashCode().ToString()).FirstOrDefault().Value;
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("买赠表.xlsx");
        }
        #endregion
    }
}