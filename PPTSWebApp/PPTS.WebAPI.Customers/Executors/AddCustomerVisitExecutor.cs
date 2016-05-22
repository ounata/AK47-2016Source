using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using System;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("增加回访")]
    public class AddCustomerVisitExecutor : PPTSEditCustomerExecutorBase<CreatableCustomerVisitModel>
    {
        /// <summary>
        /// 添加回访
        /// </summary>
        /// <param name="model"></param>
        public AddCustomerVisitExecutor(CreatableCustomerVisitModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.CustomerVisit.NullCheck("CustomerVisit");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            

            //this.Model.Customer.FillCreator();
            this.Model.CustomerVisit.VisitID = UuidHelper.NewUuidString();

            this.Model.CustomerVisit.CampusID = this.Model.CampusID;

            this.Model.CustomerVisit.CampusName = this.Model.CampusName;

            ///写入人信息
            this.Model.CustomerVisit.FillCreator();
            ///编辑人信息
            this.Model.CustomerVisit.FillModifier();
            ///
            this.Model.CustomerVisit.FillAccepter(DeluxeIdentity.CurrentUser);
            ///受理人信息
            CustomerVisitAdapter.Instance.UpdateInContext(this.Model.CustomerVisit);
        }
    }
}