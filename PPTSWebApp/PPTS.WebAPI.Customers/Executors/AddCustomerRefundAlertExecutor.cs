using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.RefundAlerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("增加退费预警记录")]
    public class AddCustomerRefundAlertExecutor : PPTSEditCustomerExecutorBase<RefundAlertCreateModel>
    {
        public AddCustomerRefundAlertExecutor(RefundAlertCreateModel model) : base(model, null)
        {
            //model.NullCheck("model");

            //model.Customer.NullCheck("Customer");
            //model.PrimaryParent.NullCheck("PrimaryParent");
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            #region 生成数据权限范围数据

            PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerRefundAlerts>

               .GetInstance(PPTS.Data.Customers.ConnectionDefine.PPTSCustomerConnectionName)

               .UpdateAuthInContext(DeluxeIdentity.CurrentUser.GetCurrentJob()

               , DeluxeIdentity.CurrentUser.GetCurrentJob().Organization()

               , this.Model.RefundAlert.CustomerID

               , PPTS.Data.Common.Authorization.RelationType.Owner);

            #endregion 生成数据权限范围数据

            return base.DoOperation(context);
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            CustomerRefundAlertAdapter.Instance.UpdateInContext(this.Model.RefundAlert);
        }
    }
}
