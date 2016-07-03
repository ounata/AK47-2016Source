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
    public class PPTSConfigRuleQueryServiceProxy : WfClientServiceProxyBase<IConfigRuleQueryService>
    {
        public static readonly PPTSConfigRuleQueryServiceProxy Instance = new PPTSConfigRuleQueryServiceProxy();

        private PPTSConfigRuleQueryServiceProxy()
        {
        }

        /// <summary>
        /// 通过校区ID获得折扣表配置信息
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <returns></returns>
        public DiscountQueryResult QueryDiscountByCampusID(string campusID)
        {
            DiscountQueryResult discountQueryResult = this.SingleCall(action => action.QueryDiscountByCampusID(campusID));
            return discountQueryResult;
        }

        /// <summary>
        /// 通过折扣ID获得折扣表配置信息
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <returns></returns>
        public DiscountQueryResult QueryDiscountByDiscountID(string discountID)
        {
            DiscountQueryResult discountQueryResult = this.SingleCall(action => action.QueryDiscountByDiscountID(discountID));
            return discountQueryResult;
        }

        /// <summary>
        /// 通过校区ID获得服务费信息
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <returns></returns>
        public ExpenseQueryResult QueryExpenseByCampusID(string campusID)
        {
            ExpenseQueryResult queryResult= this.SingleCall(action => action.QueryExpenseByCampusID(campusID));
            return queryResult;
        }

        /// <summary>
        /// 通过校区ID获得买赠配置信息
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <returns></returns>
        public PresentQueryResult QueryPresentByCampusID(string campusID)
        {
            PresentQueryResult queryResult = this.SingleCall(action => action.QueryPresentByCampusID(campusID));
            return queryResult;
        }


        protected override WfClientChannelFactory<IConfigRuleQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "configruleQueryService"));

            return new WfClientChannelFactory<IConfigRuleQueryService>(endPoint);
        }
    }
}
