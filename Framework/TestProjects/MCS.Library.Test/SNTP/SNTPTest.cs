using MCS.Library.Core;
using MCS.Library.Net.SNTP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCS.Library.Test.SNTP
{
    [TestClass]
    public class SNTPTest
    {
        [TestMethod]
        public void GetNowTest()
        {
            DateTime now = DateTime.Now;
            DateTime sntpNow = SNTPClient.GetNow();

            Console.WriteLine("Local Now: {0}, SNTP Now: {1}",
                now.ToString("yyyy-MM-dd HH:mm.ss.fff"),
                sntpNow.ToString("yyyy-MM-dd HH:mm.ss.fff"));
        }

        [TestMethod]
        public void LocalOffsetTest()
        {
            Console.WriteLine("Local Offset: {0}", SNTPClient.LocalOffset);
            Thread.Sleep(2000);
            Console.WriteLine("Local: {0}", DateTime.Now);
            Console.WriteLine("Local Offset: {0}", SNTPClient.LocalOffset);
            Console.WriteLine("Adjusted Default: {0}", SNTPClient.AdjustedTime);
            Console.WriteLine("Adjusted Local: {0}", SNTPClient.AdjustedLocalTime);
            Console.WriteLine("Adjusted Utc: {0}", SNTPClient.AdjustedUtcTime);
        }

        [TestMethod]
        public void AdjestTimeTest()
        {
            Console.WriteLine("Local Offset: {0}", SNTPClient.LocalOffset);
            Console.WriteLine("Adjusted Default: {0}", SNTPClient.AdjustedTime);
            Thread.Sleep(1000);
            Console.WriteLine("Local Offset: {0}", SNTPClient.LocalOffset);

            TimeZoneInfo oldTimeZoneInfo = TimeZoneContext.Current.CurrentTimeZone;

            TimeZoneContext.Current.CurrentTimeZone = TimeZoneInfo.CreateCustomTimeZone("SZ", TimeSpan.FromHours(4), "CHN", "CHN");
            try
            {
                Console.WriteLine("Adjusted New Time Zone: {0}", SNTPClient.AdjustedTime);
            }
            finally
            {
                TimeZoneContext.Current.CurrentTimeZone = oldTimeZoneInfo;
            }

        }
    }
}
