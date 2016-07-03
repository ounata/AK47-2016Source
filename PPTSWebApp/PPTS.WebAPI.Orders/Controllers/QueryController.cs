using System.Web.Http;
using MCS.Web.MVC.Library.Filters;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class QueryController : ApiController
    {
        #region api/query/QueryClassGroup
        #endregion

        #region api/query/QueryAssignByCustomer
        #endregion

        #region api/query/QueryAssignByTeacher
        #endregion

        #region api/query/QueryOrder
        #endregion

        #region api/query/QueryOrderExchange
        #endregion

        #region api/query/QueryOrderStock
        #endregion
    }
}