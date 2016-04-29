using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Runtime.Serialization;

namespace PPTS.Data.Products.Entities
{
    /// <summary>
    /// 薪酬规则
    /// </summary>
    [Serializable]
    [ORTableMapping("PM.ProductSalaryRules")]
    [DataContract]
    public class ProductSalaryRule
    {
        /// <summary>
        /// 薪酬规则ID
        /// </summary>
        [ORFieldMapping("RuleID")]
        [DataMember]
        public string RuleID { set; get; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [ORFieldMapping("ProductID")]
        [DataMember]
        public string ProductID { set; get; }

        /// <summary>
        /// 规则对象枚举（咨询师，学管师，教师）
        /// </summary>
        [ORFieldMapping("RuleObject")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_BO_FeedBack_Type")]
        public RuleObject RuleObject { set; get; }

        /// <summary>
        /// 课酬金额每小时
        /// </summary>
        [ORFieldMapping("MoneyPerHour")]
        [DataMember]
        public string MoneyPerHour { set; get; }

        /// <summary>
        /// 课酬金额每课时
        /// </summary>
        [ORFieldMapping("MoneyPerPeriod")]
        [DataMember]
        public string MoneyPerPeriod { set; get; }
    }

    [Serializable]
    [DataContract]
    public class ProductSalaryRuleCollection : EditableDataObjectCollectionBase<ProductSalaryRule>
    {
    }

}
