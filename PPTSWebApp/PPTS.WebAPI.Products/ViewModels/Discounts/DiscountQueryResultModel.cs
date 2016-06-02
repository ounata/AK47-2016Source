using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Discounts
{
    public class DiscountQueryResultModel
    {
        public PagedQueryResult<PPTS.Data.Products.Entities.Discount, DiscountCollection> QueryResult { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}