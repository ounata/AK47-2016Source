using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Common.Security;
using MCS.Library.Principal;
using PPTS.Data.Common;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("划转资源Executor")]
    public class EditCustomerTransferResourcesExecutor : PPTSEditCustomerExecutorBase<EditCustomerTransferResourcesModel>
    {
        public EditCustomerTransferResourcesExecutor(EditCustomerTransferResourcesModel model) : base(model, null)
        { }
        /// <summary>
        ///划转资源
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            DateTime now = DateTime.Now;
            foreach (var m in Model.CustomerTransferResources)
            {
                m.TransferID= UuidHelper.NewUuidString();
                m.TransferTime = now;
                m.TransferorName = DeluxeIdentity.CurrentUser.Name;
                m.TransferorJobID= DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
                m.TransferorJobName= DeluxeIdentity.CurrentUser.GetCurrentJob().JobName;

                m.FillCreator();
                CustomerTransferResourcesAdapter.Instance.UpdateInContext(m);
            }
        }


    }
}