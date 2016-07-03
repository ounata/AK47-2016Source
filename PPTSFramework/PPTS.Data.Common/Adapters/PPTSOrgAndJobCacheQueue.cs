using MCS.Library.Caching;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Adapters
{
    public class PPTSOrgAndJobCacheQueue : CacheQueue<string, OguDataCollection<IUser>>
    {
        public static readonly PPTSOrgAndJobCacheQueue Instance = CacheManager.GetInstance<PPTSOrgAndJobCacheQueue>();
    }
}
