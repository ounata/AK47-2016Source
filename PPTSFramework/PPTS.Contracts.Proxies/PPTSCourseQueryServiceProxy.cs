using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Orders.Models;
using PPTS.Contracts.Orders.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    /// <summary>
    /// 课程相关服务接口
    /// </summary>
    public class PPTSCourseQueryServiceProxy : WfClientServiceProxyBase<ICourseQueryService>
    {
        public static readonly PPTSCourseQueryServiceProxy Instance = new PPTSCourseQueryServiceProxy();
        public PPTSCourseQueryServiceProxy()
        { }

        /// <summary>
        /// 学员指定时间段内(外)是否存在有效上课记录
        /// </summary>
        /// <param name="customerID">学员ID</param>
        /// <param name="dateTime">分隔日期</param>
        /// <returns></returns>
        public HasPeriodCourseQueryResult QueryPeriodCourseByCustomerID(string customerID, DateTime dateTime)
        {
            return this.SingleCall(action => action.QueryPeriodCourseByCustomerID(customerID, dateTime));
        }

        /// <summary>
        /// 获得退费所需订购相关信息
        /// </summary>
        /// <param name="model">请求查询条件</param>
        /// <returns></returns>
        public OrderInfoForRefundQueryResult QueryOrderInfoForRefundByAccountID(OrderInfoForRefundQueryModel model)
        {
            return this.SingleCall(action => action.QueryOrderInfoForRefundByAccountID(model));
        }

        protected override WfClientChannelFactory<ICourseQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "courseQueryService"));

            return new WfClientChannelFactory<ICourseQueryService>(endPoint);
        }
    }
}
