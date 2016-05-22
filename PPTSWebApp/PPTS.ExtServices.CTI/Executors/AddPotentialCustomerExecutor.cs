using MCS.Library.Core;
using MCS.Library.Data.Executors;
using PPTS.Data.Customers.Executors;
using PPTS.ExtServices.CTI.Models.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Data;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;

namespace PPTS.ExtServices.CTI.Executors
{
    [DataExecutorDescription("CTI接口-增加潜在客户")]
    public class AddPotentialCustomerExecutor : PPTSEditCustomerExecutorBase<PotentialCustomerModel>
    {
        public AddPotentialCustomerExecutor(PotentialCustomerModel model) : base(model, null)
        {
            model.NullCheck("潜客集成对象为空");
            model.PotentialCustomer.NullCheck("潜客对象为空");
            model.Parent.NullCheck("家长信息为空");
            model.CustomerParentRelation.NullCheck("学员与家长关系信息为空");
            (model.PhoneCollection.Count <= 0).TrueThrow("家长联系方式信息为空");
            //model.CustomerSchoolRelation.NullCheck("学员与在读学校关系信息为空");
            model.CustomerStaffRelation.NullCheck("学员与员工关系信息为空");
            model.CustomerFollow.NullCheck("跟进信息为空");
        }

        protected override void Validate()
        {
            base.Validate();
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            PotentialCustomerAdapter.Instance.UpdateInContext(this.Model.PotentialCustomer);
            ParentAdapter.Instance.UpdateInContext(this.Model.Parent);
            CustomerParentRelationAdapter.Instance.UpdateInContext(this.Model.CustomerParentRelation);
            this.Model.PhoneCollection.ForEach(p => p.IsNotNull(phone => PhoneAdapter.Instance.UpdateInContext(phone)));
            CustomerStaffRelationAdapter.Instance.UpdateInContext(this.Model.CustomerStaffRelation);
            CustomerFollowAdapter.Instance.UpdateInContext(this.Model.CustomerFollow);
            this.Model.CustomerSchoolRelation.IsNotNull(r => CustomerSchoolRelationAdapter.Instance.UpdateInContext(r));
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }
        /// <summary>
        /// 准备日志日期
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            context.Logs.ForEach(log => log.ResourceID = this.Model.PotentialCustomer.CustomerID);
        }
    }
}