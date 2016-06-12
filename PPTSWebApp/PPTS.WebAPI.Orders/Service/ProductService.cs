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
        /// 获取2级产品分类
        /// </summary>
        /// <returns></returns>
        public static CategoryEntityCollection GetCategories() {
            return PPTS.Contracts.Proxies.PPTSCategoryQueryServiceProxy.Instance.QueryCategories();
        }


    }
    


    public class Present : Data.Products.Entities.Present {
        [DataMember]
        public List<PresentItem> Items { set; get; }
    }
    
}