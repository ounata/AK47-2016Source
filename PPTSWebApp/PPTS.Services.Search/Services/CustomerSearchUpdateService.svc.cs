using PPTS.Contracts.Search.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Contracts.Search.Models;
using MCS.Library.WcfExtensions;
using System.ServiceModel.Web;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Adapters;
using System.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data;
using PPTS.Services.Search.Executors;

namespace PPTS.Services.Search.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“CustomerSearchUpdateService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 CustomerSearchUpdateService.svc 或 CustomerSearchUpdateService.svc.cs，然后开始调试。
    public class CustomerSearchUpdateService : ICustomerSearchUpdateService
    {
        #region 更新客户信息部分
        /// <summary>
        /// 单一学员更新客户部分信息
        /// </summary>
        /// <param name="model">数据模型</param>
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void UpdateByCustomerInfo(CustomerSearchUpdateModel model)
        {
            CustomerSearchProcessor.Process(model);
        }

        /// <summary>
        /// 批量学员更新客户部分信息
        /// </summary>
        /// <param name="modelCollection"></param>
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void UpdateByCustomerCollectionInfo(List<CustomerSearchUpdateModel> modelCollection)
        {
            CustomerSearchProcessor.Process(modelCollection);
        }
        #endregion

        #region 初始化CustomerSearch信息
        /// <summary>
        /// 批量学员初始化信息
        /// </summary>
        /// <param name="customerIDs"></param>
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void InitCustomerSearch(List<string> customerIDs)
        {
            CustomerSearchProcessor.Process(customerIDs);
        }
        #endregion 
    }
}
