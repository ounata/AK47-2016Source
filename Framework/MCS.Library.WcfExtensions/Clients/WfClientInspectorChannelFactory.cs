using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.WcfExtensions.Inspectors
{
    /// <summary>
    /// 监听器的Channel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WfClientInspectorChannelFactory<T> : ChannelFactory<T>
    {
        /// <summary>
        /// 使用WfRawContentWebHttpBinding初始化客户端
        /// </summary>
        /// <param name="address"></param>
        public WfClientInspectorChannelFactory(Action<OperationDescription, object[]> action)
            : base(new WfRawContentWebHttpBinding(), new EndpointAddress("http://localhost"))
        {
            if (!this.Endpoint.Behaviors.Contains(typeof(WfClientInspectorBehavior)))
                this.Endpoint.Behaviors.Add(new WfClientInspectorBehavior(action));
        }
    }
}
