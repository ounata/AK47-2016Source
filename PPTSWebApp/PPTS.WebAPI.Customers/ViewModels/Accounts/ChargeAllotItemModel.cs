using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MCS.Library.Data.DataObjects;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 业绩分配模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class ChargeAllotItemModel : AccountChargeAllot
    {

    }
}