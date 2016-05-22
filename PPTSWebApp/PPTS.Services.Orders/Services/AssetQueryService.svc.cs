using MCS.Library.WcfExtensions;
using PPTS.Contracts.Orders.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PPTS.Contracts.Orders.Models;
using PPTS.Data.Orders.Adapters;

namespace PPTS.Services.Orders.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class AssetQueryService : IAssetQueryService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public AssetStatisticQueryResult QueryAssetStatisticByAccountID(string accountID)
        {
            AssetStatisticQueryResult result = new AssetStatisticQueryResult();
            result.AssetsValue = AssetAdapter.Instance.LoadAssetsValueByAccountID(accountID);
            return result;
        }


    }
}
