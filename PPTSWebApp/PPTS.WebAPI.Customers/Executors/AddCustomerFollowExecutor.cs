using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("增加跟进记录")]
    public class AddCustomerFollowExecutor : PPTSEditCustomerExecutorBase<CreatableCustomerFollowModel>
    {
        public AddCustomerFollowExecutor(CreatableCustomerFollowModel model)
            : base(model, null)
        {
            //model.NullCheck("model");

            //model.Customer.NullCheck("Customer");
            //model.PrimaryParent.NullCheck("PrimaryParent");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            CustomerFollowAdapter.Instance.UpdateInContext(this.Model.Follow);
            //CustomerFollowItemAdapter.Instance.UpdateInContext(this.Model.FollowItems);
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