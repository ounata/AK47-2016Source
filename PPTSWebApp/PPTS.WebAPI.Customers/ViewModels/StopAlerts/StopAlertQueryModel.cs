using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.StopAlerts
{
    [Serializable]
    public class StopAlertQueryModel : CustomerStopAlerts
    {

    }
    public class StopAlertQueryCollection : EditableDataObjectCollectionBase<StopAlertQueryModel>
    {
        public StopAlertQueryCollection() { }
    }
}
