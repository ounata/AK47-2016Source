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
    public class FinancialAssignIncome
    {
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
        [ORFieldMapping("CategoryTypeName")]
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
        /// 确认时价格
        /// </summary>
        [ORFieldMapping("ConfirmPrice")]
        public decimal ConfirmPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 数量（一对一是实际时间除以时长向下取0.5，班组是1）与资产保持一致
        /// </summary>
        [ORFieldMapping("Amount")]
        public decimal Amount
        {
            get;
            set;
        }

    }

    [Serializable]
    public class FinancialAssignIncomeCollection : EditableDataObjectCollectionBase<FinancialAssignIncome>
    { }
}
