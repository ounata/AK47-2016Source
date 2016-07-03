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
            ApprovedPresentExecutor executor = new ApprovedPresentExecutor("6e700189-f39e-a74c-472b-bbac7a48a5d0");
            executor.Execute();
        }
    }
}