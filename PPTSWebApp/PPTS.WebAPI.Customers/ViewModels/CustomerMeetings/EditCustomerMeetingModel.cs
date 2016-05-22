using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Customers.Entities;
using MCS.Library.Validation;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerMeetings
{
    /// <summary>
    /// 新增/编辑会议Model
    /// </summary>
  
    public class EditCustomerMeetingModel 
    {
        [ObjectValidator]
        public CustomerMeeting CustomerMeeting { set; get; }

        private IList<CustomerMeetingItem> cmItem = new List<CustomerMeetingItem>();
        public IList<CustomerMeetingItem> Items
        {
            set { cmItem = value; }
            get { return cmItem; }
        }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}