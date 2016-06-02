using PPTS.Data.Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Authorization
{
    public class CustomerRelationAuthorizationAdaper : CustomerRelationAuthorizationAdaperBase
    {
        public static readonly CustomerRelationAuthorizationAdaper Instance = new CustomerRelationAuthorizationAdaper();

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
