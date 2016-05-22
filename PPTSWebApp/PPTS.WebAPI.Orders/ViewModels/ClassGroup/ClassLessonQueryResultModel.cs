using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class ClassLessonQueryResultModel
    {
        public PagedQueryResult<ClassLessonModel, ClassLessonModelCollection> QueryResult { get; set; }

       
    }
}