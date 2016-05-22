using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Products.DataSources;
using PPTS.WebAPI.Products.ViewModels.ExtraGift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Products.Controllers
{
    [ApiPassportAuthentication]
    public class ExtraGiftController : ApiController
    {
        #region api/extraGift/getAllPresents
        [HttpPost]
        public PresentsQueryResultModel getAllPresents(PresentsQueryCriteriaModel criteria) {
            return new PresentsQueryResultModel()
            {
                QueryResult = PresentDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Present))
            };
        }

        #endregion
    }
}