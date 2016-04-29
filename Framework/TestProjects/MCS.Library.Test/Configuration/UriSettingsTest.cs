using MCS.Library.Configuration;
using MCS.Library.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCS.Library.Test.Configuration
{
    [TestClass]
    public class UriSettingsTest
    {
        [TestMethod]
        public void ReadExistedUrl()
        {
            UriSettings settings = UriSettings.GetConfig();

            Uri uri = settings.CheckAndGet("group1", "helpPage");

            Assert.AreEqual("http://www.google.com/", uri.ToString());
        }

        [TestMethod]
        public void ReadGroupUrls()
        {
            UriSettings settings = UriSettings.GetConfig();

            Dictionary<string, UriConfigurationElement> urls = settings.GetUrlsInGroup("group1");

            urls.ForEach(kp => Console.WriteLine("{0}: {1}", kp.Key, kp.Value.Uri));
        }

        [TestMethod]
        public void ReadNotExistedUrl()
        {
            UriSettings settings = UriSettings.GetConfig();

            Uri uri = settings.GetUrl("group2", "helpPage2");

            Assert.IsNull(uri);
        }
    }
}
