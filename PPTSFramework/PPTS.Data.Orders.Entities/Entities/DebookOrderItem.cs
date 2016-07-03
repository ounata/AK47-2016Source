using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a DebookOrderItem.
    /// 退订明细表
    /// </summary>
    [Serializable]
    [ORTableMapping("OM.DebookOrderItems")]
    [DataContract]
    public class DebookOrderItem
    {
        public DebookOrderItem()
        {
        }

        /// <summary>
        /// 退订ID
        /// </summary>
        [ORFieldMapping("DebookID")]
        [DataMember]
        public string DebookID
        {
            get;
            set;
        }

        /// <summary>
        /// 顺序号
        /// </summary>
        [ORFieldMapping("SortNo")]
        [DataMember]
        public int SortNo
        {
            get;
            set;
        }

        /// <summary>
        /// 明细ID
        /// </summary>
        [ORFieldMapping("ItemID", PrimaryKey = true)]
        [DataMember]
        [KeyFieldMapping("ItemID")]
        public string ItemID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产ID
        /// </summary>
        [ORFieldMapping("AssetID")]
        [DataMember]
        public string AssetID
        {
            get;
            set;
        }

        /// <summary>
        /// 账户ID
        /// </summary>
        [ORFieldMapping("AccountID")]
        [DataMember]
        public string AccountID
        {
            get;
            set;
        }

        /// <summary>
        /// 账户编码
        /// </summary>
        [ORFieldMapping("AccountCode")]
        [DataMember]
        public string AccountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 退订数量
        /// </summary>
        [ORFieldMapping("DebookAmount")]
        [DataMember]
        public decimal DebookAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 退订金额
        /// </summary>
        [ORFieldMapping("DebookMoney")]
        [DataMember]
        public decimal DebookMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 退订中包含赠送的数量
        /// </summary>
        [ORFieldMapping("PresentAmountOfDebook")]
        [DataMember]
        public decimal PresentAmountOfDebook
        {
            get;
            set;
        }

        /// <summary>
        /// 买赠返还金额
        /// </summary>
        [ORFieldMapping("ReturnMoney")]
        [DataMember]
        public decimal ReturnMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("TenantCode")]
        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class DebookOrderItemCollection : EditableDataObjectCollectionBase<DebookOrderItem>
    {
    }
}