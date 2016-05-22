using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.AsyncTransactional
{
    [Serializable]
    public class TxProcessContext : WfContextDictionaryBase<string, object>
    {
        public TxProcessContext()
        {
        }

        public TxProcessContext(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class TxActivityContext : WfContextDictionaryBase<string, object>
    {
        public TxActivityContext()
        {
        }

        public TxActivityContext(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
