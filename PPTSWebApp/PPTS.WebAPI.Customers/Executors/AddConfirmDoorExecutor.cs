using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerVerifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("新增上门记录")]
    public class AddConfirmDoorExecutor : PPTSEditCustomerExecutorBase<ConfirmDoorCreateModel>
    {
        public AddConfirmDoorExecutor(ConfirmDoorCreateModel model) : base(model, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            ConfirmDoorAdapter.Instance.UpdateInContext(this.Model.ConfirmDoor);
        }
    }
}
