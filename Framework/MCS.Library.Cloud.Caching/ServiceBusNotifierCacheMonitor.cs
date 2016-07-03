using MCS.Library.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.Caching
{
    public class ServiceBusNotifierCacheMonitor : NotifierCacheMonitorBase
    {
        public static readonly ServiceBusNotifierCacheMonitor Instance = new ServiceBusNotifierCacheMonitor();

        protected override CacheNotifyFetcherBase GetDataFetcher()
        {
            throw new NotImplementedException();
        }
    }
}
