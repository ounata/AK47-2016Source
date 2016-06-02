using PPTS.Data.Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Authorization
{
    public class ScopeAuthorization<T> : ScopeAuthorizationBase<T>
    {
        public static readonly ScopeAuthorization<T> Instance = new ScopeAuthorization<T>();

        public override CourseOrgAuthorizationAdapterBase GetCourseOrgAuthorizationAdapter()
        {
            throw new NotImplementedException();
        }

        public override CustomerOrgAuthorizationAdapterBase GetCustomerOrgAuthorizationAdapter()
        {
            return CustomerOrgAuthorizationAdapter.Instance;
        }

        public override CustomerRelationAuthorizationAdaperBase GetCustomerRelationAuthorizationAdaper()
        {
            return CustomerRelationAuthorizationAdaper.Instance;
        }

        public override OwnerRelationAuthorizationAdapterBase GetOwnerRelationAuthorizationAdapter()
        {
            return OwnerRelationAuthorizationAdapter.Instance;
        }

        public override RecordOrgAuthorizationAdapterBase GetRecordOrgAuthorizationAdapter()
        {
            return RecordOrgAuthorizationAdapter.Instance;
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
