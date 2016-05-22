using MCS.Library.Core;
using MCS.Library.Services;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using MCS.Services.Test.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MCS.Library.SOA.SysTasks.Test
{
    [TestClass]
    public class CallServiceTest
    {
        [TestMethod]
        public void CallUpdateUserTest()
        {
            UserData user = PrepareUserData();

            UserData userReturned = TaskDemoServiceProxy.Instance.UpdateUser(user);

            Console.WriteLine(userReturned.CreateTime);
        }

        [TestMethod]
        public void InvokeUpdateUserTaskTest()
        {
            SysAccomplishedTaskAdapter.Instance.ClearAll();
            SysTaskAdapter.Instance.ClearAll();

            UserData user = PrepareUserData();

            InvokeServiceTask task = PrepareInvokeServiceTask(user);

            UserData userReturned = ExecuteServiceTask(task);

            Assert.IsNotNull(userReturned);

            Assert.AreEqual(user.UserID, userReturned.UserID);
            Assert.AreEqual(user.UserName, userReturned.UserName);
            Assert.AreNotEqual(DateTime.MinValue, userReturned.CreateTime);
        }

        [TestMethod]
        public void SendInvokeUpdateUserTaskTest()
        {
            SysAccomplishedTaskAdapter.Instance.ClearAll();
            SysTaskAdapter.Instance.ClearAll();

            ServiceContainer container = new ServiceContainer().StarService();

            try
            {
                UserData user = PrepareUserData();

                InvokeServiceTask sysTask = PrepareInvokeServiceTask(user);

                InvokeServiceTaskAdapter.Instance.Push(sysTask);

                CheckTaskExecuted(sysTask.TaskID, TimeSpan.FromSeconds(10));
            }
            finally
            {
                container.StopService();
            }
        }

        [TestMethod]
        public void UpdateFixedTimeTaskTest()
        {
            UserData user = PrepareUserData();
            InvokeServiceFixedTimeTask task = PrepareFixedTimeTask(user, DateTime.Now);

            FixedTimeTaskAdapter.Instance.Update(task);

            FixedTimeTask loaded = FixedTimeTaskAdapter.Instance.Load(task.TaskID);

            Assert.IsNotNull(loaded);

            UserData userReturned = ExecuteServiceTask(loaded.ToSysTask());

            Assert.AreEqual(user.UserID, userReturned.UserID);
            Assert.AreEqual(user.UserName, userReturned.UserName);
            Assert.AreNotEqual(DateTime.MinValue, userReturned.CreateTime);
        }

        [TestMethod]
        public void SendFixedTimeTaskTest()
        {
            SysAccomplishedTaskAdapter.Instance.ClearAll();
            SysTaskAdapter.Instance.ClearAll();
            FixedTimeTaskAdapter.Instance.ClearAll();

            ServiceContainer container = new ServiceContainer().StarService();

            try
            {
                UserData user = PrepareUserData();
                InvokeServiceFixedTimeTask task = PrepareFixedTimeTask(user, DateTime.Now);

                FixedTimeTaskAdapter.Instance.Update(task);

                CheckTaskExecuted(task.TaskID, TimeSpan.FromSeconds(10));
            }
            finally
            {
                container.StopService();
            }
        }

        [TestMethod]
        public void FetchFixedTimeTaskTest()
        {
            FixedTimeTaskAdapter.Instance.ClearAll();

            UserData user = PrepareUserData();
            InvokeServiceFixedTimeTask task = PrepareFixedTimeTask(user, DateTime.Now);

            FixedTimeTaskAdapter.Instance.Update(task);

            int matchedCount = 0;
            FixedTimeTaskCollection loaded =
                FixedTimeTaskAdapter.Instance.FetchInTimeScopeTasks(TimeSpan.FromSeconds(30), 10, (t) => matchedCount++);

            Assert.IsTrue(matchedCount > 0);
            Assert.AreEqual(matchedCount, loaded.Count);
        }

        [TestMethod]
        public void ClearFixedTimeTaskTest()
        {
            SysAccomplishedTaskAdapter.Instance.ClearAll();
            SysTaskAdapter.Instance.ClearAll();
            FixedTimeTaskAdapter.Instance.ClearAll();

            ServiceContainer container = new ServiceContainer().StarService();

            try
            {
                UserData user = PrepareUserData();
                InvokeServiceFixedTimeTask task = PrepareFixedTimeTask(user, DateTime.Now.AddDays(-1));

                FixedTimeTaskAdapter.Instance.Update(task);

                CheckFixedTaskCleared(task.TaskID, TimeSpan.FromSeconds(10));
            }
            finally
            {
                container.StopService();
            }
        }

        private static UserData ExecuteServiceTask(SysTask task)
        {
            InvokeServiceTaskExecutor executor = new InvokeServiceTaskExecutor();

            executor.Execute(task);

            return WfServiceInvoker.InvokeContext["ReturnValue"] as UserData;
        }

        private static InvokeServiceTask PrepareInvokeServiceTask(UserData user)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "新任务",
                ResourceID = UuidHelper.NewUuidString()
            };

            task.SvcOperationDefs.Add(PrepareServiceDefine(user));

            task.FillData();

            return task;
        }

        private static InvokeServiceFixedTimeTask PrepareFixedTimeTask(UserData user, DateTime startTime)
        {
            string url = TaskDemoServiceProxy.Instance.GetEndpoint().Uri.ToString();

            InvokeServiceFixedTimeTask task = new InvokeServiceFixedTimeTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "固定时间任务",
                ResourceID = UuidHelper.NewUuidString(),
                StartTime = startTime
            };

            task.SvcOperationDefs.Add(PrepareServiceDefine(user));

            task.FillData();

            return task;
        }

        private static WfServiceOperationDefinition PrepareServiceDefine(UserData user)
        {
            string url = TaskDemoServiceProxy.Instance.GetEndpoint().Uri.ToString();

            return new WfServiceOperationDefinition("UpdateUser", "ReturnValue").SetAddress(WfServiceRequestMethod.Post, url, WfServiceContentType.Json).AddParameter("data", user);
        }

        private static UserData PrepareUserData()
        {
            UserData user = new UserData();

            user.UserID = UuidHelper.NewUuidString();
            user.UserName = "沈峥";

            return user;
        }

        private static SysAccomplishedTask CheckTaskExecuted(string taskID, TimeSpan timeout)
        {
            SysAccomplishedTask result = null;
            Stopwatch sw = new Stopwatch();

            sw.Start();

            while (sw.Elapsed < timeout)
            {
                result = SysAccomplishedTaskAdapter.Instance.Load(taskID);

                if (result != null)
                    break;

                Thread.Sleep(400);
            }

            (result != null).FalseThrow("任务{0}在超时时间内没有被执行", taskID);
            (result.Status == SysTaskStatus.Completed).FalseThrow("任务{0}执行错误: {1}", taskID, result.StatusText);

            return result;
        }

        private static void CheckFixedTaskCleared(string taskID, TimeSpan timeout)
        {
            FixedTimeTask fixedTimeTask = null;

            Stopwatch sw = new Stopwatch();

            sw.Start();

            while (sw.Elapsed < timeout)
            {
                fixedTimeTask = FixedTimeTaskAdapter.Instance.Load(taskID);

                if (fixedTimeTask == null)
                    break;

                Thread.Sleep(400);
            }

            (fixedTimeTask == null).FalseThrow("任务{0}在超时时间内没有被清理", taskID);
        }
    }
}
