using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("客户与员工归属关系Executor")]
    public class EditCustomerStaffRelationsExecutor : PPTSEditCustomerExecutorBase<EditCustomerStaffRelationsModel>
    {
        public EditCustomerStaffRelationsExecutor(EditCustomerStaffRelationsModel model) : base(model, null)
        {
            
        }
        /// <summary>
        /// 新增客户与员工归属关系数据
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            
            foreach (var cusStaff in Model.CustomerStaffRelations)
            {
                cusStaff.FillCreator();
                CustomerStaffRelationAdapter.Instance.UpdateInContext(cusStaff);
            }
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

    }
}