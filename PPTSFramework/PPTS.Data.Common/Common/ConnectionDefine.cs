using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common
{
    internal static class ConnectionDefine
    {
        /// <summary>
        /// 元数据库的连接名称
        /// </summary>
        public const string PPTSMetaDataConnectionName = "PPTS_MetaData";

        /// <summary>
        /// 权限中心的连接名称
        /// </summary>
        public const string PPTSPermissionCenterConnectionName = "PermissionsCenter";

        /// <summary>
        /// 在授权系统中PPTS的应用常量
        /// </summary>
        public const string PPTSApplicationName = "PPTS";
    }
}
