using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.ServiceFees
{
    public class ExpensesQueryResult
    {
        //}
        public PagedQueryResult<ExpensesQueryModel, ExpensesQueryCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}