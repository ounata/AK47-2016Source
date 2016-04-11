using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Product.ViewModels.Products
{

    public class ProductViewModel
    {
        public Data.Products.Entities.CategoryCatalogCollection Catalogs { set; get; }
        public CategoryType CategoryType { set; get; }
        public Data.Products.Entities.ProductView Product { set; get; }

        public Data.Products.Entities.ProductSalaryRuleCollection SalaryRules
        {
            get;
            set;
        }
        
        public Data.Products.Entities.ProductExOfCourse ExOfCourse
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<Data.Common.Entities.BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

    }
}