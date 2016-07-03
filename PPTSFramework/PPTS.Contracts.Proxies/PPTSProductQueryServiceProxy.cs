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
    public class PPTSProductQueryServiceProxy : WfClientServiceProxyBase<IProductQueryService>
    {
        public static readonly PPTSProductQueryServiceProxy Instance = new PPTSProductQueryServiceProxy();

        private PPTSProductQueryServiceProxy()
        {
        }

        /// <summary>
        /// 是否允许 手工确认
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool IsAssetConfirm(string productId)
        {
            return this.SingleCall(action => action.IsAssetConfirm(productId));
        }

        public bool IsExistsCampusInProduct(string[] campusIds, string[] productIds)
        {
            return this.SingleCall(action => action.IsExistsCampusInProduct(campusIds, productIds));
        }

        /// <summary>
        /// 通过产品ID集合获得产品信息
        /// </summary>
        /// <param name="productIDs">产品ID集合</param>
        /// <returns></returns>
        public ProductViewCollection QueryProductViewsByIDs(string[] productIDs)
        {

            PPTS.Contracts.Products.Models.ProductViewQueryResult productViewQueryResult = this.SingleCall(action => action.QueryProductViewsByIDs(productIDs));
            ProductViewCollection productViewCollection = new ProductViewCollection();
            foreach (ProductView productView in productViewQueryResult.ProductViews)
            {
                productViewCollection.Add(productView);
            }
            return productViewCollection;
        }

        protected override WfClientChannelFactory<IProductQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "productQueryService"));

            return new WfClientChannelFactory<IProductQueryService>(endPoint);
        }
    }
}
