using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.ExtServices.LeYu.Models.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.ExtServices.LeYu.Executors
{
    [DataExecutorDescription("乐语接口-增加潜在客户")]
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
            model.PotentialCustomer.NullCheck("Customer");
        }

        /// <summary>
        /// 存储前校验规则
        /// </summary>
        protected override void Validate()
        {
            base.Validate();
            //this.Model.Customer.PrimaryPhone.ToPhoneNumber()
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            PotentialCustomerAdapter.Instance.UpdateInContext(this.Model.PotentialCustomer);
            ParentAdapter.Instance.UpdateInContext(this.Model.Parent);
            CustomerParentRelationAdapter.Instance.UpdateInContext(this.Model.CustomerParentRelation);
            this.Model.PhoneCollection.ForEach((Phone p) => p.IsNotNull(phone => PhoneAdapter.Instance.UpdateInContext(phone)));
            CustomerStaffRelationAdapter.Instance.UpdateInContext(this.Model.CustomerStaffRelation);
            CustomerFollowAdapter.Instance.UpdateInContext(this.Model.CustomerFollow);
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
            context.Logs.ForEach(log => log.ResourceID = this.Model.PotentialCustomer.CustomerID);
        }
    }
}