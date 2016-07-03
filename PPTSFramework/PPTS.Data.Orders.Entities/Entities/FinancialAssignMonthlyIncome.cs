using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Entities
{
    [Serializable]
    [ORTableMapping("OM.FinancialAssignMonthlyIncome")]
    public class FinancialAssignMonthlyIncome
    {
        /// <summary>
        /// 对账年度
        /// </summary>
        [ORFieldMapping("CheckYear")]
        public int CheckYear
        {
            get;
            set;
        }
        /// <summary>
        /// 对账月份
        /// </summary>
        [ORFieldMapping("CheckMonth")]
        public int CheckMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 分公司ID
        /// </summary>
        [ORFieldMapping("BranchID")]
        public string BranchID
        {
            get;
            set;
        }
        /// <summary>
        /// 分公司名称
        /// </summary>
        [ORFieldMapping("BranchName")]
        public string BranchName
        {
            get;
            set;
        }
        /// <summary>
        /// 校区ID
        /// </summary>
        [ORFieldMapping("CampusID")]
        public string CampusID
        {
            get;
            set;
        }
        /// <summary>
        /// 校区名称
        /// </summary>
        [ORFieldMapping("CampusName")]
        public string CampusName
        {
            get;
            set;
        }
        /// <summary>
        /// 产品类型代码
        /// </summary>
        [ORFieldMapping("CategoryType")]
        public string CategoryType
        {
            get;
            set;
        }
        /// <summary>
        /// 产品类型名称
        /// </summary>
        [ORFieldMapping("CategoryName")]
        public string CategoryName
        {
            get;
            set;
        }
        /// <summary>
        /// 产品分类代码
        /// </summary>
        [ORFieldMapping("Catalog")]
        public string Catalog
        {
            get;
            set;
        }
        /// <summary>
        /// 产品分类名称
        /// </summary>
        [ORFieldMapping("CatalogName")]
        public string CatalogName
        {
            get;
            set;
        }
        /// <summary>
        /// 金额
        /// </summary>
        [ORFieldMapping("Amount")]
        public decimal Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 税额
        /// </summary>
        [ORFieldMapping("TaxAmount")]
        public decimal TaxAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 价税合计
        /// </summary>
        [ORFieldMapping("AllAmount")]
        public decimal AllAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 税率
        /// </summary>
        [ORFieldMapping("TaxRate")]
        public decimal TaxRate
        {
            get;
            set;
        }
        /// <summary>
        /// 是否已同步（0未同步，1已同步,2无需同步）
        /// </summary>
        [ORFieldMapping("IsSyn")]
        public string IsSyn
        {
            get;
            set;
        }
        /// <summary>
        /// 同步时间
        /// </summary>
        [ORFieldMapping("SynTime")]
        public DateTime SynTime
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where, DefaultExpression = "GETUTCDATE()")]
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        public DateTime ModifyTime
        {
            get;
            set;
        }
    }
    [Serializable]
    public class FinancialAssignMonthlyIncomeCollection : EditableDataObjectCollectionBase<FinancialAssignMonthlyIncome>
    { }
}
