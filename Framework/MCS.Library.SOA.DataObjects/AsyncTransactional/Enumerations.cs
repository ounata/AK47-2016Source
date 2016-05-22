using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.AsyncTransactional
{
    public enum TxProcessStatus
    {
        NotRunning,

        Running,

        Completed,

        RollingBack,

        RolledBack
    }

    public enum TxActivityStatus
    {
        NotRunning,

        Running,

        Completed,

        RollingBack,

        RolledBack
    }
}
