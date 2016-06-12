using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Orders;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.DataSources
{
    public class ClassGroupDataSource : GenericOrderDataSource<ClassSearchModel, ClassSearchModelCollection>
    {
        public static readonly new ClassGroupDataSource Instance = new ClassGroupDataSource();

        public ClassGroupDataSource()
        {
        }


        public PagedQueryResult<ClassSearchModel, ClassSearchModelCollection> LoadClasses(IPageRequestParams prp, ClassesQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var classBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition, new AdjustConditionValueDelegate(ClassesQueryCriteriaModel.ClassesAdjustConditionValueDelegate));
            var classLessonItemsBuilder = ConditionMapping.GetWhereSqlClauseBuilder(condition, new AdjustConditionValueDelegate(ClassesQueryCriteriaModel.ClassLessonItemsAdjustConditionValueDelegate));
            var classLessonsBuilder = ConditionMapping.GetWhereSqlClauseBuilder(condition, new AdjustConditionValueDelegate(ClassesQueryCriteriaModel.ClassLessonsAdjustConditionValueDelegate));

            string classWhere =classBuilder.ToSqlString(TSqlBuilder.Instance);
            string classLessonsWhere = condition.CheckClassLessonsAdjustCondition() ? string.Format(" and exists(select * from [OM].[ClassLessons] cl where {0} and Classes.ClassID = cl.ClassID  )", classLessonsBuilder.ToSqlString(TSqlBuilder.Instance) ) : "";
            string classLessonItemsWhere = condition.CheckClassLessonItemsAdjustCondition() ? string.Format(" and  exists(select * from [OM].[ClassLessonItems] cli inner join [OM].[ClassLessons] cll on cli.LessonID = cll.LessonID and cll.ClassID = Classes.ClassID and {0})", classLessonItemsBuilder.ToSqlString(TSqlBuilder.Instance)) : "";

            string sqlWhere = string.Format("{0}{1}{2}", string.IsNullOrEmpty(classWhere)?" 1 = 1 ": classWhere, classLessonsWhere, classLessonItemsWhere);

            sqlWhere = sqlWhere + string.Format(" and  ClassStatus != {0} ", ((int)ClassStatusDefine.Deleted).ToString());

            var result = Query(prp, sqlWhere, " ClassID desc ");

            return result;
        }







    }
}