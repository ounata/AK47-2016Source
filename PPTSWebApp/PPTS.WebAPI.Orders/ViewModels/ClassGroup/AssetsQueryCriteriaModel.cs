using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    /// <summary>
    /// 资产视图查询请求模型
    /// </summary>
    public class AssetsQueryCriteriaModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [ConditionMapping("ProductID")]
        public string ProductID
        {
            get;
            set;
        }

        [ConditionMapping(fieldName: "UnAssignAmount", op: ">")]
        public string UnAssignAmount { get { return "0"; } }

        [NoMapping]
        public PageRequestParams PageParams
        {
            get;
            set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get;
            set;
        }
    }
}