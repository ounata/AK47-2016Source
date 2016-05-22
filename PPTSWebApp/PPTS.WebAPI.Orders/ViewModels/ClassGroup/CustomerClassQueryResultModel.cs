using MCS.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class CustomerClassQueryResultModel
    {
        public PagedQueryResult<CustomerClassSearchModel, CustomerClassSearchModelCollection> QueryResult { get; set; }
    }
}