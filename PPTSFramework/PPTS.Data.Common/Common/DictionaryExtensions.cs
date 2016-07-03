using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;

namespace PPTS
{

    public static class DictionaryExtensions
    {

        public static string GetCategoryName(this Dictionary<string, IEnumerable<BaseConstantEntity>> dictionary, string category, string key)
        {
            category = category.ToLower();
            var fname = category.StartsWith("c_code") ? category : "c_code_abbr_product_" + category;
            var dv = dictionary[fname].SingleOrDefault(m => m.Key == key);
            var rv = dv == null ? null : dv.Value;
            return rv;
        }

        public static Dictionary<string, IEnumerable<BaseConstantEntity>> PrepareTypes(this Dictionary<string, IEnumerable<BaseConstantEntity>> dictionary, params Type[] types)
        {
            ConstantAdapter.Instance.GetSimpleEntitiesByCategories(types).ForEach(m => { dictionary.Add(m.Key.ToLower(), m.Value); });
            return dictionary;
        }


    }


}
