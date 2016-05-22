using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using System;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("增加客服")]
    public class AddCustomerServiceExecutor : PPTSEditCustomerExecutorBase<CreatableCustomerServiceModel>
    {
        
        /// <summary>
        /// 添加客服
        /// </summary>
        /// <param name="model"></param>
        public AddCustomerServiceExecutor(CreatableCustomerServiceModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.Customer.NullCheck("Customer");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);


            //this.Model.Customer.FillCreator();
            this.Model.Customer.ServiceID = UuidHelper.NewUuidString();
            this.Model.Customer.AcceptTime = DateTime.Now;
            this.Model.Customer.ServiceStatus = "1";
            this.Model.Customer.CampusID = this.Model.CampusID;
            this.Model.Customer.CampusName = this.Model.CampusName;

            ///写入人信息
            this.Model.Customer.FillCreator();
            ///编辑人信息
            this.Model.Customer.FillModifier();
            ///
            this.Model.Customer.FillAccepter(DeluxeIdentity.CurrentUser);
            ///受理人信息
            CustomerServiceAdapter.Instance.UpdateInContext(this.Model.Customer);
        }
    }
}