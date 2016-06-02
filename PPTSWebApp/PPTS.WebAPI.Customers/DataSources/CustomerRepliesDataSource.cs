using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerRepliesDataSource : GenericCustomerDataSource<CustomerRepliesQueryModel, CustomerRepliesQueryCollection>
    {
        public static readonly new CustomerRepliesDataSource Instance = new CustomerRepliesDataSource();
        private CustomerRepliesDataSource() { }


        /// <summary>
        /// 学大反馈分页查询
        /// </summary>
        /// <param name="prp">分页参数</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderByBuilder">排序条件</param>
        /// <returns></returns>
        public PagedQueryResult<CustomerRepliesQueryModel, CustomerRepliesQueryCollection> GetCustomerRepliesList(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
           
            string select = "*";
            string from = @"(SELECT   cReplies.ReplyID,cReplies.CampusID, cReplies.ReplyTime,
                            customer.CustomerId,cReplies.ReplyContent,cReplies.Poster,
                            cReplies.ParentName,cReplies.ReplyObject,cReplies.ReplierName,
                            (
							case 
							when cReplies.ReplyObject=6 and cReplies.Poster=1 Then 6--周反馈
							when cReplies.ReplyObject<>6 and cReplies.Poster=1 Then 3--对家长回复
							when  cReplies.Poster=2 Then 7--对家长回复
							ELSE 0 END)ReplyType,
                            (CASE when  cReplies.Poster=1 Then 2--学管师
							ELSE cReplies.ReplyObject END)ReplyObject1,
                            customer.CustomerCode,customer.CustomerName,customer.CampusName,
                            customer.Grade  
            FROM CM.CustomerReplies cReplies LEFT JOIN CM.Customers_Current customer on cReplies.CustomerId=customer.CustomerId)Z";
            PagedQueryResult<CustomerRepliesQueryModel, CustomerRepliesQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}