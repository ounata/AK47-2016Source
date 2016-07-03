using PPTS.Contracts.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Operations
{
    [ServiceContract]
    public interface IOrderScopeAuthorizationService
    {
        [OperationContract]
        void CourseOrgAuthorizationToSearch(OrderScopeAuthorizationModel model);

        [OperationContract]
        void CourseRelationAuthorizationToSearch(OrderScopeAuthorizationModel model);

        [OperationContract]
        void OwnerRelationAuthorizationToSearch(OrderScopeAuthorizationModel model);

        [OperationContract]
        void RecordOrgAuthorizationToSearch(OrderScopeAuthorizationModel model);

    }
}
