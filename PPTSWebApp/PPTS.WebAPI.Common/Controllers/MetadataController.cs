using System.Web.Http;
using PPTS.Data.Common.Adapters;
using PPTS.WebAPI.Common.Models;
using MCS.Library.Core;

namespace PPTS.WebAPI.Common.Controllers
{
    public class MetadataController : ApiController
    {
        [HttpPost]
        public SelectionItemCollection GetData(QueryMetadataParams queryParams)
        {
            queryParams.NullCheck("queryParams");
            queryParams.ParentKey.CheckStringIsNullOrEmpty("ParentKey");

            var items = ConstantAdapter.Instance.LoadByCategoryAndParentKey(queryParams.Category, queryParams.ParentKey);

            return items.ToSelectionItems();
        }
    }
}