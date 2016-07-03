using System;
using System.Linq;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data;

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

        /// <summary>
        /// 获取在读学校信息
        /// </summary>
        /// <param name="schoolName">在读学校名称</param>
        /// <returns></returns>
        public School LoadByName(string schoolName)
        {
            return this.Load(builder => builder.AppendItem("SchoolName", schoolName)).SingleOrDefault();
        }

        public SchoolCollection LoadSchools(string searchTerm, int maxCount)
        {
            return this.QueryData(String.Format("SELECT TOP {0} * FROM CM.SCHOOLS WHERE SchoolName LIKE N'%{1}%' ORDER BY SchoolName ASC", maxCount, searchTerm));
        }
    }
}