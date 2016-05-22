using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects
{
    [Serializable]
    public class TxActivityActionServiceTask : InvokeServiceTask
    {
        public TxActivityActionServiceTask()
        {
            this.TaskType = "InvokeTxActivityActionService";
        }

        public TxActivityActionServiceTask(SysTask other)
            : base(other)
        {
        }
    }
}
