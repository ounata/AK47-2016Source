using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;

namespace PPTS.Data.Orders.Adapters
{
    public class ClassLessonItemsAdapter : ClassGroupAdapterBase<ClassLessonItem, ClassLessonItemCollection>
    {
        public static readonly ClassLessonItemsAdapter Instance = new ClassLessonItemsAdapter();

        private ClassLessonItemsAdapter()
        {
        }

        public ClassLessonItemCollection LoadCollection(IList<ClassLesson> clc)
        {           
            string lessonIDs = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var v in clc)
                sb.AppendFormat(",'{0}'", v.LessonID);
            if (sb.Length == 0)
                return null;
            lessonIDs = string.Format("({0})", sb.ToString().Substring(1));
            return this.Load(builder => builder.AppendItem("LessonID", lessonIDs, "in", true));            
        }

        public ClassLessonItemCollection LoadStudentCountCollection(string classID) {           

            //将要上课的那一节课的学生人数
            string sql =string.Format( @" select * 
                            from [OM].[ClassLessonItems] where AssignStatus = 1 and LessonID = (
                            select top 1 LessonID
                            from [OM].[ClassLessons]
                            where ClassID = '{0}' and LessonStatus = 1 
                            order by StartTime desc)",classID);

            return this.QueryData(sql);
        }

        public void UpdateCollectionInContext(ClassLessonItemCollection collection) {
            collection.IsNotNull(c => {
                c.ForEach(item => { UpdateInContext(item); });
            });
        }

    }
}
