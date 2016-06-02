using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Mapping;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Customers.Operations;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PPTS.Services.Customers.Services
{
    public class CustomerUpdateService : ICustomerUpdateService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void DeductExpenses(List<CustomerExpenseRelation> expenses)
        {
            AccountDeductAppliesModel model = new AccountDeductAppliesModel(expenses);
            model.Prepare();
            new AccountDeductAppliesExecutor(model).Execute();
        }
    }
}
