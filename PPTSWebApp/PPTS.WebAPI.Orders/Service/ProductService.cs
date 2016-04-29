using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.Service
{
    public class ProductService
    {
        public static List<Product> GetProductsByIds(params string[] ids)
        {
            var mapper = new AutoMapper.MapperConfiguration(c => c.CreateMap<Data.Products.Entities.ProductView, Product>()).CreateMapper();
            var results = Data.Products.Adapters.ProductViewAdapter.Instance.LoadByInBuilder(new MCS.Library.Data.Adapters.InLoadingCondition() { DataField = "ProductId", BuilderAction = where => where.AppendItem(ids) });
            return results.Select(m =>
            {
                var n = mapper.Map<Product>(m);
                return n;
            }).ToList();

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
            return result.First();
        }


    }

    public class Product : Data.Products.Entities.ProductView { }



    public class Present : Data.Products.Entities.Present {
        [DataMember]
        public List<PresentItem> Items { set; get; }
    }

    public class PresentItem : Data.Products.Entities.PresentItem { }


}