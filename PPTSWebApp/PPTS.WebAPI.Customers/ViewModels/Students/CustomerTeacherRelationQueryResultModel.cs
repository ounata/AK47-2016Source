using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    public class CustomerTeacherRelationQueryResultModel
    {
        public CustomerTeacherRelationCollection CustomerTeachers { get; set; }


        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}