using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customer.ViewModels.CustomerFollows;

namespace PPTS.WebAPI.Customer.Executors
{
    [DataExecutorDescription("编辑跟进记录")]
    public class EditCustomerFollowExecutor : PPTSEditCustomerExecutorBase<EditableCustomerFollowModel>
    {
        public EditCustomerFollowExecutor(EditableCustomerFollowModel model)
            : base(model, null)
        {
            //model.NullCheck("model");

            //model.Customer.NullCheck("Customer");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            CustomerFollowAdapter.Instance.UpdateInContext(this.Model.Follow);

            //PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.Customer.CustomerID,
            //    this.Model.Customer.ToPhones(this.Model.Customer.CustomerID));
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);

            context.Logs.ForEach(log => log.ResourceID = this.Model.Follow.FollowID);
        }
    }
}