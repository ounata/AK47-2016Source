using MCS.Library.Caching;
using MCS.Library.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCS.Library.Test.Configuration
{
    [TestClass]
    public class CacheNotifySettingsTest
    {
        [TestMethod]
        public void ReadGroupQueueNames()
        {
            IEnumerable<string> queueNames = CacheNotifySettings.GetConfig().GetQueueNamesInGroup("group1");

            queueNames.ForEach(s => Console.WriteLine(s));

            Assert.AreEqual(2, queueNames.Count());
        }

        [TestMethod]
        public void CacheNotifyDataFromSettingsTest()
        {
            CacheNotifyData[] data = CacheNotifyData.FromSettings("group1", CacheNotifyType.Invalid);

            data.ForEach(d => Console.WriteLine(d.CacheQueueTypeDesp));

            Assert.AreEqual(2, data.Length);
        }
    }
}
