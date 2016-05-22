using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
