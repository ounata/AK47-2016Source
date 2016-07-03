using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Presents
{
    public class PresentWorkflowCriteriaModel
    {
        public string ProcessID { get; set; }

        public string ActivityID { get; set; }

        public string ResourceID { get; set; }
    }
}