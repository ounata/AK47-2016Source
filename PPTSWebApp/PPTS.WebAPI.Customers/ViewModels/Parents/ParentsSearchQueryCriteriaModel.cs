using MCS.Library.Data;
using MCS.Library.Data.Mapping;

namespace PPTS.WebAPI.Customers.ViewModels.Parents
{
    public class ParentsSearchQueryCriteriaModel
    {
        [NoMapping]
        public string CustomerID { get; set; }

        [NoMapping]
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