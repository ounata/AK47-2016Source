using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a CustomerExpenseRelation.
    /// 学员服务费扣除关系表（如果返还，该记录删除）
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerExpenseRelations")]
    [DataContract]
    public class CustomerExpenseRelation : IEntityWithCreator
    {
        public CustomerExpenseRelation()
        {
        }
        
        /// <summary>
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID", PrimaryKey = true)]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 费用ID
        /// </summary>
        [ORFieldMapping("ExpenseID", PrimaryKey = true)]
        [DataMember]
        public string ExpenseID
        {
            get;
            set;
        }

        /// <summary>
        /// 费用类型
        /// </summary>
        [ORFieldMapping("ExpenseType")]
        [DataMember]
        public string ExpenseType
        {
            get;
            set;
        }

        /// <summary>
        /// 费用金额
        /// </summary>
        [ORFieldMapping("ExpenseMoney")]
        [DataMember]
        public decimal ExpenseMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 当前扣除的账号
        /// </summary>
        [ORFieldMapping("AccountID")]
        [DataMember]
        public string AccountID
        {
            get;
            set;
        }

        /// <summary>
        /// 关联订单id
        /// </summary>
        [ORFieldMapping("OrderID")]
        [DataMember]
        public string OrderID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime")]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class CustomerExpenseRelationCollection : EditableDataObjectCollectionBase<CustomerExpenseRelation>
    {
    }
}