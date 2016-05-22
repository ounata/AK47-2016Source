using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class PotentialCustomerDataSource : GenericCustomerDataSource<PotentialCustomerSearchModel, PotentialCustomerSearchModelCollection>
    {
        public static readonly new PotentialCustomerDataSource Instance = new PotentialCustomerDataSource();

        private PotentialCustomerDataSource()
        {
        }

        protected override void OnAfterQuery(PotentialCustomerSearchModelCollection result)
        {
            result.ForEach(customer =>
            {
                var relations = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(customer.CustomerID);
                customer.ConsultantStaff = relations.GetStaffName(CustomerRelationType.Consultant);
                customer.Consultant = relations.GetStaff(CustomerRelationType.Consultant); 
                customer.Market = relations.GetStaff(CustomerRelationType.Market);
                customer.MarketStaff = relations.GetStaffName(CustomerRelationType.Market);
            });
        }

        public PagedQueryResult<PotentialCustomerSearchModel, PotentialCustomerSearchModelCollection> LoadPotentialCustomers(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = " pcc.*, pc.ParentName ";
            var from = @" [CM].[PotentialCustomers_Current] pcc
                          inner join [CM].[CustomerParentRelations_Current] cprc on pcc.CustomerID = cprc.CustomerID and cprc.IsPrimary = 1
                          inner join [CM].[Parents_Current] pc on cprc.ParentID = pc.ParentID
                          left join [CM].[PotentialCustomersFulltext] pcf on pcc.CustomerID = pcf.OwnerID ";
            var result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}