using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerTeacherAssignApplieAdapter : UpdatableAndLoadableAdapterBase<CustomerTeacherAssignApplie, CustomerTeacherAssignApplieCollection>
    {
        public static readonly CustomerTeacherAssignApplieAdapter Instance = new CustomerTeacherAssignApplieAdapter();

        private CustomerTeacherAssignApplieAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
