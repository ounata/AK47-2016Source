using MCS.Library.Core;
using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class PhoneAdapterTest
    {
        [TestMethod]
        public void UpdatePhonesTest()
        {
            string ownerID = UuidHelper.NewUuidString();

            PhoneCollection phones = DataHelper.PreparePhonesData(ownerID);

            using (DbContext context = PhoneAdapter.Instance.GetDbContext())
            {
                PhoneAdapter.Instance.UpdateByOwnerIDInContext(ownerID, phones);

                context.ExecuteTimePointSqlInContext();
            }

            PhoneCollection loaded = PhoneAdapter.Instance.LoadByOwnerID(ownerID);

            phones.AreEqual(loaded);
        }

        [TestMethod]
        public void HomePhoneTypeTest()
        {
            PhoneTypeDefine phoneType = "021-68767788".ToPhoneType();

            Assert.AreEqual(PhoneTypeDefine.HomePhone, phoneType);
        }

        [TestMethod]
        public void HomePhoneWithExtTypeTest()
        {
            PhoneTypeDefine phoneType = "021-68767788-5461".ToPhoneType();

            Assert.AreEqual(PhoneTypeDefine.HomePhone, phoneType);
        }

        [TestMethod]
        public void MobilePhoneTypeTest()
        {
            PhoneTypeDefine phoneType = "13501126279".ToPhoneType();

            Assert.AreEqual(PhoneTypeDefine.MobilePhone, phoneType);
        }

        [TestMethod]
        public void UnknownPhoneTypeTest()
        {
            PhoneTypeDefine phoneType = "".ToPhoneType();

            Assert.AreEqual(PhoneTypeDefine.Unknown, phoneType);
        }

        [TestMethod]
        public void TowPhoneNumbersTest()
        {
            FakePhoneNumbers numbers = new FakePhoneNumbers();

            numbers.PrimaryPhone = "021-68767788-5461".ToPhone("abc", true);
            numbers.SecondaryPhone = "13501126279".ToPhone("abc", false);

            PhoneCollection phones = numbers.ToPhones(UuidHelper.NewUuidString());

            Assert.AreEqual(2, phones.Count);
            Assert.AreEqual(PhoneTypeDefine.HomePhone, phones[0].PhoneType);
            Assert.IsTrue(phones[0].IsPrimary);
            Assert.AreEqual(PhoneTypeDefine.MobilePhone, phones[1].PhoneType);
            Assert.IsFalse(phones[1].IsPrimary);
        }

        [TestMethod]
        public void PrimaryPhoneNumberTest()
        {
            FakePhoneNumbers numbers = new FakePhoneNumbers();

            numbers.PrimaryPhone = "021-68767788-5461".ToPhone("abc", true);

            PhoneCollection phones = numbers.ToPhones(UuidHelper.NewUuidString());

            Assert.AreEqual(1, phones.Count);
            Assert.AreEqual(PhoneTypeDefine.HomePhone, phones[0].PhoneType);
            Assert.IsTrue(phones[0].IsPrimary);
        }

        [TestMethod]
        public void SecondaryPhoneNumberTest()
        {
            FakePhoneNumbers numbers = new FakePhoneNumbers();

            numbers.SecondaryPhone = "13501126279".ToPhone("abc", false);

            PhoneCollection phones = numbers.ToPhones(UuidHelper.NewUuidString());

            Assert.AreEqual(1, phones.Count);
            Assert.AreEqual(PhoneTypeDefine.MobilePhone, phones[0].PhoneType);
            Assert.IsFalse(phones[0].IsPrimary);
        }
    }
}
