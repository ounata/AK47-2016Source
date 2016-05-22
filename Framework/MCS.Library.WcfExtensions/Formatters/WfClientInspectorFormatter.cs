using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.WcfExtensions.Inspectors
{
    internal class WfClientInspectorFormatter : IClientMessageFormatter
    {
        private OperationDescription _OperationDesc;
        private Action<OperationDescription, object[]> _Action = null;

        public WfClientInspectorFormatter(OperationDescription operation, Action<OperationDescription, object[]> action)
        {
            this._OperationDesc = operation;
            this._Action = action;
        }

        public object DeserializeReply(Message message, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
        {
            if (this._Action != null)
                this._Action(this._OperationDesc, parameters);

            throw new NotImplementedException();
        }
    }
}
