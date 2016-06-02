using PPTS.Data.Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Authorization
{
    public class CustomerOrgAuthorizationAdapter: CustomerOrgAuthorizationAdapterBase
    {
        public static readonly CustomerOrgAuthorizationAdapter Instance = new CustomerOrgAuthorizationAdapter();

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
