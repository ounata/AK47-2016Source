using MCS.Library.Data;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Web.MVC.Library.ApiCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Products.Controllers;
using PPTS.WebAPI.Products.DataSources;
using PPTS.WebAPI.Products.ViewModels.Discounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Products.Controllers.Tests
{
    [TestClass()]
    public class DiscountControllerTests
    {
        [TestMethod()]
        public void GetDiscountDetialTest()
        {
            string discountID = "fac8aa10-fe22-88fa-4341-dddb5f760481";
            DiscountDetialModel model = new DiscountDetialModel()
            {
                Discount = GenericDiscountAdapter<DiscountViewModel, DiscountCollectionViewModel>.Instance.Load(builder => builder.AppendItem("DiscountID", discountID)).FirstOrDefault(),
                DiscountItemCollection = GenericDiscountAdapter<DiscountItemViewModel, DiscountItemCollectionViewModel>.Instance.Load(builder => builder.AppendItem("DiscountID", discountID)),
                DiscountPermissionCollection = GenericDiscountAdapter<DiscountPermissionViewModel, DiscountPermissionCollectionViewModel>.Instance.Load(builder => builder.AppendItem("DiscountID", discountID)).AddCampusName().ConvertEndDate(discountID),
                DiscountPermissionsApplieCollection = GenericDiscountAdapter<DiscountPermissionsApplyViewModel, DiscountPermissionsApplieCollectionViewModel>.Instance.Load(builder => builder.AppendItem("DiscountID", discountID)).AddCampusName()
            };


        }

        [TestMethod()]
        public void ExportAllDiscountsTest()
        {
            DiscountQueryCriteriaModel criteria = new DiscountQueryCriteriaModel() {
                PageParams =new PageRequestParams() {  PageIndex = 0, PageSize=0 },
                OrderBy =  new OrderByRequestItem[] {
                   new OrderByRequestItem() { DataField="DiscountID", SortDirection= FieldSortDirection.Descending }
                }                
            };            

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

            //var result = wb.ToResponseMessage("折扣表.xlsx");
        }
    }
}