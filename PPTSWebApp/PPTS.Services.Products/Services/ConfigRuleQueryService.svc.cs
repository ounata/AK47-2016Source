using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Contracts.Products.Models;
using PPTS.Contracts.Products.Operations;
using MCS.Library.WcfExtensions;
using System.ServiceModel.Web;
using MCS.Library.Data.Adapters;
using PPTS.Data.Products.Adapters;

namespace PPTS.Services.Products.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ConfigRuleQueryService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ConfigRuleQueryService.svc 或 ConfigRuleQueryService.svc.cs，然后开始调试。
    public class ConfigRuleQueryService : IConfigRuleQueryService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public DiscountQueryResult QueryDiscountByCampusID(string campusID)
        {
            DiscountQueryResult discountQueryResult = new DiscountQueryResult();
            discountQueryResult.Discount = DiscountAdapter.Instance.LoadByCampusID(campusID);
            if (discountQueryResult.Discount != null)
            {
                discountQueryResult.DiscountItemCollection = DiscountItemAdapter.Instance.Load(builder=> builder.AppendItem("DiscountID", discountQueryResult.Discount.DiscountID)).ToList();
            }
            return discountQueryResult;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ExpenseQueryResult QueryExpenseByCampusID(string campusID)
        {
            ExpenseQueryResult queryResult = new ExpenseQueryResult();
            queryResult.ExpenseCollection = ExpenseAdapter.Instance.LoadByCampusID(campusID).ToList();
            return queryResult;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public PresentQueryResult QueryPresentByCampusID(string campusID)
        {
            PresentQueryResult queryResult = new PresentQueryResult();
            queryResult.Present = PresentAdapter.Instance.LoadByCampusID(campusID);
            if (queryResult.Present != null)
            {
                queryResult.PresentItemCollection = PresentItemAdapter.Instance.Load(builder => builder.AppendItem("PresentID", queryResult.Present.PresentID)).ToList();
            }
            return queryResult;
        }
    }
}
