using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Products.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Expense.
    /// 服务费用表
    /// </summary>
    [Serializable]
    [ORTableMapping("[PM].[Expenses]")]
    [DataContract]
    #region 数据范围权限(存入识别)
    [EntityAuth(RecordType = RecordType.Product)]
    #endregion 

    #region 数据范围权限(数据读取权限)
    [OwnerRelationScope(Name = "综合服务费管理-本分公司", Functions = "综合服务费管理-本分公司", RecordType = RecordType.Customer)]
    [OwnerRelationScope(Name = "综合服务费管理-全国", Functions = "综合服务费管理-全国", RecordType = RecordType.Customer)]
   
    #endregion

    #region 数据范围权限(编辑操作权限)
    [OwnerRelationScope(Name = "新增/编辑/删除综合服务费-本分公司", Functions = "新增/编辑/删除综合服务费-本分公司", ActionType = ActionType.Edit, RecordType = RecordType.Customer)]
    [OwnerRelationScope(Name = "新增/编辑/删除-全国", Functions = "新增/编辑/删除-全国", ActionType = ActionType.Edit, RecordType = RecordType.Customer)]
    #endregion
    public class Expense
    {
        public Expense()
        {
        }

        /// <summary>
        /// 服务费ID
        /// </summary>
        [ORFieldMapping("ExpenseID", PrimaryKey = true)]
        [DataMember]
        public string ExpenseID
        {
            get;
            set;
        }
        
        /// <summary>
        /// 服务费类型（一对一，班组）
        /// </summary>
        [ORFieldMapping("ExpenseType")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_ServiceFee_ServiceType")]
        public string ExpenseType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("ExpenseValue")]
        [DataMember]
        public decimal ExpenseValue
        {
            get;
            set;
        }
        
        /// <summary>
        /// 分工司ID
        /// </summary>
        [ORFieldMapping("BranchID")]
        [DataMember]
        public string BranchID
        {
            get;
            set;
        }

        /// <summary>
        /// 分公司名称
        /// </summary>
        [ORFieldMapping("BranchName")]
        [DataMember]
        public string BranchName
        {
            get;
            set;
        }

        /// <summary>
        /// 校区ID列表（逗号分割）
        /// </summary>
        [ORFieldMapping("CampusIDs")]
        [DataMember]
        public string CampusIDs
        {
            get;
            set;
        }

        /// <summary>
        /// 校区名称列表（逗号分割）
        /// </summary>
        [ORFieldMapping("CampusNames")]
        [DataMember]
        public string CampusNames
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
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [ORFieldMapping("ModifierID")]
        [DataMember]
        public string ModifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [DataMember]
        public string ModifierName
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class ExpenseCollection : EditableDataObjectCollectionBase<Expense>
    {
    }
}