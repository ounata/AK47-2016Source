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
    public class DiscountController : ApiController
    {
        #region api/discount/getCurrentBranchName
        public DiscountDetialModel GetCurrentBranchName() {
            DiscountDetialModel result = new DiscountDetialModel() { Discount=new DiscountViewModel() { DiscountName=string.Empty } };
            var branch = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Branch);
            if (branch != null)
                result.Discount.DiscountName = PPTS.Data.Products.Helper.GetDiscountName(branch.Name);
            return result; 
        }
        #endregion

        #region api/discount/getAllDiscounts 
        [HttpPost]
        public DiscountQueryResultModel GetAllDiscounts(DiscountQueryCriteriaModel criteria)
        {
            return new DiscountQueryResultModel()
            {
                QueryResult = DiscountDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Discount))
            };
        }

        [HttpPost]
        public PagedQueryResult<Discount, DiscountCollection> GetPagedDiscounts(DiscountQueryCriteriaModel criteria)
        {
            return DiscountDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/discount/createDiscount
        [HttpPost]
        public void createDiscount(CreateDiscountModel model)
        {
            CreateDiscountExecutor executor = new CreateDiscountExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/discount/checkCreateDiscount
        [HttpPost]
        public CheckResultModel CheckCreateDiscount(CreateDiscountModel model)
        {
            return model.CheckCreateDiscount();
        }
        #endregion

        #region api/discount/getDiscountDetial
        [HttpGet]
        public DiscountDetialModel GetDiscountDetial(string DiscountID)
        {
            // string discountID = data.discountID;
            return new DiscountDetialModel()
            {
                Discount = GenericDiscountAdapter<DiscountViewModel, DiscountCollectionViewModel>.Instance.Load(builder => builder.AppendItem("DiscountID", DiscountID)).FirstOrDefault(),
                DiscountItemCollection = GenericDiscountAdapter<DiscountItemViewModel, DiscountItemCollectionViewModel>.Instance.Load(builder => builder.AppendItem("DiscountID", DiscountID)),
                DiscountPermissionCollection = GenericDiscountAdapter<DiscountPermissionViewModel, DiscountPermissionCollectionViewModel>.Instance.Load(builder => builder.AppendItem("DiscountID", DiscountID)).AddCampusName().ConvertEndDate(DiscountID),
                DiscountPermissionsApplieCollection = GenericDiscountAdapter<DiscountPermissionsApplyViewModel, DiscountPermissionsApplieCollectionViewModel>.Instance.Load(builder => builder.AppendItem("DiscountID", DiscountID)).AddCampusName()
            };
        }
        #endregion

        #region api/discount/deleteDiscount
        [HttpPost]
        public void DeleteDiscount(dynamic data)
        {
            string discountID = data.discountID;
            DeleteDiscountExecutor executor = new DeleteDiscountExecutor(discountID);
            executor.Execute();
        }
        #endregion

        #region api/discount/disableDiscount
        [HttpPost]
        public void DisableDiscount(dynamic data)
        {
            string discountID = data.discountID;
            DisableDiscountExecutor executor = new DisableDiscountExecutor(discountID);
            executor.Execute();
        }
        #endregion

        #region api/discount/exportAllDiscounts
        [HttpPost]
        public HttpResponseMessage ExportAllDiscounts([ModelBinder(typeof(FormBinder))]DiscountQueryCriteriaModel criteria) {
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            PagedQueryResult<Discount, DiscountCollection> pageData = DiscountDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);
            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];
            TableDescription tableDesp = new TableDescription("折扣表");
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("折扣编码", typeof(string))) { PropertyName = "DiscountCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("分公司", typeof(string))) { PropertyName = "BranchName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("启用时间", typeof(string))) { PropertyName = "StartDate" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("状态", typeof(string))) { PropertyName = "DiscountStatus" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("提交人", typeof(string))) { PropertyName = "SubmitterName", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("审批日期", typeof(string))) { PropertyName = "ApproveTime", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("最终审批人", typeof(string))) { PropertyName = "ApproverName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("审批人岗位", typeof(string))) { PropertyName = "ApproverJobName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("创建日期", typeof(string))) { PropertyName = "CreateTime", Format = "yyyy-MM-dd" });
            Dictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Discount));

            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {
                switch (param.ColumnDescription.PropertyName)
                {
                    case "StartDate":
                    case "ApproveTime":
                    case "CreateTime":
                        cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
                        break;
                    case "DiscountStatus":
                        cell.Value = Dictionaries["C_CODE_ABBR_BO_Infra_DiscountStatus"].Where(c => c.Key == param.PropertyValue.GetHashCode().ToString()).FirstOrDefault().Value;
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("折扣表.xlsx");
        }
        #endregion
    }
}