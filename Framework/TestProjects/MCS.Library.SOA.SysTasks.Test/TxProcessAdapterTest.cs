using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.SOA.DataObjects.AsyncTransactional;

namespace MCS.Library.SOA.SysTasks.Test
{
    [TestClass]
    public class TxProcessAdapterTest
    {
        [TestMethod]
        public void UpdateTxProcessTest()
        {
            TxProcess process = DataHelper.PrepareProcess();

            TxProcessAdapter.DefaultInstance.Update(process);

            TxProcess loaded = TxProcessAdapter.DefaultInstance.Load(process.ProcessID);

            Assert.IsNotNull(loaded);
            Assert.AreEqual(process.ProcessID, loaded.ProcessID);
            Assert.AreEqual(process.ResourceID, loaded.ResourceID);
            Assert.AreEqual(process.Activities.Count, loaded.Activities.Count);
        }

        [TestMethod]
        public void CopyProcessTest()
        {
            TxProcess process = DataHelper.PrepareProcess();

            TxProcessAdapter bizAdapter = TxProcessAdapter.GetInstance(DataHelper.BizConnectionName);

            bizAdapter.Update(process);

            TxProcess loaded = bizAdapter.Load(process.ProcessID);

            bizAdapter.CopyTo(loaded, TxProcessAdapter.DefaultInstance.ConnectionName);

            TxProcess loadedInDefault = TxProcessAdapter.DefaultInstance.Load(process.ProcessID);

            Assert.IsNotNull(loadedInDefault);
            Assert.AreEqual(loaded.ProcessID, loadedInDefault.ProcessID);
            Assert.AreEqual(loaded.Activities.Count, loadedInDefault.Activities.Count);
            Assert.AreEqual(loaded.CreateTime, loadedInDefault.CreateTime);
        }

        [TestMethod]
        public void MoveToTest()
        {
            TxProcess process = DataHelper.PrepareProcess();

            Assert.AreEqual(TxProcessStatus.NotRunning, process.Status);
            Assert.IsNotNull(process.NextActivity);

            process.MoveToNextActivity();
            Assert.AreEqual(0, process.CurrentActivityIndex);
            Assert.AreEqual(TxProcessStatus.Running, process.Status);
            Assert.AreEqual(TxActivityStatus.Running, process.CurrentActivity.Status);
            Assert.IsNull(process.PreviousActivity);
            Assert.IsNotNull(process.NextActivity);

            process.MoveToNextActivity();
            Assert.AreEqual(TxActivityStatus.Completed, process.PreviousActivity.Status);
            Assert.AreEqual(1, process.CurrentActivityIndex);
            Assert.AreEqual(TxProcessStatus.Running, process.Status);
            Assert.AreEqual(TxActivityStatus.Running, process.CurrentActivity.Status);
            Assert.IsNull(process.NextActivity);

            process.MoveToNextActivity();
            Assert.AreEqual(TxActivityStatus.Completed, process.PreviousActivity.Status);
            Assert.IsNull(process.CurrentActivity);
            Assert.AreEqual(TxProcessStatus.Completed, process.Status);
            Assert.IsNull(process.NextActivity);
        }

        [TestMethod]
        public void RollbackTest()
        {
            TxProcess process = DataHelper.PrepareProcess();

            process.MoveToNextActivity();
            process.MoveToNextActivity();
            process.MoveToNextActivity();

            Assert.AreEqual(TxProcessStatus.Completed, process.Status);

            process.RollbackToPreviousActivity();
            Assert.AreEqual(1, process.CurrentActivityIndex);
            Assert.AreEqual(TxProcessStatus.RollingBack, process.Status);
            Assert.AreEqual(TxActivityStatus.RollingBack, process.CurrentActivity.Status);
            Assert.IsNotNull(process.PreviousActivity);
            Assert.IsNull(process.NextActivity);

            process.RollbackToPreviousActivity();
            Assert.AreEqual(0, process.CurrentActivityIndex);
            Assert.AreEqual(TxProcessStatus.RollingBack, process.Status);
            Assert.AreEqual(TxActivityStatus.RollingBack, process.CurrentActivity.Status);
            Assert.IsNull(process.PreviousActivity);
            Assert.IsNotNull(process.NextActivity);

            process.RollbackToPreviousActivity();
            Assert.AreEqual(-1, process.CurrentActivityIndex);
            Assert.AreEqual(TxProcessStatus.RolledBack, process.Status);
            Assert.IsNull(process.CurrentActivity);
            Assert.IsNull(process.PreviousActivity);
            Assert.IsNotNull(process.NextActivity);
        }
    }
}
