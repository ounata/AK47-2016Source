using MCS.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products
{
    /// <summary>
    /// 数据库连接信息
    /// </summary>
    public static class ConnectionDefine
    {
        public const string PPTSProductConnectionName = "PPTS_Product";

        /// <summary>
        /// 得到默认的数据操作上下文
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDbContext()
        {
            return DbContext.GetContext(PPTSProductConnectionName);
        }
    }
}
