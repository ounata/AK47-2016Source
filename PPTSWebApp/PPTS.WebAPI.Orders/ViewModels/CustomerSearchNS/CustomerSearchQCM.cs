using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;
using PPTS.Data.Orders.Common;

namespace PPTS.WebAPI.Orders.ViewModels.CustomerSearchNS
{
    public class CustomerSearchQCM : PageParamsBase
    {
        [ConditionMapping("CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerName { get; set; }

        [ConditionMapping("CustomerCode", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerCode { get; set; }

        [ConditionMapping("SchoolName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string SchoolName { get; set; }

        [ConditionMapping("EducatorName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string EducatorName { get; set; }

        [ConditionMapping("Grade")]
        public string Grade{ get; set; }
        
        ///校区ID
        [ConditionMapping("CampusID")]
        public string CampusID { get; set; }

        ///一对一剩余课次数
        [ConditionMapping("AssetOneToOneAmount", Operation = ">", DefaultValueUsage = DefaultValueUsageType.UseDefaultValue)]
        public int AssetOneToOneAmount { get; set; }

        public CustomerSearchQCM()
        {
           this.AssetOneToOneAmount = 0;
        }


    }
}