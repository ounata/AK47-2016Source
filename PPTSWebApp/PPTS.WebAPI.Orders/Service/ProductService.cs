using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.Service
{
    public class ProductService
    {
        public static List<ProductView> GetProductsByIds(params string[] ids)
        {
            return PPTS.Contracts.Proxies.PPTSProductQueryServiceProxy.Instance.QueryProductViewsByIDs(ids).ToList();

            //var mapper = new AutoMapper.MapperConfiguration(c => c.CreateMap<Data.Products.Entities.ProductView, Product>()).CreateMapper();
            //var results = Data.Products.Adapters.ProductViewAdapter.Instance.LoadByInBuilder(new MCS.Library.Data.Adapters.InLoadingCondition() { DataField = "ProductId", BuilderAction = where => where.AppendItem(ids) });
            //return results.Select(m =>
            //{
            //    var n = mapper.Map<Product>(m);
            //    return n;
            //}).ToList();

        }

        /// <summary>
        /// 获取买赠信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static Present GetPresentByOrgId(string orgId)
        {
            var presetCollection = Data.Products.Adapters.PresentAdapter.Instance.LoadByOrgId(orgId);
            var presetItemCollection = Data.Products.Adapters.PresentItemAdapter.Instance.LoadByPresentIds(presetCollection.Select(m => m.PresentID).ToArray());

            var mapper = new AutoMapper.MapperConfiguration(c => {
                c.CreateMap<Data.Products.Entities.Present, Present>();
                c.CreateMap<Data.Products.Entities.PresentItem, PresentItem>();
            }).CreateMapper();

            var result = new List<Present>();
            presetCollection.ForEach(m => {
                var n = mapper.Map<Present>(m);
                n.Items = presetItemCollection.Where(c => c.PresentID == m.PresentID).OrderByDescending(o=>o.PresentStandard).Select(cm => mapper.Map<PresentItem>(cm)).ToList();
                result.Add(n);
            });
            return result.FirstOrDefault();
        }


        /// <summary>
        /// 获取服务费用通过校区id
        /// </summary>
        /// <param name="campusId"></param>
        /// <returns></returns>
        public static Data.Products.Entities.Expense GetServiceChargeByCampusId(string campusId)
        {
            return PPTS.Contracts.Proxies.PPTSConfigRuleQueryServiceProxy.Instance.QueryExpenseByCampusID(campusId).Expense;
        }


    }

    //public class Product : Data.Products.Entities.ProductView { }



    public class Present : Data.Products.Entities.Present {
        [DataMember]
        public List<PresentItem> Items { set; get; }
    }

    public class PresentItem : Data.Products.Entities.PresentItem { }


}