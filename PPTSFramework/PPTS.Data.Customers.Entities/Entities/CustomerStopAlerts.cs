using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 停课休学实体类
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerStopAlerts")]
    [DataContract]
    public class CustomerStopAlerts
    {
        /// <summary>
        /// 停课休学ID
        /// </summary>
        [ORFieldMapping("AlertID", PrimaryKey = true)]
        [DataMember]
        public string AlertID
        {
            get;
            set;
        }

        /// <summary>
        /// 停课休学时间
        /// </summary>
        [ORFieldMapping("AlertTime")]
        [DataMember]
        public DateTime AlertTime
        {
            get;
            set;
        }

        /// <summary>
        /// 停课休学类型
        /// </summary>
        [ORFieldMapping("AlertType")]
        [DataMember]
        public StopAlertType AlertType
        {
            get;
            set;
        }

        /// <summary>
        /// 停课休学原因类型
        /// </summary>
        [ORFieldMapping("AlertReason")]
        [DataMember]
        public string AlertReason
        {
            get;
            set;
        }

        /// <summary>
        /// 停课休学原因
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
    }

    [Serializable]
    [DataContract]
    public class CustomerStopAlertsCollection : EditableDataObjectCollectionBase<CustomerStopAlerts>
    {

    }
}
