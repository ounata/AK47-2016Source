using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Adapters;
using PPTS.WebAPI.Order.ViewModels.CustomerSearchs;
using PPTS.Data.Common.Adapters;

namespace PPTS.WebAPI.Order.Controllers
{
    public class StudentAssignmentController : ApiController
    {
        //[HttpGet]
        //public string GetAllStudentAssignment()
        //{
        //    CustomerSearchQueryCriteriaModel criteria = new CustomerSearchQueryCriteriaModel();
        //    criteria.PageParams = new PageRequestParams();
        //    criteria.PageParams.PageIndex = 1;
        //    criteria.PageParams.PageSize = 10;

        //    OrderByRequestItem item = new OrderByRequestItem();
        //    item.DataField = "CustomerCode";
        //    item.SortDirection = FieldSortDirection.Ascending;

        //    criteria.OrderBy = new OrderByRequestItem[] { item };

        //    CustomerSearchQueryResult result = new CustomerSearchQueryResult
        //    {
        //        QueryResult = GenericSearchDataSource<CustomerSearch, CustomerSearchCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
        //        //Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerSearch), typeof(Parent))
        //    };
        //    var r = result.QueryResult.PagedData;
        //    string str = string.Empty;
        //    foreach (var v in r)
        //    {
        //        str += v.CustomerName + ";";
        //    }

        //    return string.Format("这是测试例子,{0},{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), str);
        //}

        [HttpPost]
        public CustomerSearchQueryResult GetAllStudentAssignment(CustomerSearchQueryCriteriaModel criteria)
        {
            CustomerSearchQueryResult result = new CustomerSearchQueryResult
            {
                QueryResult = GenericSearchDataSource<CustomerSearch, CustomerSearchCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerSearch))
            };

            return result;
        }
        [HttpPost]
        public PagedQueryResult<CustomerSearch, CustomerSearchCollection> GetPagedStudentAssignment(CustomerSearchQueryCriteriaModel criteria)
        {
            return GenericSearchDataSource<CustomerSearch, CustomerSearchCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }


    }
}
