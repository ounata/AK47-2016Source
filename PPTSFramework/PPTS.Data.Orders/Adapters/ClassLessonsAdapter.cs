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

        ///// <summary>
        ///// 获取 未结账课时
        ///// </summary>
        ///// <param name="classIds"></param>
        ///// <param name="date"></param>
        ///// <returns></returns>
        //public Dictionary<string, decimal> GetNoOpenAccountLessonCount(DateTime date,params string[] classIds)
        //{
        //    var result = new Dictionary<string, decimal>();
            
        //    GetNoOpenAccountLessonCountInContext(d => { result = d; }, date, classIds);
        //    GetDbContext().DoAction(dbContext => dbContext.ExecuteDataSetSqlInContext());
            
        //    return result;
        //}

        //public void GetNoOpenAccountLessonCountInContext(Action<Dictionary<string, decimal>> action, DateTime date, params string[] classIds)
        //{
        //    var whereSql = new WhereSqlClauseBuilder();
        //    whereSql.AppendItem("EndTime", date, ">");
        //    var inSql = new InSqlClauseBuilder("ClassID").AppendItem(classIds);
        //    var builder = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, whereSql, inSql);
        //    string sql = string.Format("select ClassID,count(1) as lesscount from {0} where {1} group by ClassID", this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));
        //    var sqlContext = this.GetSqlContext();
        //    sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);
        //    sqlContext.RegisterTableAction("NoOpenAccountLessonTable", table => {
        //        var result = new Dictionary<string, decimal>();
        //        table.Select().ToList().ForEach(row => { result.Add(row["ClassID"].ToString(), Convert.ToDecimal( row["lesscount"].ToString())); });
        //        action(result);
        //    });

        //}

        public ClassLessonCollection LoadCollectionByClassID(string ClassID) {
            ClassLessonCollection result = new ClassLessonCollection();
            
            result = this.Load(builder=> builder.AppendItem("ClassID", ClassID));
            return result;
        }

    }
}
