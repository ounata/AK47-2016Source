using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerVisitDataSource : GenericCustomerDataSource<CustomerVisitModel, CustomerVisitModelCollection>
    {
        public static readonly new CustomerVisitDataSource Instance = new CustomerVisitDataSource();
        private CustomerVisitDataSource()
        {

        }

        public PagedQueryResult<CustomerVisitModel, CustomerVisitModelCollection> LoadCustomerVisit(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = @" c.CustomerName,c.CustomerCode,c.CustomerID,p.ParentName,cv.VisitID,cv.VisitTime,cv.VisitWay,cv.VisitType,cv.VisitorName,cv.Satisficing,
                               cv.VisitContent,cv.CampusName ";
            string from = @"  CM.CustomerVisits cv left join CM.Customers_Current c 
                              on cv.CustomerID = c.CustomerID
                              left join CM.CustomerParentRelations_Current cr on cv.CustomerID = cr.CustomerID
                              left join CM.Parents_Current p on cr.ParentID = p.ParentID ";

            PagedQueryResult<CustomerVisitModel, CustomerVisitModelCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }

    }
}