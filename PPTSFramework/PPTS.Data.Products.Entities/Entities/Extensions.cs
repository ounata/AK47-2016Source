using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Entities
{
    /// <summary>
    ///  产品 扩展方法
    /// </summary>
    public static class Extensions
    {
        public static T FirstOrNull<T>(this EditableDataObjectCollectionBase<T> collection) {
            if (collection.Count > 0) { return collection[0]; }
            return default(T);
        }

        public static Product First(this ProductCollection collection)
        {
            if (collection.Count > 0) { return collection[0]; }
            return null;
        }


        //public static ProductSalaryRulesCollection ToProductSalaryRules(this List<ProductSalaryRules> list,string productId)
        //{
        //    productId.CheckStringIsNullOrEmpty("productId");
        //    ProductSalaryRulesCollection collection = new ProductSalaryRulesCollection();
        //    list.ForEach(m => collection.Add(m));
        //    return collection;
        //}

        //public static void FillFromProductSalaryRules(this List<ProductSalaryRules> list, ProductSalaryRulesCollection collection)
        //{
        //    list.NullCheck("list");
        //    collection.NullCheck("collection");
        //    list = collection.ToList();
        //}

    }
}
