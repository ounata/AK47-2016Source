using MCS.Library.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.Caching
{
    public class ServiceBusCacheNotifyFetcher : CacheNotifyFetcherBase
    {
        public static readonly CacheNotifyFetcherBase Instance = new ServiceBusCacheNotifyFetcher();

        private ServiceBusCacheNotifyFetcher()
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
        }

        public override CacheNotifyData[] GetData()
        {
            throw new NotImplementedException();
        }

        public override TimeSpan GetInterval()
        {
            return TimeSpan.FromSeconds(2);
        }
    }
}
