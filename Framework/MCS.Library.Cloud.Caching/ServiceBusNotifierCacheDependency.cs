using MCS.Library.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.Caching
{
    public class ServiceBusNotifierCacheDependency : NotifierCacheDependencyBase
    {
        protected override NotifierCacheMonitorBase GetMonitor()
        {
            return ServiceBusNotifierCacheMonitor.Instance;
        }
    }
}
