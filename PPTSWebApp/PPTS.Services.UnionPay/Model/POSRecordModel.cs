using PPTS.Data.Customers.Entities;
using PPTS.Services.UnionPay.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.Services.UnionPay.Model
{
    public class POSRecordModel
    {
        public POSRecordCollection POSRecordCollection
        {
            get;
            set;
        }

        public void UpdatePOSRecord()
        {
            AddPOSRecordExecutor executor = new AddPOSRecordExecutor(this);
            executor.Execute();
        }
    }
}