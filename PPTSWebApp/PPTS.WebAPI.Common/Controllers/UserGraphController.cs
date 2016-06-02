using System.Web.Http;
using PPTS.WebAPI.Common.Models;
using MCS.Library.Core;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.DataSources;
using PPTS.Data.Common.Entities;

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
    }
}