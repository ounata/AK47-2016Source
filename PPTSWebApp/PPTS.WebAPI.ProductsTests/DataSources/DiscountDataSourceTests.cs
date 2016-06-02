using MCS.Library.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.Data.Common.Security;
using PPTS.Data.Products;
using PPTS.WebAPI.Products.DataSources;
using PPTS.WebAPI.Products.ViewModels.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Products.DataSources.Tests
{
    [TestClass()]
    public class DiscountDataSourceTests
    {
        [TestMethod()]
        public void LoadTest()
        {

            DiscountQueryCriteriaModel model = new DiscountQueryCriteriaModel() { PageParams = new MCS.Library.Data.PageRequestParams() { PageIndex = 1, PageSize = 10, TotalCount = -1 }, OrderBy = new MCS.Library.Data.OrderByRequestItem[] { new MCS.Library.Data.OrderByRequestItem() { DataField = "DiscountID", SortDirection = MCS.Library.Data.FieldSortDirection.Descending } } };
            //model.DiscountStatus = DiscountStatusDefine.Approved;
            model.StartDate = new DateTime(2016, 4, 1);
            model.EndDate = new DateTime(2016, 7, 1);

            model.CampusIDs = new string[] { "18" };
            model.CampusStatus = CampusUseInfoDefine.DQ;
            var result = DiscountDataSource.Instance.Load(model.PageParams, model, model.OrderBy);
        }

        [TestMethod()]
        public void LoadTest2()
        {
            MCS.Library.Principal.DeluxeIdentity di = new MCS.Library.Principal.DeluxeIdentity(OGUExtensions.GetUserByOAName("zhangxiaoyan_2"));
            DeluxePrincipal dp = new DeluxePrincipal(di);
            Thread.CurrentPrincipal = dp;
            Console.WriteLine(di.Name);
            Console.WriteLine(DeluxeIdentity.CurrentUser.Name);
            Console.WriteLine(DeluxeIdentity.CurrentUser.GetCurrentJob());

        }
    }
}