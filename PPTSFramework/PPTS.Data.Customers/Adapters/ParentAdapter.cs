using System.Collections.Generic;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class ParentAdapter : GenericParentAdapter<Parent, ParentCollection>
    {
        public new static readonly ParentAdapter Instance = new ParentAdapter();

        private ParentAdapter()
        {
        }

        public List<string> LoadAddress(string searchTerm, int maxCount)
        {
            List<string> list = new List<string>();
            
            ParentCollection parents = this.QueryData(this.GetMappingInfo(),
                string.Format(@" SELECT DISTINCT TOP {0} AddressDetail FROM CM.Parents WHERE AddressDetail LIKE N'%{1}%' ORDER BY AddressDetail ASC", maxCount, searchTerm));

            if (parents != null && parents.Count > 0)
            {
                parents.ForEach(p =>
                {
                    if (!list.Contains(p.AddressDetail))
                    {
                        list.Add(p.AddressDetail);
                    }
                });
            }
            return list;
        }
    }
}