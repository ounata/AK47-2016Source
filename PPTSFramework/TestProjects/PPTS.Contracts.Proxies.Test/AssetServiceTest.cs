using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Contracts.Orders.Models;

namespace PPTS.Contracts.Proxies.Test
{
    /// <summary>
    /// AssetServiceTest 的摘要说明
    /// </summary>
    [TestClass]
    public class AssetServiceTest
    {

        [TestMethod]
        public void QueryAssetStatisticByAccountIDTest()
        {
            AssetStatisticQueryResult queryResult = PPTSAssetQueryServiceProxy.Instance.QueryAssetStatisticByAccountID("145291");
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.AssetsValue);
            Console.WriteLine(queryResult.AssetsValue);
        }
    }
}
