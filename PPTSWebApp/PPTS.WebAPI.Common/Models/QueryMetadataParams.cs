namespace PPTS.WebAPI.Common.Models
{
    public class QueryMetadataParams
    {
        public QueryMetadataParams()
        {
        }

        public QueryMetadataParams(string parentKey, string category)
        {
            this.ParentKey = parentKey;
            this.Category = category;
        }

        public string ParentKey
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }
    }
}