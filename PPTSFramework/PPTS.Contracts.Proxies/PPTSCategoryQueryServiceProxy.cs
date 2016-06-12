using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Products.Operations;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSCategoryQueryServiceProxy : WfClientServiceProxyBase<ICategoryQueryService>
    {
        public static readonly PPTSCategoryQueryServiceProxy Instance = new PPTSCategoryQueryServiceProxy();

        private PPTSCategoryQueryServiceProxy()
        {
        }

        /// <summary>
        /// 获取 2级 分类
        /// </summary>
        /// <returns></returns>
        public CategoryEntityCollection QueryCategories() {
            var result = new CategoryEntityCollection();
            this.SingleCall(a => a.QueryCategory()).CategoryCollection.ToList().ForEach(m => result.Add(m));
            return result;
        }

        protected override WfClientChannelFactory<ICategoryQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "categoryQueryService"));

            return new WfClientChannelFactory<ICategoryQueryService>(endPoint);
        }
    }
}
