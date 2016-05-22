using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    /// <summary>
    /// 课程详情-班级-模型
    /// </summary>
    public class ClassDetailModel
    {
        public ClassModel Class { get; set; }
        public ClassLessonQueryResultModel ClassLessones { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

    }

    public class ClassModel : Class    {    }

    public class ClassModelCollection : EditableDataObjectCollectionBase<ClassModel> { }

    /// <summary>
    /// 课程详情-课程-模型
    /// </summary>
    public class ClassLessonModel : ClassLesson {

    }

    /// <summary>
    /// 课程详情-课程-模型-集合
    /// </summary>
    public class ClassLessonModelCollection : EditableDataObjectCollectionBase<ClassLessonModel>
    {
    }
}