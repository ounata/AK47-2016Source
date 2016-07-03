using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;

namespace MCS.Library.Test
{
    public class PropertyTestObject
    {
        private IUser _User = null;

        public string PublicField = "公有字段";
        private string PrivateField = "私有字段";

        public IUser User
        {
            get { return this._User; }
            set { this._User = value; }
        }

        public string ID
        {
            get;
            set;
        }

        private int PrivateInt
        {
            get;
            set;
        }

        public Decimal DecimalValue
        {
            get;
            set;
        }

        public bool BooleanValue
        {
            get;
            set;
        }

        public DateTime DateValue
        {
            get;
            set;
        }

        public TimeSpan TimeSpanValue
        {
            get;
            set;
        }

        public BooleanState BooleanStateValue
        {
            get;
            set;
        }

        public int Add(int a, int b)
        {
            return a + b;
        }

        public int GetPrivateInt()
        {
            return this.PrivateInt;
        }

        public string GetPrivateField()
        {
            return this.PrivateField;
        }

        public static PropertyTestObject PrepareTestData()
        {
            PropertyTestObject data = new PropertyTestObject();

            data.ID = UuidHelper.NewUuidString();
            data.User = TestUser.PrepareTestData();
            data.PrivateInt = 1024;
            data.BooleanValue = true;
            data.DecimalValue = 18.25M;
            data.DateValue = DateTime.Now;
            data.TimeSpanValue = TimeSpan.FromSeconds(1800);
            data.BooleanStateValue = BooleanState.Unknown;

            return data;
        }
    }

    [ObjectCompare("ID")]
    public class TestUser : IUser
    {
        #region IUser Members

        [PropertyCompare("用户ID")]
        public string ID
        {
            get;
            set;
        }

        [PropertyCompare("用户名称")]
        public string Name
        {
            get;
            set;
        }

        #endregion

        public static TestUser PrepareTestData()
        {
            TestUser user = new TestUser();

            user.ID = UuidHelper.NewUuidString();
            user.Name = "Test User";

            return user;
        }
    }

    /// <summary>
    /// 空Property和Field的对象
    /// </summary>
    public class EmptyFieldsData
    {
    }
}
