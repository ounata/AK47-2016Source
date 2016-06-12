using MCS.Library.Data;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.ModelBinder;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Products.DataSources;
using PPTS.WebAPI.Products.Executors;
using PPTS.WebAPI.Products.ViewModels.Discounts;
using PPTS.WebAPI.Products.ViewModels.Presents;
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
        [HttpPost]
        public PresentsQueryResultModel getAllPresents(PresentsQueryCriteriaModel criteria) {
            return new PresentsQueryResultModel()
            {
                QueryResult = PresentDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Present))
            };
        }

        [HttpPost]
        public PagedQueryResult<Present, PresentCollection> GetPagedPresents(PresentsQueryCriteriaModel criteria)
        {
            return PresentDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/present/createPresent
        [HttpPost]
        public void CreatePresent(CreatePresentModel model)
        {
            CreatePresentExecutor executor = new CreatePresentExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/present/checkCreatePresent
        [HttpPost]
        public CheckResultModel CheckCreatePresent(CreatePresentModel model)
        {
            return model.CheckCreatePresent();
        }
        #endregion

        #region api/present/getPresentDetial
        [HttpGet]
        public PresentDetialModel GetPresentDetial(string PresentID)
        {
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
        [HttpPost]
        public void DeletePresent(dynamic data)
        {
            string PresentID = data.presentID;
            DeletePresentExecutor executor = new DeletePresentExecutor(PresentID);
            executor.Execute();
        }
        #endregion

        #region api/present/disablePresent
        [HttpPost]
        public void DisablePresent(dynamic data)
        {
            string PresentID = data.presentID;
            DisablePresentExecutor executor = new DisablePresentExecutor(PresentID);
            executor.Execute();
        }
        #endregion

        #region api/Present/exportAllPresents
        [HttpPost]
        public HttpResponseMessage ExportAllPresents([ModelBinder(typeof(FormBinder))]PresentsQueryCriteriaModel criteria)
        {
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            PagedQueryResult<Present, PresentCollection> pageData = PresentDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);
            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];
            TableDescription tableDesp = new TableDescription("买赠表");
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("买赠编码", typeof(string))) { PropertyName = "PresentCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("分公司", typeof(string))) { PropertyName = "BranchName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("启用时间", typeof(string))) { PropertyName = "StartDate" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("状态", typeof(string))) { PropertyName = "PresentStatus" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("提交人", typeof(string))) { PropertyName = "SubmitterName", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("审批日期", typeof(string))) { PropertyName = "ApproveTime", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("最终审批人", typeof(string))) { PropertyName = "ApproverName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("审批人岗位", typeof(string))) { PropertyName = "ApproverJobName" });
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