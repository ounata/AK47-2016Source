using PPTS.Data.Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Authorization
{
    public class RecordOrgAuthorizationAdapter: RecordOrgAuthorizationAdapterBase
    {
        public static readonly RecordOrgAuthorizationAdapter Instance = new RecordOrgAuthorizationAdapter();

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
