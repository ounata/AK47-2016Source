using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using PPTS.Data.Common.Adapters;

namespace PPTS.Data.Common
{
    public class ConfigsCache
    {
        /// <summary>
        /// 获取配置参数。
        /// </summary>
        /// <param name="orgID">机构ID</param>
        /// <returns></returns>
        public static ConfigArgs GetArgs(string orgID)
        {
            return ConfigAdapter.Instance.Load(orgID);
        }
    }
}
