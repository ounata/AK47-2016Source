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
    }
}
