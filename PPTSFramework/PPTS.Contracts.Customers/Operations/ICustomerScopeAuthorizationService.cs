using PPTS.Contracts.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Operations
{
    [ServiceContract]
    public interface ICustomerScopeAuthorizationService
    {
        [OperationContract]
        void CustomerOrgAuthorizationToOrder(CustomerScopeAuthorizationModel model);

        [OperationContract]
        void CustomerRelationAuthorizationToOrder(CustomerScopeAuthorizationModel model);

        [OperationContract]
        void CustomerOrgAuthorizationToSearch(CustomerScopeAuthorizationModel model);

        [OperationContract]
        void CustomerRelationAuthorizationToSearch(CustomerScopeAuthorizationModel model);

        [OperationContract]
        void OwnerRelationAuthorizationToSearch(CustomerScopeAuthorizationModel model);

        [OperationContract]
        void RecordOrgAuthorizationToSearch(CustomerScopeAuthorizationModel model);


    }
}
