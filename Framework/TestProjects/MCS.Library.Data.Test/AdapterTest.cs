using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Test.Adapters;
using MCS.Library.Data.Test.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Transactions;

namespace MCS.Library.Data.Test
{
    [TestClass]
    public class AdapterTest
    {
        [TestMethod]
        public void UpdateUserTest()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            UserAdapter.Instance.Update(user);

            User userLoaded = UserAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(user.UserID), "UserID")).Single();

            AssertEqual(user, userLoaded);
        }

        [TestMethod]
        public void UpdateUserInContextTest()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            UserAdapter.Instance.UpdateInContext(user);

            Console.WriteLine(UserAdapter.Instance.GetSqlContext().GetSqlInContext());

            using (DbContext context = UserAdapter.Instance.GetDbContext())
            {
                context.ExecuteNonQuerySqlInContext();

                User userLoaded = UserAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(user.UserID), "UserID")).Single();

                AssertEqual(user, userLoaded);
            }
        }

        [TestMethod]
        public void DeleteUserInContextByID()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            UserAdapter.Instance.Update(user);

            UserAdapter.Instance.DeleteInContext(user);

            Console.WriteLine(UserAdapter.Instance.GetSqlContext().GetSqlInContext());

            using (DbContext context = UserAdapter.Instance.GetDbContext())
            {
                context.ExecuteNonQuerySqlInContext();

                User userLoaded = UserAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(user.UserID), "UserID")).SingleOrDefault();

                Assert.IsNull(userLoaded);
            }
        }

        [TestMethod]
        public void DeleteUserInContextByBuilder()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            UserAdapter.Instance.Update(user);

            UserAdapter.Instance.DeleteInContext(builder => builder.AppendItem("UserID", user.UserID));

            Console.WriteLine(UserAdapter.Instance.GetSqlContext().GetSqlInContext());

            using (DbContext context = UserAdapter.Instance.GetDbContext())
            {
                context.ExecuteNonQuerySqlInContext();

                User userLoaded = UserAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(user.UserID), "UserID")).SingleOrDefault();

                Assert.IsNull(userLoaded);
            }
        }

        [TestMethod]
        public void UpdateInContextAndTransactionTest()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                UserAdapter.Instance.UpdateInContext(user);

                Console.WriteLine(UserAdapter.Instance.GetSqlContext().GetSqlInContext());

                using (DbContext context = UserAdapter.Instance.GetDbContext())
                {
                    context.ExecuteNonQuerySqlInContext();

                    User userLoaded = UserAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(user.UserID), "UserID")).Single();

                    AssertEqual(user, userLoaded);
                }
            }
        }

        [TestMethod]
        public void UpdateInContextAndNestedTransactionTest()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            UserAdapter.Instance.UpdateInContext(user);

            Console.WriteLine(UserAdapter.Instance.GetSqlContext().GetSqlInContext());

            using (DbContext context = UserAdapter.Instance.GetDbContext())
            {
                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    using (DbContext nestedContext = UserAdapter.Instance.GetDbContext())
                    {
                        nestedContext.ExecuteNonQuerySqlInContext();

                        User userLoaded = UserAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(user.UserID), "UserID")).Single();

                        AssertEqual(user, userLoaded);
                    }
                }
            }
        }

        [TestMethod]
        public void LoadInContextTest()
        {
            User user1 = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };
            User user2 = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈嵘", Gender = GenderType.Male };

            UserAdapter.Instance.Update(user1);
            UserAdapter.Instance.Update(user2);

            using (DbContext context = UserAdapter.Instance.GetDbContext())
            {
                User user1Loaded = null;
                User user2Loaded = null;

                UserAdapter.Instance.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("UserID", user1.UserID)),
                    users => user1Loaded = users.SingleOrDefault(), "User1");

                UserAdapter.Instance.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("UserID", user2.UserID)),
                    users => user2Loaded = users.SingleOrDefault(), "User2");

                context.ExecuteDataSetSqlInContext();

                Assert.IsNotNull(user1Loaded);
                AssertEqual(user1, user1Loaded);

                Assert.IsNotNull(user2Loaded);
                AssertEqual(user2, user2Loaded);
            }
        }

        [TestMethod]
        public void LoadByBuilderInContextTest()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            UserAdapter.Instance.Update(user);

            TestObject testObj = new TestObject() { ID = UuidHelper.NewUuidString(), Name = "GFW", Amount = 100 };

            TestObjectAdapter.Instance.Update(testObj);

            using (DbContext context = UserAdapter.Instance.GetDbContext())
            {
                User userLoaded = null;

                UserAdapter.Instance.LoadByBuilderInContext(new ConnectiveLoadingCondition(new InSqlClauseBuilder("UserID").AppendItem(user.UserID)),
                    users => userLoaded = users.SingleOrDefault());

                TestObject testObjLoaded = null;

                TestObjectAdapter.Instance.LoadByBuilderInContext(new ConnectiveLoadingCondition(new InSqlClauseBuilder("ID").AppendItem(testObj.ID)),
                    objs => testObjLoaded = objs.SingleOrDefault());

                context.ExecuteDataSetSqlInContext();

                Assert.IsNotNull(userLoaded);
                AssertEqual(user, userLoaded);

                Assert.IsNotNull(testObjLoaded);
                Assert.AreEqual(testObj.ID, testObjLoaded.ID);
            }
        }

        private static void AssertEqual(User expected, User actual)
        {
            Assert.AreEqual(expected.UserID, actual.UserID);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.Gender, actual.Gender);
        }
    }
}
