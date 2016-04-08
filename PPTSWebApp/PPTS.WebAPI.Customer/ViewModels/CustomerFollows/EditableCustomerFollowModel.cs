using System.Collections.Generic;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customer.ViewModels.CustomerFollows
{
    public class EditableCustomerFollowModel
    {
        public CustomerFollow Follow
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public EditableCustomerFollowModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }
}