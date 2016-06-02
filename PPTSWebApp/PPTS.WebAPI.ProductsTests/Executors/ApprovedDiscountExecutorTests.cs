using MCS.Library.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.WebAPI.Products.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Products.Executors.Tests
{
    [TestClass()]
    public class ApprovedDiscountExecutorTests
    {
        [TestMethod()]
        public void ApprovedDiscountExecutorTest()
        {
            MCS.Library.Principal.DeluxeIdentity di = new MCS.Library.Principal.DeluxeIdentity(OGUExtensions.GetUserByOAName("zhangxiaoyan_2"));
            DeluxePrincipal dp = new DeluxePrincipal(di);
            Thread.CurrentPrincipal = dp;
            ApprovedDiscountExecutor executor = new ApprovedDiscountExecutor("6bd37843-617d-a46d-44c6-2e78da44e807");
            executor.Execute();
        }
    }
}