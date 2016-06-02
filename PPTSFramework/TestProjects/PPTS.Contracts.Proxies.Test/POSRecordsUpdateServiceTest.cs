using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Contracts.Proxies;
using System.Diagnostics;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class POSRecordsUpdateServiceTest
    {
        [TestMethod]
        public void POSRecordsUpdateTest()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            long useTime = 0;
            PPTSPOSRecordsUpdateServiceProxy.Instanse.UpdatePOSRecords("UnProcessFilePath");
            watch.Stop();
            useTime = watch.ElapsedMilliseconds;

            Console.WriteLine(useTime);
        }
    }
}
