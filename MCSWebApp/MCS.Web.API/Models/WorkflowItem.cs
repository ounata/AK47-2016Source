using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.API.Models
{
    public class WorkflowItem
    {
        public string Title { get; set; }
        public string[] Approvers { get; set; }
    }
}