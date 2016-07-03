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
    [DataExecutorDescription("编辑客服")]
    public class EditableStudentSverviceExecutor : PPTSEditCustomerExecutorBase<EditableCustomerServiceModel>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public EditableStudentSverviceExecutor(EditableCustomerServiceModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.CustomerService.NullCheck("CustomerService");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            

            ///编辑人信息
            this.Model.Customer.FillModifier();

            this.Model.CustomerServiceItem = new Data.Customers.Entities.CustomerServiceItem();

            if (this.Model.CustomerService.HandlerJobName != "")
            {
                this.Model.CustomerServiceItem.ItemID = UuidHelper.NewUuidString();
                this.Model.CustomerServiceItem.ServiceID = this.Model.CustomerService.ServiceID;
                this.Model.CustomerServiceItem.HandleTime = MCS.Library.Net.SNTP.SNTPClient.AdjustedTime;
                this.Model.CustomerServiceItem.HandleStatus = "5";
                this.Model.CustomerServiceItem.HandlerID = DeluxeIdentity.CurrentUser.ID;
                this.Model.CustomerServiceItem.HandlerName = DeluxeIdentity.CurrentUser.Name;
                this.Model.CustomerServiceItem.HandlerJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
                this.Model.CustomerServiceItem.HandlerJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;

                string schoolName = DeluxeIdentity.CurrentUser.GetCurrentJob().Organization().Name;

                //DeluxeIdentity.CurrentUser.GetCurrentJob().Organization().GetUpperDataScope().Name;
            }

            CustomerServiceAdapter.Instance.UpdateInContext(this.Model.CustomerService);
            if (this.Model.CustomerService.HandlerJobName != "")
            {
                CustomerServiceItemsAdapter.Instance.UpdateInContext(this.Model.CustomerServiceItem);
            }

        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);

            context.Logs.ForEach(log => log.ResourceID = this.Model.CustomerService.ServiceID);
        }
    }
}