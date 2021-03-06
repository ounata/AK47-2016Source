﻿using PPTS.Contracts.Products.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PPTS.Data.Products.Entities;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Products.Models;
using PPTS.Data.Products.Adapters;

namespace PPTS.Services.Products.Services
{
    /// <summary>
    ///产品中心数据接口服务 
    /// </summary>
    public class ProductQueryService : IProductQueryService
    {
        /// <summary>
        /// 是否允许手工确认
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool IsAssetConfirm(string productId)
        {
            return ProductAdapter.Instance.IsAssetConfirm(productId);
        }

        /// <summary>
        /// 是否 存在校区 在 产品列表中
        /// </summary>
        /// <param name="campusIds"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool IsExistsCampusInProduct(string[] campusIds, string[] productIds)
        {
            return ProductPermissionAdapter.Instance.IsExistsCampusInProduct(campusIds, productIds);
        }

        /// <summary>
        /// 根据产品id集合返回对应的产品视图信息
        /// </summary>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ProductViewQueryResult IProductQueryService.QueryProductViewsByIDs(string[] productIDs)
        {
            MCS.Library.Data.Adapters.InLoadingCondition inLoadingCondition = new MCS.Library.Data.Adapters.InLoadingCondition(builder => { builder.AppendItem<string>(productIDs).DataField = "ProductID"; });
            return new ProductViewQueryResult()
            {
                ProductViews = PPTS.Data.Products.Adapters.ProductViewAdapter.Instance.LoadByInBuilder(inLoadingCondition).ToList<ProductView>()
            };
        }
    }
}
