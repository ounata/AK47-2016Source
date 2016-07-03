using MCS.Web.MVC.Library.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Discounts
{
    public class DiscountWorkflowResultModel
    {
        public WfClientProcess ClientProcess { get; set; }

        public DiscountDetialModel DiscountDetial { get; set; }
    }
}