using MCS.Library.Core;
using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerMeetings
{
    public class CustomerMeetingQueryResult
    {
        /// <summary>
        /// 构造ID
        /// </summary>
        public string ResourceId
        {
            //新增提前生成Id,以供附件上传关联使用
            get { return UuidHelper.NewUuidString(); }
        }
        public string CustomerName { set; get; }
        public PagedQueryResult<CustomerMeetingQueryModel, CustomerMeetingQueryCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

    }
}