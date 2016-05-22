using MCS.Library.Services;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivateWfProcessService
{
    public class ExecuteFixedTimeTasksThread : ThreadTaskBase
    {
        public override void OnThreadTaskStart()
        {
            FixedTimeTaskAdapter.Instance.FetchInTimeScopeTasks(
                FixedTimeTaskSettings.GetConfig().TimeTolerance,
                this.Params.BatchCount, task =>
            {
                GenerateSysTasks(task);
            });
        }

        private static void GenerateSysTasks(FixedTimeTask fixedTimeTask)
        {
            SysTaskAdapter.Instance.Update(fixedTimeTask.ToSysTask());
            FixedTimeTaskAdapter.Instance.Delete(fixedTimeTask);
        }
    }
}
