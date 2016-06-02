using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.Validation;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVerifies
{
    public class CustomerVerifyResult
    {
        [ObjectValidator]
        public CustomerVerifyModel Verify
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }
        
        public static CustomerVerifyResult CreateCustomerVerify()
        {
            CustomerVerifyResult result = new CustomerVerifyResult();

            result.Verify = new CustomerVerifyModel { VerifyID = UuidHelper.NewUuidString(), CreateTime = DateTime.Now, VerifyTime = DateTime.Now };
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerVerify), typeof(CustomerVerifyResult));
            return result;
        }
    }
}
