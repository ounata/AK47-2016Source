using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MCS.Library.Core;

namespace PPTS.WebAPI.Orders.Service
{
    public class ProductService
    {
        public static List<ProductView> GetProductsByIds(params string[] ids)
        {
            return PPTS.Contracts.Proxies.PPTSProductQueryServiceProxy.Instance.QueryProductViewsByIDs(ids).ToList();
            
        }

        /// <summary>
        /// 获取买赠信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static Present GetPresentByOrgId(string orgId)
        {
            var serviceResult = PPTS.Contracts.Proxies.PPTSConfigRuleQueryServiceProxy.Instance.QueryPresentByCampusID(orgId);
            if (serviceResult.PresentItemCollection ==null) return null;

            var mapper = new AutoMapper.MapperConfiguration(c =>
            {
                c.CreateMap<Data.Products.Entities.Present, Present>();
            }).CreateMapper();
            var result = mapper.Map<Present>(serviceResult.Present);
            result.Items = serviceResult.PresentItemCollection.OrderByDescending(o => o.PresentStandard).ToList();

            return result;
        }


        /// <summary>
        /// 获取服务费用通过校区id
        /// </summary>
        /// <param name="campusId"></param>
        /// <returns></returns>
        public static List<Data.Products.Entities.Expense> GetServiceChargeByCampusId(string campusId)
        {
            return PPTS.Contracts.Proxies.PPTSConfigRuleQueryServiceProxy.Instance.QueryExpenseByCampusID(campusId).ExpenseCollection;
        }

        /// <summary>
        /// 是否存在插班信息
        /// </summary>
        /// <param name="campusId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static bool IsExistsTransfer(string []campusId , string []productId)
        {
            return true;
        }

        /// <summary>
        /// 是否允许 手工 确认非上课类收入
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static bool IsAssetConfirm(string productId)
        {
            return PPTS.Contracts.Proxies.PPTSProductQueryServiceProxy.Instance.IsAssetConfirm(productId);
        }

        /// <summary>
        /// 是否 存在校区 在 产品列表中
        /// </summary>
        /// <param name="campusIds"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public static bool IsExistsCampusInProduct(string[] campusIds, string[] productIds)
        {
            return PPTS.Contracts.Proxies.PPTSProductQueryServiceProxy.Instance.IsExistsCampusInProduct(campusIds, productIds);
        }

    }
    


    public class Present : Data.Products.Entities.Present {
        [DataMember]
        public List<PresentItem> Items { set; get; }
    }
    
}