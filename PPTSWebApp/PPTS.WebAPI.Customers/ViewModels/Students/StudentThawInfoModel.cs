using MCS.Web.MVC.Library.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    public class StudentThawInfoModel
    {
        public StudentThawModel Thaw { get; set; }

        public WfClientProcess ClientProcess { get; set; }
    }
}
