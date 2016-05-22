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
        
        public ClassCollection Load(params string[] classIds)
        {
            return this.LoadByInBuilder(new MCS.Library.Data.Adapters.InLoadingCondition(i => i.AppendItem(classIds), "ClassID"));
        }

        //public void LoadInContext(Action<ClassCollection> action, params string[] classIds)
        //{
        //    LoadByInBuilderInContext(new MCS.Library.Data.Adapters.InLoadingCondition(w => w.AppendItem(classIds), "ClassID"), action);
        //}

    }
}
