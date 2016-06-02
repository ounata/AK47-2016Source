using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
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
    public class AddCustomerVerifyExecutor : PPTSEditCustomerExecutorBase<CustomerVerifyModel>
    {
        public AddCustomerVerifyExecutor(CustomerVerifyModel model) : base(model, null)
        {

        }

        

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Model.FillCreator();
            this.Model.InitVerifier(DeluxeIdentity.CurrentUser);
            CustomerVerifyAdapter.Instance.UpdateInContext(this.Model);
        }
    }
}
