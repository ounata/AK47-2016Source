using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class ClassServiceTest
    {
        [TestMethod]
        public void SyncClassCountToProductTest()
        {
            PPTSClassServiceProxy.Instance.SyncClassCountToProduct("1115284");
        }

        [TestMethod]
        public void ConfirmClassLessonTest() {
            PPTSClassServiceProxy.Instance.ConfirmClassLesson(DateTime.Now);
        }

        [TestMethod]
        public void Job_ConfirmClassLessonTest()
        {
            PPTSClassServiceProxy.Instance.Job_ConfirmClassLesson();
        }

        [TestMethod]
        public void Job_InitClassCountToProductTest()
        {
            PPTSClassServiceProxy.Instance.Job_InitClassCountToProduct();
        }
    }
}
