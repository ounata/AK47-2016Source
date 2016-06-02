using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerTeacherAssignApplyAdapter : UpdatableAndLoadableAdapterBase<CustomerTeacherAssignApply, CustomerTeacherAssignApplyCollection>
    {
        public static readonly CustomerTeacherAssignApplyAdapter Instance = new CustomerTeacherAssignApplyAdapter();

        private CustomerTeacherAssignApplyAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
