using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using MCS.Services.Test.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.SysTasks.Test
{
    public class TaskDemoServiceProxy : WfClientServiceProxyBase<ITaskDemoService>
    {
        public static TaskDemoServiceProxy Instance = new TaskDemoServiceProxy();

        private TaskDemoServiceProxy()
        {
        }

        public UserData UpdateUser(UserData user)
        {
            return this.SingleCall((action) => action.UpdateUser(user));
        }

        public EndpointAddress GetEndpoint()
        {
            return new EndpointAddress(UriSettings.GetConfig().CheckAndGet("mcs.soa.systask", "taskDemoService").ToString());
        }

        protected override WfClientChannelFactory<ITaskDemoService> GetService()
        {
            EndpointAddress endPoint = this.GetEndpoint();

            return new WfClientChannelFactory<ITaskDemoService>(endPoint);
        }
    }
}
