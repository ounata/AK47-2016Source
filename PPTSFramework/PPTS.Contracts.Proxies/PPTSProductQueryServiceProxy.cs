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
    public class PPTSProductQueryServiceProxy : PPTSClientServiceProxyBase<IProductQueryService>
    {
        public static readonly PPTSProductQueryServiceProxy Instance = new PPTSProductQueryServiceProxy();

        private PPTSProductQueryServiceProxy()
        {
        }

        /// <summary>
        /// 通过产品ID集合获得产品信息
        /// </summary>
        /// <param name="productids">产品ID集合</param>
        /// <returns></returns>
        public ProductViewCollection QueryProductViewsByIDs(string[] productids)
        {

            PPTS.Contracts.Products.Models.ProductViewQueryResult pvqr = this.SingleCall(action => action.QueryProductViewsByIDs(productids));
            ProductViewCollection pvc = new ProductViewCollection();
            foreach (ProductView pv in pvqr.ProductViews)
            {
                pvc.Add(pv);
            }
            return pvc;
        }

        protected override WfClientChannelFactory<IProductQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "productQueryService"));

            return new WfClientChannelFactory<IProductQueryService>(endPoint);
        }
    }
}
