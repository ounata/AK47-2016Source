using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customer.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customer.Executors
{
    [DataExecutorDescription("编辑潜在客户")]
    public class EditPotentialCustomerExecutor : PPTSEditCustomerExecutorBase<EditablePotentialCustomerModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public EditPotentialCustomerExecutor(EditablePotentialCustomerModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.Customer.NullCheck("Customer");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            PotentialCustomerAdapter.Instance.UpdateInContext(this.Model.Customer);

            PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.Customer.CustomerID,
                this.Model.Customer.ToPhones(this.Model.Customer.CustomerID));
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