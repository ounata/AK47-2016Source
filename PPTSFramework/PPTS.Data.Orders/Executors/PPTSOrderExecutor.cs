using PPTS.Data.Common.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Core;

namespace PPTS.Data.Orders.Executors
{
    public class PPTSOrderExecutor : PPTSExecutorBase
    {
        public PPTSOrderExecutor(string opType) : base(opType)
        {

        }

        public string OrderId { set; get; }
        public Dictionary<string,object> EditPaymentParams;

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            if(OperationType == "EditPayment")
            {
                EditPaymentParams.NullCheck("EditPaymentParams");
                
                OrdersAdapter.Instance.Update(OrderId, EditPaymentParams);
                
            }
            return null;
            //throw new NotImplementedException();
        }


        protected override string GetOperationDescription()
        {
            var descrption = base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(descrption)) { return OperationType; }
            return descrption;
        }

    }
}
