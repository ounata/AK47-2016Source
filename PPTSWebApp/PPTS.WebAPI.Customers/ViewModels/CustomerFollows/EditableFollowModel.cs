using System.Collections.Generic;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    public class EditableFollowModel
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

        public EditableFollowModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }
}