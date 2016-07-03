using MCS.Library.Data.Configuration;
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
        public static string PPTSMetaDataConnectionName
        {
            get
            {
                return ConnectionNameMappingSettings.GetConfig().GetConnectionName("PPTS_MetaData", "PPTS_MetaData");
            }
        }

        /// <summary>
        /// 权限中心的连接名称
        /// </summary>
        public static string PPTSPermissionCenterConnectionName
        {
            get
            {
                return ConnectionNameMappingSettings.GetConfig().GetConnectionName("PermissionsCenter", "PermissionsCenter");
            }
        }

        public static string PPTSCustomerConnectionName
        {
            get
            {
                return ConnectionNameMappingSettings.GetConfig().GetConnectionName("PPTS_Customer", "PPTS_Customer");
            }
        }

        public static string PPTSOrderConnectionName
        {
            get
            {
                return ConnectionNameMappingSettings.GetConfig().GetConnectionName("PPTS_Order", "PPTS_Order");
            }
        }

        /// <summary>
        /// 在授权系统中PPTS的应用常量
        /// </summary>
        public const string PPTSApplicationName = "PPTS";
    }
}
