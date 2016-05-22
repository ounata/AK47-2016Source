using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Customers.Operations;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSCustomerQueryServiceProxy : WfClientServiceProxyBase<ICustomerQueryService>
    {
        public static readonly PPTSCustomerQueryServiceProxy Instance = new PPTSCustomerQueryServiceProxy();

        private PPTSCustomerQueryServiceProxy()
        {
        }

        public Customer QueryCustomerByID(string customerID)
        {
            return this.SingleCall(action => action.QueryCustomerByID(customerID));
        }

        public TeacherRelationByCustomerQueryResult QueryCustomerTeacherRelationByCustomerID(string customerID)
        {
            return this.SingleCall(action => action.QueryCustomerTeacherRelationByCustomerID(customerID));
        }

        public CustomerRelationByTeacherQueryResult QueryCustomerTeacherRelationByTeacherJobID(string teacherJobID)
        {
            return this.SingleCall(action => action.QueryCustomerTeacherRelationByTeacherJobID(teacherJobID));
        }

        /// <summary>
        /// 通过学员ID集合，获得学员集合信息
        /// </summary>
        /// <param name="customerIDs">学员ID集合</param>
        /// <returns></returns>
        public CustomerCollectionQueryResult QueryCustomerCollectionByCustomerIDs(params string[] customerIDs)
        {
            return this.SingleCall(action => action.QueryCustomerCollectionByCustomerIDs(customerIDs));
        }

        /// <summary>
        /// 通过学员ID,获得综合服务费扣减情况信息
        /// </summary>
        /// <param name="customerID">学员ID</param>
        /// <returns></returns>
        public CustomerExpenseCollectionQueryResult QueryCustomerExpenseByCustomerID(string customerID)
        {
            return this.SingleCall(action => action.QueryCustomerExpenseByCustomerID(customerID));
        }

        /// <summary>
        /// 通过学员ID集合,获得综合服务费扣减情况信息
        /// </summary>
        /// <param name="customerIDs">学员ID集合</param>
        /// <returns></returns>
        //public CustomerExpenseCollectionQueryResult QueryCustomerExpenseByCustomerIDs(params string[] customerIDs)
        //{
        //    return this.SingleCall(action => action.QueryCustomerExpenseByCustomerIDs(customerIDs));
        //}

        protected override WfClientChannelFactory<ICustomerQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "customerQueryService"));

            return new WfClientChannelFactory<ICustomerQueryService>(endPoint);
        }
    }
}
