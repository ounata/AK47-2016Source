using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.DataSources
{
    public class ClassCustomerDataSource : GenericOrderDataSource<ClassLessonItem, ClassLessonItemCollection>
    {
        public static readonly new ClassCustomerDataSource Instance = new ClassCustomerDataSource();

        public ClassCustomerDataSource()
        {
        }

        public PagedQueryResult<ClassLessonItem, ClassLessonItemCollection> Load(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            ClassCustomerQueryCriteriaModel model = (ClassCustomerQueryCriteriaModel)condition;
            ClassLessonCollection clc = ClassLessonsAdapter.Instance.LoadCollectionByClassID(model.ClassID);
            if (model.LessonIDs == null || model.LessonIDs.Length == 0)
            {
                model.LessonIDs = new string[clc.Count];
                for (int i = 0; i < clc.Count; i++)
                {
                    model.LessonIDs[i] = clc[i].LessonID;
                }
            }
            
            
            var from_select = " distinct CustomerID,CustomerName,CustomerCode,CustomerGradeName,ConsultantName,EducatorName,IsJoinClass,AssetCode ";
            var from_where = ConditionMapping.GetConnectiveClauseBuilder(model).ToSqlString(MCS.Library.Data.Builder.TSqlBuilder.Instance);
            
            var from = String.Format(" (select {0} from [OM].[ClassLessonItems]  where {1}) cli ",from_select,from_where);
            //var result = Query(prp, select, from, model, orderByBuilder);
            var result = Query(prp, "*", from, null, orderByBuilder);
            return result;
        }
    }
}