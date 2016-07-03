using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Parent.
    /// 家长关系表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.Parents", "CM.Parents_Current")]
    [DataContract]
    public class Parent : IVersionDataObjectWithoutID, IEntityWithCreator, IEntityWithModifier
    {
        public Parent()
        {
        }

        /// <summary>
        /// 家长的ID
        /// </summary>
        [ORFieldMapping("ParentID", PrimaryKey = true)]
        [DataMember]
        public string ParentID
        {
            get;
            set;
        }

        /// <summary>
        /// 家长名称
        /// </summary>
        [ORFieldMapping("ParentName")]
        [StringLengthValidator(128, MessageTemplate = "家长名称的长度不能超过128个字符")]
        [StringEmptyValidator(MessageTemplate = "家长名称不允许为空")]
        [DataMember]
        public string ParentName
        {
            get;
            set;
        }

        /// <summary>
        /// 客户ID命名规则：家长P+年份后两位+月+日+999999，学生S+年份后两位+月份+日期+999999
        /// </summary>
        [ORFieldMapping("ParentCode")]
        [DataMember]
        public string ParentCode
        {
            get;
            set;
        }

        /// <summary>
        /// 性别(C_CODE_ABBR_GENDER)。1--男，2--女
        /// </summary>
        [ORFieldMapping("Gender")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_GENDER")]
        public GenderType Gender
        {
            get;
            set;
        }

        /// <summary>
        /// 所属行业(C_CODE_ABBR_INDUSTRY)
        /// </summary>
        [ORFieldMapping("Industry")]
        [DataMember]
        [ConstantCategory("Industry")]
        public string Industry
        {
            get;
            set;
        }

        /// <summary>
        /// 从事职业(C_CODE_ABBR_OCCUPATION)
        /// </summary>
        [ORFieldMapping("Career")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_OCCUPATION")]
        public string Career
        {
            get;
            set;
        }

        /// <summary>
        /// 家庭年收入(C_CODE_ABBR_HOMEINCOME)
        /// </summary>
        [ORFieldMapping("Income")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_HOMEINCOME")]
        public int Income
        {
            get;
            set;
        }

        /// <summary>
        /// 家长生日
        /// </summary>
        [ORFieldMapping("Birthday", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// 证件类型(C_CODE_ABBR_BO_Customer_CertificateType)
        /// </summary>
        [ORFieldMapping("IDType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_BO_Customer_CertificateType")]
        public IDTypeDefine IDType
        {
            get;
            set;
        }

        /// <summary>
        /// 证件号码
        /// </summary>
        [ORFieldMapping("IDNumber")]
        [DataMember]
        public string IDNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 国家(C_CODE_ABBR_COUNTRY)。默认是中国1004115
        /// </summary>
        [ORFieldMapping("Country")]
        [DataMember]
        public string Country
        {
            get;
            set;
        }

        /// <summary>
        /// 省份(C_CODE_ABBR_LOCATION)
        /// </summary>
        [ORFieldMapping("Province")]
        [DataMember]
        public string Province
        {
            get;
            set;
        }

        /// <summary>
        /// 城市(C_CODE_ABBR_LOCATION)
        /// </summary>
        [ORFieldMapping("City")]
        [DataMember]
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// 区县(C_CODE_ABBR_LOCATION)
        /// </summary>
        [ORFieldMapping("County")]
        [DataMember]
        public string County
        {
            get;
            set;
        }

        /// <summary>
        /// 街道名称
        /// </summary>
        [ORFieldMapping("StreetName")]
        [DataMember]
        public string StreetName
        {
            get;
            set;
        }

        /// <summary>
        /// 详细地址描述
        /// </summary>
        [ORFieldMapping("AddressDetail")]
        [DataMember]
        public string AddressDetail
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件地址
        /// </summary>
        [ORFieldMapping("Email")]
        [DataMember]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者名称
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [ORFieldMapping("ModifierID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string ModifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者名称
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string ModifierName
        {
            get;
            set;
        }

        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update, DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 租户的ID
        /// </summary>
        [ORFieldMapping("TenantCode")]
        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }

        /// <summary>
        /// 版本开始时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionStartTime", PrimaryKey = true, UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 版本结束时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionEndTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionEndTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class ParentCollection : EditableDataObjectCollectionBase<Parent>
    {
    }
}