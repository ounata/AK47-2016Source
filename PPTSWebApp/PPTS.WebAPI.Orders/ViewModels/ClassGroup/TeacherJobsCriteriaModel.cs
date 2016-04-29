using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class TeacherJobsCriteriaModel
    {
        [ConditionMapping("CampusID")]
        public string CampusID { get; set; }

        [ConditionMapping("TeacherName")]
        public string TeacherName { get; set; }

        [ConditionMapping("GradeMemo")]
        public string GradeMemo { get; set; }

        [ConditionMapping("SubjectMemo")]
        public string SubjectMemo { get; set; }


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