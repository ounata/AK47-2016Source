using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.StopAlerts
{
    [Serializable]
    public class StopAlertQueryModel : CustomerStopAlerts
    {
        ///// <summary>
        ///// 停课休学类型
        ///// </summary>
        //[ORFieldMapping("AlertType")]
        //[DataMember]
        //[ConstantCategory("C_CODE_ABBR_Customer_StopAlertType")]
        //public int AlertType
        //{
        //    get;
        //    set;
        //}
    }
    public class StopAlertQueryCollection : EditableDataObjectCollectionBase<StopAlertQueryModel>
    {
        public StopAlertQueryCollection() { }
    }
}
