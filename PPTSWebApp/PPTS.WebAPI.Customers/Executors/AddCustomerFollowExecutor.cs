using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("增加跟进记录")]
    public class AddCustomerFollowExecutor : PPTSEditCustomerExecutorBase<CreatableFollowModel>
    {
        public AddCustomerFollowExecutor(CreatableFollowModel model)
            : base(model, null)
        {
            //model.NullCheck("model");

            //model.Customer.NullCheck("Customer");
            //model.PrimaryParent.NullCheck("PrimaryParent");
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            #region 生成数据权限范围数据

            PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerFollow>

               .GetInstance(PPTS.Data.Customers.ConnectionDefine.PPTSCustomerConnectionName)

               .UpdateAuthInContext(DeluxeIdentity.CurrentUser.GetCurrentJob()

               , DeluxeIdentity.CurrentUser.GetCurrentJob().Organization()

               , this.Model.Follow.CustomerID

               , PPTS.Data.Common.Authorization.RelationType.Owner);

            #endregion 生成数据权限范围数据

            return base.DoOperation(context);
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            this.Model.Follow.FillCreator();
            this.Model.InitFollower(DeluxeIdentity.CurrentUser);
            CustomerFollowAdapter.Instance.UpdateInContext(this.Model.Follow);

            //准备更新潜客或者学员的跟进信息的SQL
            if (this.Model.Follow.IsPotential)
            {
                PotentialCustomer summary = PotentialCustomerAdapter.Instance.Load(this.Model.Follow.CustomerID);
                this.Model.Follow.FillFollowSummary(summary);

                GenericPotentialCustomerAdapter<PotentialCustomer, PotentialCustomerCollection>.Instance.UpdateFollowSummaryInContext(summary);
            }
            else
            {
                Customer summary = CustomerAdapter.Instance.Load(this.Model.Follow.CustomerID);
                this.Model.Follow.FillFollowSummary(summary);

                GenericPotentialCustomerAdapter<Customer, CustomerCollection>.Instance.UpdateFollowSummaryInContext(summary);
            }
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
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