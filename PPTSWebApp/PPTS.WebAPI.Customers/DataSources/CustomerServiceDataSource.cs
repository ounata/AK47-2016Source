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
            string select = " pc.OrgName,pc.CampusName,cs.CustomerID,cs.ServiceID,pc.CustomerName CustomerName ,p.ParentName ParentName,pc.grade,cs.AcceptTime,";
            select += "cs.ServiceType,cs.AccepterName,cs.ServiceStatus,cs.HandlerName,";
            select += " cs.ComplaintTimes,cs.ComplaintLevel,'校区反馈' SchoolMemo,cs.IsUpgradeHandle,cs.VoiceID,'录音状态' VoiceStatus ";
            string from = @" CM.CustomerServices cs left join CM.PotentialCustomers_Current pc 
                              on cs.CustomerID = pc.CustomerID
                              left join CM.CustomerParentRelations_Current csr on cs.CustomerID = csr.CustomerID
                              left join CM.Parents_Current p on csr.ParentID = p.ParentID";

            PagedQueryResult<CustomerServiceModel, CustomerServiceModelCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}