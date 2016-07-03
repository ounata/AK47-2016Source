using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Authorization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Customer.
    /// 学员信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.Customers", "CM.Customers_Current")]
    [DataContract]

    #region 数据范围权限(存入识别)
    [EntityAuth(RecordType = RecordType.Customer)]
    #endregion 

    #region 数据范围权限(数据读取权限)
    [OwnerRelationScope(Name = "学员管理", Functions = "学员管理", RecordType = RecordType.Customer)]
    #endregion

    #region 数据范围权限(编辑操作权限)
    [OwnerRelationScope(Name = "修改家长、学员非关键信息", Functions = "修改家长、学员非关键信息", ActionType = ActionType.Edit, RecordType = RecordType.Customer)]
    [RecordOrgScope(Name = "修改家长、学员关键信息-本分公司", Functions = "修改家长、学员关键信息-本分公司", OrgType = OrgType.Department, ActionType = ActionType.Edit, RecordType = RecordType.Customer)]
    #endregion

    public class Customer : CustomerBase
    {
        public Customer()
        {
        }

        /// <summary>
        /// 是否锁定(学员属性)
        /// </summary>
        [ORFieldMapping("Locked")]
        [DataMember]
        public bool Locked
        {
            get;
            set;
        }

        /// <summary>
        /// 锁定描述（目前毕业就冻结锁定）
        /// </summary>
        [ORFieldMapping("LockMemo")]
        [DataMember]
        public string LockMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 学员状态
        /// </summary>
        [ORFieldMapping("StudentStatus")]
        [DataMember]
        public StudentStatusDefine StudentStatus
        {
            set;
            get;
        }

        /// <summary>
        /// 是否高三毕业(学员属性)
        /// </summary>
        [ORFieldMapping("Graduated")]
        [DataMember]
        public bool Graduated
        {
            get;
            set;
        }

        /// <summary>
        /// 高三毕业年份(学员属性)
        /// </summary>
        [ORFieldMapping("GraduateYear")]
        [DataMember]
        public string GraduateYear
        {
            get;
            set;
        }

        /// <summary>
        /// 首次签约时间
        /// </summary>
        [ORFieldMapping("FirstSignTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime FirstSignTime
        {
            get;
            set;
        }

        /// <summary>
        /// 首次签约人ID(学员属性)
        /// </summary>
        [ORFieldMapping("FirstSignerID")]
        [DataMember]
        public string FirstSignerID
        {
            get;
            set;
        }

        /// <summary>
        /// 首次签约人(学员属性)
        /// </summary>
        [ORFieldMapping("FirstSignerName")]
        [DataMember]
        public string FirstSignerName
        {
            get;
            set;
        }

        /// <summary>
        /// 当前回访时间(学员属性)
        /// </summary>
        [ORFieldMapping("VisitTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime VisitTime
        {
            get;
            set;
        }

        /// <summary>
        /// 已回访次数(学员属性)
        /// </summary>
        [ORFieldMapping("VisitedCount")]
        [DataMember]
        public int VisitedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 下次回访时间(学员属性)
        /// </summary>
        [ORFieldMapping("NextVisitTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime NextVisitTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class CustomerCollection : EditableDataObjectCollectionBase<Customer>
    {
    }
}