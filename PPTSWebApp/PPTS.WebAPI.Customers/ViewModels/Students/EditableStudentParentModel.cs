using System.Collections.Generic;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    public class EditableStudentParentModel
    {
        public StudentModel Customer { get; set; }

        public ParentModel Parent { get; set; }

        public CustomerParentRelation CustomerParentRelation { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public EditableStudentParentModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }
    
}