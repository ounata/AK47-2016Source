using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class POSRecordAdapter : CustomerAdapterBase<POSRecord, POSRecordCollection>
    {
        public static readonly POSRecordAdapter Instance = new POSRecordAdapter();

        private POSRecordAdapter()
        {
        }

    }
}
