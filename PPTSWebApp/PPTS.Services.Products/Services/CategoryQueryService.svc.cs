using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PPTS.Data.Products.Entities;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Products.Models;
using PPTS.Contracts.Products.Operations;
using PPTS.Data.Products.Adapters;

namespace PPTS.Services.Products.Services
{

    public class CategoryQueryService : ICategoryQueryService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public CategoryQueryResult QueryCategory()
        {
            return new CategoryQueryResult() { CategoryCollection = CategoryAdapter.Instance.LoadAll() };
        }
    }
}
