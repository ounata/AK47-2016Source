using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.WcfExtensions.Inspectors
{
    public static class WfClientInspector
    {
        /// <summary>
        /// 从方法调用获取参数信息
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static WfClientOperationInfo InspectParameters<TChannel>(Action<TChannel> action)
        {
            WfClientOperationInfo result = new WfClientOperationInfo();

            WfClientInspectorChannelFactory<TChannel> factory = new WfClientInspectorChannelFactory<TChannel>(
                (op, parameters) => result.Fill(op, parameters));

            TChannel proxy = factory.CreateChannel();

            if (action != null)
                ExceptionHelper.DoSilentAction(() => action(proxy));

            return result;
        }
    }
}
