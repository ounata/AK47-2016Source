using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.RefundAlerts
{
    public class RefundAlertEditModel
    {
        [ObjectValidator]
        public CustomerRefundAlerts RefundAlert
        {
            get;
            set;
        }

        public bool IsEditor
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
        public RefundAlertEditModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public static RefundAlertEditModel Load(string id)
        {
            RefundAlertEditModel result = new RefundAlertEditModel();
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerRefundAlerts),typeof(RefundAlertEditModel));
            result.RefundAlert = CustomerRefundAlertAdapter.Instance.Load(id);

            return result;
        }
    }
}
