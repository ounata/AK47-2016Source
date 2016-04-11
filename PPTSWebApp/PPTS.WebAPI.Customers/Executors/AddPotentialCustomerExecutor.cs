using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Validation;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace PPTS.WebAPI.Customers.Executors
{
    /// <summary>
    /// 
    /// </summary>
    [DataExecutorDescription("增加潜在客户")]
    public class AddPotentialCustomerExecutor : PPTSEditCustomerExecutorBase<CreatablePortentialCustomerModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public AddPotentialCustomerExecutor(CreatablePortentialCustomerModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.Customer.NullCheck("Customer");
            model.PrimaryParent.NullCheck("PrimaryParent");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            PotentialCustomerAdapter.Instance.UpdateInContext(this.Model.Customer);
            ParentAdapter.Instance.UpdateInContext(this.Model.PrimaryParent);
            CustomerParentRelationAdapter.Instance.UpdateInContext(this.Model.ToRelation());

            PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.Customer.CustomerID,
                this.Model.Customer.ToPhones(this.Model.Customer.CustomerID));

            PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.PrimaryParent.ParentID,
                this.Model.PrimaryParent.ToPhones(this.Model.PrimaryParent.ParentID));
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);

            context.Logs.ForEach(log => log.ResourceID = this.Model.Customer.CustomerID);
        }
    }
}