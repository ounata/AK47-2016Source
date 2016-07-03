using MCS.Web.MVC.Library.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Presents
{
    public class PresentWorkflowResultModel
    {
        public WfClientProcess ClientProcess { get; set; }

        public PresentDetialModel PresentDetial { get; set; }
    }
}