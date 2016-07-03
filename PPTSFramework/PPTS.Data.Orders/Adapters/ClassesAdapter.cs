using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
    public class ClassesAdapter : ClassGroupAdapterBase<Class, ClassCollection>
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

        public int GetClassCountByProductID(string productID)
        {
            int classCount = 0;
            string sqlText_classCount = string.Format(@"select count(1) c
                                                        from[OM].[Classes]
                                                        where ProductID = '{0}' and ClassStatus != 9", productID);
            DataSet ds_classCount = DbHelper.RunSqlReturnDS(sqlText_classCount, OrdersAdapter.Instance.GetDbContext().Name);

            if (ds_classCount.Tables[0].Rows.Count > 0)
                classCount = int.Parse(ds_classCount.Tables[0].Rows[0][0].ToString());
            return classCount;
        }

        public int GetValidClassesCountByProductID(string productID, DateTime closeDate)
        {
            int ValidClassesCount = 0;
            string sqlText_ValidClassesCount = string.Format(@"select count(1) c
                                                        from[OM].[Classes]
                                                        where ProductID = '{0}' and ClassStatus != 9 and EndTime < '{1}'", productID, closeDate.ToString(""));
            DataSet ds_classCount = DbHelper.RunSqlReturnDS(sqlText_ValidClassesCount, OrdersAdapter.Instance.GetDbContext().Name);

            if (ds_classCount.Tables[0].Rows.Count > 0)
                ValidClassesCount = int.Parse(ds_classCount.Tables[0].Rows[0][0].ToString());
            return ValidClassesCount;
        }

        /// <summary>
        /// 是否存在插班
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool IsExistsTransfer(ClassCollection collection)
        {
            
            var whereSqlBuilder = new WhereSqlClauseBuilder();

            collection.ForEach(m =>
            {
                whereSqlBuilder.AppendItem("ClassID", m.ClassID).AppendItem("ProductID",m.ProductID);
            });

            whereSqlBuilder.AppendItem("LessonCount-InvalidLessons", "0", ">", true);

            var sql = string.Format("if exists (select 1 from {0} where {1} ) begin select 1 end else select 0 end",
                GetQueryTableName(),
                whereSqlBuilder.ToSqlString(TSqlBuilder.Instance));

            return ((int)DbHelper.RunSqlReturnScalar(sql, GetConnectionName())) == 1;

        }


    }
}
