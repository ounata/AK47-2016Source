using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerServiceDataSource : GenericCustomerDataSource<CustomerServiceModel, CustomerServiceModelCollection>
    {
        public static readonly new CustomerServiceDataSource Instance = new CustomerServiceDataSource();

        private CustomerServiceDataSource()
        {

        }

        public PagedQueryResult<CustomerServiceModel, CustomerServiceModelCollection> LoadCustomerService(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " cs.[CustomerID],cs.[ServiceID],pc.[CustomerName] CustomerName ,p.ParentName ParentName,pc.grade,cs.AcceptTime,";
            select += "cs.serviceType,cs.accepterName,cs.serviceStatus,cs.handlerName,";
            select += " cs.complaintTimes,'校区反馈' SchoolMemo,cs.isUpgradeHandle,cs.VoiceID,'录音状态' VoiceStatus ";
            string from = @" [CustomerServices] cs inner join [PotentialCustomers] pc 
                              on cs.CustomerID = pc.CustomerID
                              inner join[CustomerParentRelations] csr on cs.CustomerID = csr.CustomerID
                              inner join[Parents] p on csr.ParentID = p.ParentID";

            PagedQueryResult<CustomerServiceModel, CustomerServiceModelCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}