using MCS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class TeacherCourseController : ApiController
    {
        [HttpPost]
        public AssignQCR GetTchCourse(AssignQCM criteriaQCM)
        {


            return null;

        }
    }
}