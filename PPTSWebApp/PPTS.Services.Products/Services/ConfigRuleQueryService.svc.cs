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
        public DiscountQueryResult QueryDiscountByCampusID(string CampusID)
        {
            DiscountQueryResult dqr = new DiscountQueryResult();
            dqr.Discount = DiscountAdapter.Instance.LoadByCampusID(CampusID);
            if (dqr.Discount != null)
            {
                dqr.DiscountItemCollection = DiscountItemAdapter.Instance.Load(builder=> builder.AppendItem("DiscountID", dqr.Discount.DiscountID)).ToList();
            }
            return dqr;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ExpenseQueryResult QueryExpenseByCampusID(string CampusID)
        {
            ExpenseQueryResult queryresult = new ExpenseQueryResult();
            queryresult.Expense = ExpenseAdapter.Instance.LoadByCampusID(CampusID);
            return queryresult;
        }
    }
}
