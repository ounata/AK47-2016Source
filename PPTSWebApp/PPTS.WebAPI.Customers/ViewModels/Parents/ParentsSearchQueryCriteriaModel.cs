using MCS.Library.Data;
using MCS.Library.Data.Mapping;

namespace PPTS.WebAPI.Customers.ViewModels.Parents
{
    public class ParentsSearchQueryCriteriaModel
    {
        [ConditionMapping("", Template = "CONTAINS(b.*, ${Data}$)")]
        public string Keyword { get; set; }

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