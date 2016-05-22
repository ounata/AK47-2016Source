using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using PPTS.Data.Common.Adapters;
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
    public class StopAlertCreateModel
    {
        [ObjectValidator]
        public CustomerStopAlerts StopAlert
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        /// <summary>
        /// 退费预警类型
        /// </summary>
        [NoMapping]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_StopAlertType")]
        public int StopAlertType
        {
            get;
            set;
        }

        /// <summary>
        /// 停课休学原因类型
        /// </summary>
        [NoMapping]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_StopAlertReason")]
        public int StopAlertReason
        {
            get;
            set;
        }


        public static StopAlertCreateModel CreateStopAlert()
        {
            StopAlertCreateModel model = new StopAlertCreateModel();

            model.StopAlert = new CustomerStopAlerts { AlertID = UuidHelper.NewUuidString(), AlertTime = DateTime.Now };
            model.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerStopAlerts), typeof(StopAlertCreateModel));
            return model;
        }
    }
}
