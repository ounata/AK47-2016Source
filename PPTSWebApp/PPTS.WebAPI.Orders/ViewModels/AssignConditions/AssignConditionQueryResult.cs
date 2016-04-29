using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Orders.ViewModels.AssignConditions
{
    public class AssignConditionQueryResult
    {     
        /// <summary>
        /// 排课条件列表
        /// 已经有的排课条件
        /// </summary>
        public AssignConditionCollection AssignCondition
        {
            get;
            set;
        }

        /// <summary>
        /// 资产列表
        /// 还有剩余课时
        /// </summary>
        //public IList<AssignSuper> AssignExtension
        //{
        //    get;
        //    set;
        //}

        public OrderItemViewCollection AssignExtension
        {
            get;
            set;
        }

        public IList<TeacherModel> Teacher
        {
            get;
            set;
        }

        //public AssignSuper Assign { get; set; }
        public OrderItemView Assign { get; set; }


        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }


        public AssignConditionQueryResult()
        {
            Assign = new OrderItemView();
        }
    }
}