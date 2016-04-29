using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Products.Models;
using PPTS.Contracts.Products.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    /// <summary>
    /// 配置规则服务代理类
    /// </summary>
    public class PPTSConfigRuleQueryServiceProxy : PPTSClientServiceProxyBase<IConfigRuleQueryService>
    {
        public static readonly PPTSConfigRuleQueryServiceProxy Instance = new PPTSConfigRuleQueryServiceProxy();

        private PPTSConfigRuleQueryServiceProxy()
        {
        }

        /// <summary>
        /// 通过校区ID获得折扣表配置信息
        /// </summary>
        /// <param name="CampusID">校区ID</param>
        /// <returns></returns>
        public DiscountQueryResult QueryDiscountByCampusID(string CampusID)
        {
            DiscountQueryResult pqr= this.SingleCall(action => action.QueryDiscountByCampusID(CampusID));
            return pqr;
        }

        /// <summary>
        /// 通过校区ID获得服务费信息
        /// </summary>
        /// <param name="CampusID">校区ID</param>
        /// <returns></returns>
        public ExpenseQueryResult QueryExpenseByCampusID(string CampusID)
        {
            ExpenseQueryResult queryresult= this.SingleCall(action => action.QueryExpenseByCampusID(CampusID));
            return queryresult;
        }

        protected override WfClientChannelFactory<IConfigRuleQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "configruleQueryService"));

            return new WfClientChannelFactory<IConfigRuleQueryService>(endPoint);
        }
    }
}
