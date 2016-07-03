using System.Web.Http;
using PPTS.WebAPI.Common.Models;
using MCS.Library.Core;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.DataSources;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using System.Collections.Generic;
using MCS.Web.MVC.Library.Models;

namespace PPTS.WebAPI.Common.Controllers
{
    public class UserGraphController : ApiController
    {
        [HttpPost]
        public SelectionItemCollection GetData(QueryUserParams queryParams)
        {
            queryParams.NullCheck("queryParams");

            PPTSJob educatorJob = queryParams.Jobs.Find(job => job.JobType == queryParams.JobType);

            if (educatorJob != null)
            {
                UserAndJobDataSource dataSource = new UserAndJobDataSource(educatorJob.Organization(), queryParams.JobType);

                int totalCount = -1;

                UserAndJobCollection items = dataSource.Query(0, int.MaxValue, ref totalCount);

                return items.ToSelectionItems();
            }

            return new SelectionItemCollection();
        }

        [HttpPost]
        public SchoolCollection GetSchools(UserGraphSearchParams searchParams)
        {
            return SchoolAdapter.Instance.LoadSchools(searchParams.SearchTerm, searchParams.MaxCount);
        }

        [HttpPost]
        public List<string> GetAddress(UserGraphSearchParams searchParams)
        {
            return ParentAdapter.Instance.LoadAddress(searchParams.SearchTerm, searchParams.MaxCount);
        }
    }
}