using MCS.Library.Data;
using System.Collections.Generic;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products.Entities;


namespace PPTS.WebAPI.Products.ViewModels.Products
{
    public class ProductQueryResult
    {
        
        public PagedQueryResult<PPTS.Data.Products.Entities.ProductView, ProductViewCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}