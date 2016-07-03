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

        //public string OrderId { set; get; }
        //public Dictionary<string,object> EditPaymentParams;

        public Data.Orders.Entities.Order Order { set; get; }

        public string[] CampusIDs { set; get; }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            if(OperationType == "EditPayment")
            {
                Order.NullCheck("Order");
                OrdersAdapter.Instance.ModifyChargeApply(Order, CampusIDs);
            }
            return null;

        }


        protected override string GetOperationDescription()
        {
            var descrption = base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(descrption)) { return OperationType; }
            return descrption;
        }

    }
}
