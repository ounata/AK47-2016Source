using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Products.Entities;
using System;

namespace PPTS.Data.Products.Adapters
{
    public class DiscountItemAdapter : ProductAdapterBase<DiscountItem, DiscountItemCollection>
    {
        public static DiscountItemAdapter Instance = new DiscountItemAdapter();

        /// <summary>
        /// 根据折扣ID获取折扣信息。
        /// </summary>
        /// <param name="discountID">折扣ID</param>
        /// <returns></returns>
        public DiscountItemCollection LoadCollectionByDiscountID(string discountID)
        {
            return this.Load(builder => builder.AppendItem("DiscountID", discountID));
        }
    }
}
