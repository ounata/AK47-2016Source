using System;
using System.Linq;
using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerTransferApplyAdapter : CustomerAdapterBase<CustomerTransferApply, CustomerTransferApplyCollection>
    {
        public static readonly CustomerTransferApplyAdapter Instance = new CustomerTransferApplyAdapter();

        private CustomerTransferApplyAdapter()
        {
        }
        
        public CustomerTransferApply LoadByApplyID(string applyID)
        {
            return this.Load(builder => builder.AppendItem("ApplyID", applyID)).SingleOrDefault();
        }


        protected override void BeforeInnerUpdateInContext(CustomerTransferApply data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(CustomerTransferApply data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(CustomerTransferApply data)
        {
            if (data.ApplyID.IsNullOrEmpty())
                data.ApplyID = System.Guid.NewGuid().ToString();
        }
    }
}