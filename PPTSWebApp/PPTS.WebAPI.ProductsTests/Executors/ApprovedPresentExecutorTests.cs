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
    public class ApprovedPresentExecutorTests
    {
        [TestMethod()]
        public void ApprovedPresentExecutorTest()
        {
            MCS.Library.Principal.DeluxeIdentity di = new MCS.Library.Principal.DeluxeIdentity(OGUExtensions.GetUserByOAName("zhangxiaoyan_2"));
            DeluxePrincipal dp = new DeluxePrincipal(di);
            Thread.CurrentPrincipal = dp;
            ApprovedPresentExecutor executor = new ApprovedPresentExecutor("fb89d932-c4dc-b2e0-4eec-3752d47945ec");
            executor.Execute();
        }
    }
}