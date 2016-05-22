using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.RefundAlerts
{
    [Serializable]
    public class RefundAlertQueryModel : CustomerRefundAlerts
    {

    }

    public class RefundAlertQueryCollection : EditableDataObjectCollectionBase<RefundAlertQueryModel>
    {
        public RefundAlertQueryCollection() { }
    }
}
