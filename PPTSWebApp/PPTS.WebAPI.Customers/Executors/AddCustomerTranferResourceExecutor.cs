using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("资源划转")]
    public class AddCustomerTranferResourceExecutor : PPTSEditCustomerExecutorBase<CreatableCustomerTransferResourceModel>
    {
        public AddCustomerTranferResourceExecutor(CreatableCustomerTransferResourceModel model)
            : base(model, null)
        {
            model.NullCheck("model");
            model.TransferResources.NullCheck("TransferResources");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);


            //this.Model.Customer.FillCreator();
            //this.Model.Customer.ServiceID = UuidHelper.NewUuidString();
            //this.Model.Customer.AcceptTime = DateTime.Now;

            //CustomerServiceAdapter.Instance.UpdateInContext(this.Model.Customer);
        }
    }
}