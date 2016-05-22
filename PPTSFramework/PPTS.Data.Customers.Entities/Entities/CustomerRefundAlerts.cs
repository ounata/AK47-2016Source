using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 退费预警实体类
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerRefundAlerts")]
    [DataContract]
    public class CustomerRefundAlerts
    {
        /// <summary>
        /// 预警ID
        /// </summary>
        [ORFieldMapping("AlertID", PrimaryKey = true)]
        [DataMember]
        public string AlertID
        {
            get;
            set;
        }

        /// <summary>
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 退费预警时间
        /// </summary>
        [ORFieldMapping("AlertTime")]
        [DataMember]
        public DateTime AlertTime
        {
            get;
            set;
        }

        /// <summary>
        /// 退费预警类型
        /// </summary>
        [ORFieldMapping("AlertStatus")]
        [DataMember]
        public RefundAlertStatus AlertStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 退费预警原因类型
        /// </summary>
        [ORFieldMapping("AlertReason")]
        [DataMember]
        public string AlertReason
        {
            get;
            set;
        }

        /// <summary>
        /// 退费预警原因
        /// </summary>
        [ORFieldMapping("AlertReasonName")]
        [DataMember]
        public string AlertReasonName
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人ID
        /// </summary>
        [ORFieldMapping("OperatorID")]
        [DataMember]
        public string OperatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人
        /// </summary>
        [ORFieldMapping("OperatorName")]
        [DataMember]
        public string OperatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人岗位ID
        /// </summary>
        [ORFieldMapping("OperatorJobID")]
        [DataMember]
        public string OperatorJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人岗位名称
        /// </summary>
        [ORFieldMapping("OperatorJobName")]
        [DataMember]
        public string OperatorJobName
        {
            get;
            set;
        }
    }
    [Serializable]
    [DataContract]
    public class CustomerRefundAlertsCollection : EditableDataObjectCollectionBase<CustomerRefundAlerts>
    {

    }
}
