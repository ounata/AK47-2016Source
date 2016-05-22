using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class ClassCustomerQueryCriteriaModel
    {
        [NoMapping]
        public string ClassID { get; set; }

        [InConditionMapping("LessonID")]
        public string[] LessonIDs { get; set; }

        [ConditionMapping(fieldName: "AssignStatus", op: "!=")]
        public AssignStatusDefine AssignStatus = AssignStatusDefine.Invalid;

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