using PPTS.Data.Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Authorization
{
    public class OwnerRelationAuthorizationAdapter: OwnerRelationAuthorizationAdapterBase
    {
        public static readonly OwnerRelationAuthorizationAdapter Instance = new OwnerRelationAuthorizationAdapter();

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
