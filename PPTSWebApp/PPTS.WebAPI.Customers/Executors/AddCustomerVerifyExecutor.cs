using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
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

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            #region 生成数据权限范围数据

            PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerVerify>

               .GetInstance(PPTS.Data.Customers.ConnectionDefine.PPTSCustomerConnectionName)

               .UpdateAuthInContext(DeluxeIdentity.CurrentUser.GetCurrentJob()

               , DeluxeIdentity.CurrentUser.GetCurrentJob().Organization()

               , this.Model.CustomerID

               , PPTS.Data.Common.Authorization.RelationType.Owner);

            #endregion 生成数据权限范围数据

            return base.DoOperation(context);
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Model.FillCreator();
            this.Model.InitVerifier(DeluxeIdentity.CurrentUser);
            CustomerFollowCollection followList = CustomerFollowAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", Model.CustomerID));
            
            if (followList != null && followList.Count > 0)
            {
                CustomerFollow follow = followList.OrderByDescending(m => m.CreateTime).FirstOrDefault();
                Model.IsInvited = 1;
                Model.PlanVerifyTime = follow.CreateTime;
            }
            else
            {
                Model.IsInvited = 0;
            }

            CustomerVerifyAdapter.Instance.UpdateInContext(this.Model);
        }
    }
}
