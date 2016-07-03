using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    #region 数据范围权限(存入识别)
    [EntityAuth(RecordType = RecordType.CustomerStopAlert)]
    #endregion

    //[CustomerRelationScope()]
    //[OrgCustomerRelationScope()]

    #region 数据范围权限(数据读取权限)

    [OwnerRelationScope(Name = "学员视图-停课休学/退费预警", Functions = "学员视图-停课休学/退费预警", RecordType = RecordType.CustomerStopAlert)]
    [RecordOrgScope(Name = "学员视图-停课休学/退费预警-本部门", Functions = "学员视图-停课休学/退费预警-本部门", OrgType = Common.Authorization.OrgType.Department, RecordType = RecordType.CustomerStopAlert)]
    [RecordOrgScope(Name = "学员视图-停课休学/退费预警-本校区", Functions = "学员视图-停课休学/退费预警-本校区", OrgType = Common.Authorization.OrgType.Campus, RecordType = RecordType.CustomerStopAlert)]
    [RecordOrgScope(Name = "学员视图-停课休学/退费预警-本分公司", Functions = "学员视图-停课休学/退费预警-本分公司", OrgType = Common.Authorization.OrgType.Branch, RecordType = RecordType.CustomerStopAlert)]
    [RecordOrgScope(Name = "学员视图-停课休学/退费预警-全国", Functions = "学员视图-停课休学/退费预警-全国", OrgType = Common.Authorization.OrgType.HQ, RecordType = RecordType.CustomerStopAlert)]

    #endregion

    [OwnerRelationScope(Name = "新增停课/休学", Functions = "新增停课/休学", ActionType = ActionType.Edit, RecordType = RecordType.CustomerStopAlert)]
    [CustomerRelationScope(Name = "新增停课/休学", Functions = "新增停课/休学", ActionType = ActionType.Edit, RecordType = CustomerRecordType.Customer)]

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
        [ConstantCategory("C_CODE_ABBR_Customer_StopAlertType")]
        public int AlertType
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
