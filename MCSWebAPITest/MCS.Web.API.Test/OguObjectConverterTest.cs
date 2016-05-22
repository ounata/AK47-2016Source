using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using MCS.Web.MVC.Library.Configuration;
using MCS.Web.MVC.Library.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCS.Web.API.Test
{
    [TestClass]
    public class OguObjectConverterTest
    {
        [TestMethod]
        public void UserSerializationTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            string json = JsonConvert.SerializeObject(user, Formatting.Indented, new JavascriptOguObjectConverter());

            Console.WriteLine(json);
        }

        [TestMethod]
        public void UserDeserializationTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            string json = JsonConvert.SerializeObject(user, Formatting.Indented, new JavascriptOguObjectConverter());

            OguUser oguObj = JsonConvert.DeserializeObject<OguUser>(json, new JavascriptOguObjectConverter());

            AreEqual(user, oguObj);
        }

        [TestMethod]
        public void SimpleUserDeserializationTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            var simpleUser = new { id = user.ID, name = user.Name, objectType = (int)user.ObjectType };

            string json = JsonConvert.SerializeObject(simpleUser, Formatting.Indented);

            Console.WriteLine(json);

            OguUser oguObj = JsonConvert.DeserializeObject<OguUser>(json, new JavascriptOguObjectConverter());

            Assert.AreEqual(simpleUser.id, oguObj.ID);
            Assert.AreEqual(simpleUser.name, oguObj.Name);
            Assert.AreEqual(simpleUser.objectType, (int)oguObj.ObjectType);
        }

        [TestMethod]
        public void ConverterSettingsTest()
        {
            JsonDotNetConvertersSettings settings = JsonDotNetConvertersSettings.GetConfig();

            IEnumerable<JsonConverter> converters = settings.GetConverters();

            foreach (JsonConverter converter in converters)
                Console.WriteLine("Type: {0}", converter.GetType().FullName);

            Assert.AreEqual(2, converters.Count());
        }

        private static void AreEqual(IUser expected, OguUser actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.FullPath, actual.FullPath);
            Assert.AreEqual(expected.LogOnName, actual.LogOnName);
        }
    }
}
