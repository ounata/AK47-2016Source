using System.Web.Http;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.Controllers
{
    public class AccountsController : ApiController
    {

        [HttpGet]
        public AccountCombinationModel GetAccountCombinationInfo(string customerID)
        {
            //PPTS.Data.Customers.Entities.Customer customer = CustomerAdapter

            throw new System.Exception();
        }
    }
}