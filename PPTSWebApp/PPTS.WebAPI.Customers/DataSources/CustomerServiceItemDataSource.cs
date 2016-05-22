using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerServiceItems;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerServiceItemDataSource : GenericCustomerDataSource<CustomerServiceItemListMosel, CustomerServiceItemListMoselCollection>
    {
        public static readonly new CustomerServiceItemDataSource Instance = new CustomerServiceItemDataSource();

        private CustomerServiceItemDataSource()
        {

        }

        public PagedQueryResult<CustomerServiceItemListMosel, CustomerServiceItemListMoselCollection> LoadCustomerServiceItems(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = @" ci.[ServiceID] ,ci.[ItemID] ,ci.[HandleTime]  ,ci.[HandleStatus] ,ci.[HandleMemo] ,ci.[HandlerID] ,ci.[HandlerName] ,ci.[HandlerJobID] ,ci.[HandlerJobName] ";
            string from = @" [PPTS_Customer_Dev].[CM].[CustomerServiceItems] ci ";

            PagedQueryResult<CustomerServiceItemListMosel, CustomerServiceItemListMoselCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }

        public PagedQueryResult<CustomerServiceItemListMosel, CustomerServiceItemListMoselCollection> LoadCustomerServiceItemsAll(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = @" ci.[ServiceID] ,ci.[ItemID] ,ci.[HandleTime]  ,ci.[HandleStatus] ,ci.[HandleMemo] ,ci.[HandlerID] ,ci.[HandlerName] ,ci.[HandlerJobID] ,ci.[HandlerJobName],c.customerID ";
            string from = @" [CustomerServiceItems] ci inner join CustomerServices c on ci.[ServiceID] = c.[ServiceID]  ";

            PagedQueryResult<CustomerServiceItemListMosel, CustomerServiceItemListMoselCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}