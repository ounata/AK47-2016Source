using MCS.Library.Services;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivateWfProcessService
{
    public class ClearFixedTimeTasksThread : ThreadTaskBase
    {
        public override void OnThreadTaskStart()
        {
            FixedTimeTaskAdapter.Instance.DeleteExpiredTasks();
        }
    }
}
