using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.StopAlerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("增加停课休学记录")]
    public class AddCustomerStopAlertExecutor : PPTSEditCustomerExecutorBase<StopAlertCreateModel>
    {
        public AddCustomerStopAlertExecutor(StopAlertCreateModel model) : base(model, null)
        {
            //model.NullCheck("model");

            //model.Customer.NullCheck("Customer");
            //model.PrimaryParent.NullCheck("PrimaryParent");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            CustomerStopAlertAdapter.Instance.UpdateInContext(this.Model.StopAlert);
        }
    }
}
