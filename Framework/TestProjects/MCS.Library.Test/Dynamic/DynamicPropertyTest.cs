using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Core;

namespace MCS.Library.Test
{
    using Data.DataObjects;
    using System.Linq.Expressions;
    using System.Reflection;

    [TestClass]
    public class DynamicPropertyTest
    {
        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void SimplePropertyGetTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            string originalID = data.ID;

            Func<object, string> reader = (Func<object, string>)DynamicHelper.GetPropertyGetterDelegate(typeof(PropertyTestObject).GetProperty("ID"));

            string idRead = reader(data);

            Assert.AreEqual(originalID, idRead);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void InterfacePropertyGetTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            IUser originalUser = data.User;

            Func<object, IUser> reader = (Func<object, IUser>)DynamicHelper.GetPropertyGetterDelegate(typeof(PropertyTestObject).GetProperty("User"));

            IUser userRead = reader(data);

            Assert.AreSame(originalUser, userRead);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void InterfacePropertySetNullTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            var writer = (Func<object, IUser, IUser>)DynamicHelper.GetPropertySetterDelegate(typeof(PropertyTestObject).GetProperty("User"));

            writer(data, null);

            Assert.IsNull(data.User);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void InterfacePropertySetUserTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            var writer = (Func<object, IUser, IUser>)DynamicHelper.GetPropertySetterDelegate(typeof(PropertyTestObject).GetProperty("User"));

            IUser user = TestUser.PrepareTestData();

            writer(data, user);

            Assert.AreSame(data.User, user);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PrivatePropertyGetTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            Func<object, int> reader = (Func<object, int>)DynamicHelper.GetPropertyGetterDelegate(typeof(PropertyTestObject).GetProperty("PrivateInt", BindingFlags.Instance | BindingFlags.NonPublic));

            int dataRead = (int)reader(data);

            Assert.AreEqual(data.GetPrivateInt(), dataRead);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PrivatePropertySetTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            var writer = (Func<object, int, int>)DynamicHelper.GetPropertySetterDelegate(typeof(PropertyTestObject).GetProperty("PrivateInt", BindingFlags.Instance | BindingFlags.NonPublic));

            writer(data, 2048);

            Assert.AreEqual(2048, data.GetPrivateInt());
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PropertyGetMethodDelegateCacheTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            Func<object, object> reader1 = (Func<object, object>)DynamicHelper.GetPropertyGetterDelegate(typeof(PropertyTestObject).GetProperty("PrivateInt", BindingFlags.Instance | BindingFlags.NonPublic), typeof(object));
            Func<object, object> reader2 = (Func<object, object>)DynamicHelper.GetPropertyGetterDelegate(typeof(PropertyTestObject).GetProperty("PrivateInt", BindingFlags.Instance | BindingFlags.NonPublic), typeof(object));

            Assert.AreSame(reader1, reader2);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PropertySetMethodDelegateCacheTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            var writer1 = (Func<object, int, int>)DynamicHelper.GetPropertySetterDelegate(typeof(PropertyTestObject).GetProperty("PrivateInt", BindingFlags.Instance | BindingFlags.NonPublic));
            var writer2 = (Func<object, int, int>)DynamicHelper.GetPropertySetterDelegate(typeof(PropertyTestObject).GetProperty("PrivateInt", BindingFlags.Instance | BindingFlags.NonPublic));

            Assert.AreSame(writer1, writer2);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PropertyGetValueTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            PropertyInfo piInt = typeof(PropertyTestObject).GetProperty("PrivateInt", BindingFlags.Instance | BindingFlags.NonPublic);

            object intValue = piInt.GetPropertyValue(data);

            Assert.AreEqual(data.GetPrivateInt(), intValue);

            PropertyInfo piString = typeof(PropertyTestObject).GetProperty("ID");

            object stringValue = piString.GetPropertyValue(data);

            Assert.AreEqual(data.ID, stringValue);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PropertyGetValueTypeTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            Assert.AreEqual(data.BooleanValue, typeof(PropertyTestObject).GetProperty("BooleanValue").GetPropertyValue(data));

            Assert.AreEqual(data.DecimalValue, typeof(PropertyTestObject).GetProperty("DecimalValue").GetPropertyValue(data));

            Assert.AreEqual(data.DateValue, typeof(PropertyTestObject).GetProperty("DateValue").GetPropertyValue(data));
            Assert.AreEqual(data.TimeSpanValue, typeof(PropertyTestObject).GetProperty("TimeSpanValue").GetPropertyValue(data));
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PropertyGetEnumValueTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            Assert.AreEqual(data.BooleanStateValue, typeof(PropertyTestObject).GetProperty("BooleanStateValue").GetPropertyValue(data));
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PropertySetValueTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            PropertyInfo piInt = typeof(PropertyTestObject).GetProperty("PrivateInt", BindingFlags.Instance | BindingFlags.NonPublic);

            piInt.SetPropertyValue(data, 2048);

            Assert.AreEqual(data.GetPrivateInt(), 2048);

            PropertyInfo piString = typeof(PropertyTestObject).GetProperty("ID");

            piString.SetPropertyValue(data, "沈峥");

            Assert.AreEqual("沈峥", data.ID);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PropertySetValueTypeTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            typeof(PropertyTestObject).GetProperty("BooleanValue").SetPropertyValue(data, true);
            Assert.AreEqual(true, data.BooleanValue);

            typeof(PropertyTestObject).GetProperty("DecimalValue").SetPropertyValue(data, 42.6M);
            Assert.AreEqual(42.6M, data.DecimalValue);

            DateTime now = DateTime.Now;
            typeof(PropertyTestObject).GetProperty("DateValue").SetPropertyValue(data, now);
            Assert.AreEqual(now, data.DateValue);

            typeof(PropertyTestObject).GetProperty("TimeSpanValue").SetPropertyValue(data, TimeSpan.FromHours(5));
            Assert.AreEqual(TimeSpan.FromHours(5), data.TimeSpanValue);
        }

        [TestMethod]
        [TestCategory("Dynamic Invoke")]
        public void PropertySetEnumValueTest()
        {
            PropertyTestObject data = PropertyTestObject.PrepareTestData();

            data.BooleanStateValue = BooleanState.False;

            typeof(PropertyTestObject).GetProperty("BooleanStateValue").SetPropertyValue(data, BooleanState.Unknown);

            Assert.AreEqual(BooleanState.Unknown, data.BooleanStateValue);
        }
    }
}
