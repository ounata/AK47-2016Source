using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class SchoolAdapter : CustomerAdapterBase<School, SchoolCollection>
    {
        public static readonly SchoolAdapter Instance = new SchoolAdapter();

        private SchoolAdapter()
        {
        }

        /// <summary>
        /// 获取在读学校信息
        /// </summary>
        /// <param name="schoolID">在读学校ID</param>
        /// <returns></returns>
        public School Load(string schoolID)
        {
            return this.Load(builder => builder.AppendItem("SchoolID", schoolID)).SingleOrDefault();
        }
    }
}