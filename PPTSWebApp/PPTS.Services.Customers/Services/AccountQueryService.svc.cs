﻿using PPTS.Contracts.Customers.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Contracts.Customers.Models;
using MCS.Library.WcfExtensions;
using System.ServiceModel.Web;
using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Adapters;

namespace PPTS.Services.Customers.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“AccountQueryService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 AccountQueryService.svc 或 AccountQueryService.svc.cs，然后开始调试。
    public class AccountQueryService : IAccountQueryService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public AccountCollectionQueryResult QueryAccountCollectionByCustomerID(string customerID)
        {
            AccountCollectionQueryResult queryResult = new AccountCollectionQueryResult();
            WhereLoadingCondition whereCondition = new WhereLoadingCondition(builder => builder.AppendItem("CustomerID", customerID).AppendItem("AccountMoney", 0, ">"));
            queryResult.AccountCollection = AccountAdapter.Instance.Load(whereCondition, DateTime.MinValue).ToList();
            return queryResult;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        public AccountChargeCollectionQueryResult QueryAccountChargeCollectionByCustomerID(string customerID)
        {
            AccountChargeCollectionQueryResult queryResult = new AccountChargeCollectionQueryResult();
            queryResult.AccountChargeCollection = AccountChargeApplyAdapter.Instance.LoadValidChargeByCustomerID(customerID).ToList();
            return queryResult;
        }
    }
}
