using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("增加跟进辅导机构信息记录")]
    public class AddCustomerFollowItemExecutor : PPTSEditCustomerExecutorBase<List<CustomerFollowItem>>
    {
        public AddCustomerFollowItemExecutor(List<CustomerFollowItem> ItemModelList)
            : base(ItemModelList, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            if (this.Model != null && this.Model.Count > 0)
            {
                foreach (CustomerFollowItem item in this.Model)
                {
                    CustomerFollowItemAdapter.Instance.UpdateInContext(item);
                }
            }
        }
    }
}
