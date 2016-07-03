using MCS.Web.MVC.Library.Models;
using MCS.Web.MVC.Library.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    [Serializable]
    public class StudentThawQueryCriteriaModel : IWfClientSearchParameters
    {
        public string ProcessID { get; set; }
        public string ActivityID { get; set; }
        public string ResourceID { get; set; }
    }
}
