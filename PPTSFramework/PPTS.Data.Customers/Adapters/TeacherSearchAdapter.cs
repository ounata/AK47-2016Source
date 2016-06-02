using PPTS.Data.Customers.Entities;
using System.Linq;

namespace PPTS.Data.Customers.Adapters
{
    public class TeacherSearchAdapter : SearchAdapterBase<TeacherSearch, TeacherSearchCollection>
    {
        public static readonly TeacherSearchAdapter Instance = new TeacherSearchAdapter();

        private TeacherSearchAdapter()
        {
        }

        public TeacherSearchCollection Load(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID));
        }

        public TeacherSearchCollection Load(string customerID,string teacherID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID).AppendItem("TeacherID", teacherID));
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSSearchConnectionName;
        }
    }
}
