using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.WcfExtensions.Inspectors
{
    internal class WfClientInspectorBehavior : WebHttpBehavior
    {
        private Action<OperationDescription, object[]> _Action = null;

        public WfClientInspectorBehavior(Action<OperationDescription, object[]> action)
        {
            this._Action = action;
        }

        protected override IClientMessageFormatter GetRequestClientFormatter(OperationDescription operationDescription, ServiceEndpoint endpoint)
        {
            return new WfClientInspectorFormatter(operationDescription, this._Action);
        }
    }
}
