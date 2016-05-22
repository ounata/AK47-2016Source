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

namespace PPTS.WebAPI.Customers.ViewModels.RefundAlerts
{
    public class RefundAlertCreateModel
    {
        [ObjectValidator]
        public CustomerRefundAlerts RefundAlert
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
        [ConstantCategory("C_CODE_ABBR_Customer_RefundAlertStatus")]
        public int RefundAlertStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 退费预警原因类型
        /// </summary>
        [NoMapping]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_RefundAlertReason")]
        public int RefundAlertReason
        {
            get;
            set;
        }

        public static RefundAlertCreateModel CreateRefundAlert()
        {
            RefundAlertCreateModel model = new RefundAlertCreateModel();

            model.RefundAlert = new CustomerRefundAlerts { AlertID = UuidHelper.NewUuidString(), AlertTime = DateTime.Now };
            model.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerRefundAlerts), typeof(RefundAlertCreateModel));
            return model;
        }
    }
}
