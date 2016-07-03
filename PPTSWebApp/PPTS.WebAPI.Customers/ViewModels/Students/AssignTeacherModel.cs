using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    public class AssignTeacherModel
    {
        public CustomerTeacherRelationCollection ctrc { get; set; }

        public int[] sendEmailSMS { get; set; }
    }
}