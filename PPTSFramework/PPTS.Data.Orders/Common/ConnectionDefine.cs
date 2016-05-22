using MCS.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders
{
    /// <summary>
    /// 数据库连接信息
    /// </summary>
    public static class ConnectionDefine
    {
        public const string PPTSOrderConnectionName = "PPTS_Order";

        public const string PPTSSearchConnectionName = "PPTS_Search";

        public const string PPTSMetaConnectionName = "PPTS_MetaData";
        /// <summary>
        /// 得到默认的数据操作上下文
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDbContext()
        {
            return DbContext.GetContext(PPTSOrderConnectionName);
        }
    }
}
