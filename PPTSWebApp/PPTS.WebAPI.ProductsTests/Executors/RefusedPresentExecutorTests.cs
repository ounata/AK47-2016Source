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
    public class RefusedPresentExecutorTests
    {
        [TestMethod()]
        public void RefusedPresentExecutorTest()
        {
            MCS.Library.Principal.DeluxeIdentity di = new MCS.Library.Principal.DeluxeIdentity(OGUExtensions.GetUserByOAName("zhangxiaoyan_2"));
            DeluxePrincipal dp = new DeluxePrincipal(di);
            Thread.CurrentPrincipal = dp;
            RefusedPresentExecutor executor = new RefusedPresentExecutor("a2923d2c-e6f3-84f8-485e-9cb56c0599ab");
            executor.Execute();
        }
    }
}