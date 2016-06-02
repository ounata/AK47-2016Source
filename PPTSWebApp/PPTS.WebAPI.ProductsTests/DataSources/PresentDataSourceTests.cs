using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Products;
using PPTS.WebAPI.Products.DataSources;
using PPTS.WebAPI.Products.ViewModels.Presents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Products.DataSources.Tests
{
    [TestClass()]
    public class PresentDataSourceTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            PresentsQueryCriteriaModel model = new PresentsQueryCriteriaModel() { PageParams = new MCS.Library.Data.PageRequestParams() { PageIndex = 1, PageSize = 10, TotalCount = -1 }, OrderBy = new MCS.Library.Data.OrderByRequestItem[] { new MCS.Library.Data.OrderByRequestItem() { DataField = "PresentID", SortDirection = MCS.Library.Data.FieldSortDirection.Descending } } };
            model.PresentStatus = 2;
            model.StartDate = new DateTime(2016, 4, 1);
            model.EndDate = new DateTime(2016, 7, 1);

            model.CampusIDs = new string[] { "18" };
            model.CampusStatus = CampusUseInfoDefine.DQ;
            var result = PresentDataSource.Instance.Load(model.PageParams, model, model.OrderBy);
        }
    }
}