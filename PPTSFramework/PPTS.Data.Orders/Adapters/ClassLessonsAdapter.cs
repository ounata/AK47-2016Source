using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;

namespace PPTS.Data.Orders.Adapters
{
    public class ClassLessonsAdapter : ClassGroupAdapterBase<ClassLesson, ClassLessonCollection>
    {
        public static readonly ClassLessonsAdapter Instance = new ClassLessonsAdapter();

        private ClassLessonsAdapter()
        {
        }

        /// <summary>
        /// 获取 未结账课时
        /// </summary>
        /// <param name="classIds"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Dictionary<string,string> GetNoOpenAccountLessonCount(string[] classIds,DateTime date)
        {
            var result = new Dictionary<string, string>();

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder(LogicOperatorDefine.And);
            builder.AppendItem("EndTime", date, ">");
            var whereSql = builder.ToSqlString(TSqlBuilder.Instance);
            var inSql = new InSqlClauseBuilder("ClassID").AppendItem(classIds).ToSqlString(TSqlBuilder.Instance);

            string sql = string.Format("select ClassID,count(1) as lesscount from {0} where {1} and {2} group by ClassID", this.GetTableName(), inSql, whereSql);
            var ds = DbHelper.RunSqlReturnDS(sql, this.GetConnectionName());
            if(ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0) {
                ds.Tables[0].Select().ToList().ForEach(row => { result.Add(row["ClassID"].ToString(), row["lesscount"].ToString()); });
            }
            return result;
        }

    }
}
