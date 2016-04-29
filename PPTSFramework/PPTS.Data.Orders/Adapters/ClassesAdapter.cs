using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
    public class ClassesAdapter:ClassGroupAdapterBase<Class, ClassCollection>
    {
        public static readonly ClassesAdapter Instance = new ClassesAdapter();

        private ClassesAdapter()
        {
        }

        public Class LoadByClassID(string classID)
        {
            return this.Load(builder => builder.AppendItem("ClassID", classID)).SingleOrDefault();
        }
    }
}
