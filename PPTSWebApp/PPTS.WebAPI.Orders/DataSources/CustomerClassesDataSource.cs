using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Orders.DataSources;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.DataSources
{
    public class CustomerClassesDataSource : GenericOrderDataSource<CustomerClassSearchModel, CustomerClassSearchModelCollection>
    {
        public static readonly new CustomerClassesDataSource Instance = new CustomerClassesDataSource();

        public CustomerClassesDataSource()
        {
        }


        public PagedQueryResult<CustomerClassSearchModel, CustomerClassSearchModelCollection> Load(IPageRequestParams prp, CustomerClassQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = " distinct c.createTime,c.campusName,c.productCode,c.productName,c.className,c.subjectName,c.gradeName,c.lessonCount,c.lessonDurationValue,c.periodsOfLesson,c.creatorName,a.RealAmount,a.ConfirmedAmount ";
            var from = string.Format(@"  [OM].[ClassLessonItems] cli 
                                         inner join [OM].[v_Assets]
                                                a on cli.AssetID = a.AssetID
                                        inner join[OM].[ClassLessons]
                                                cl on cl.LessonID = cli.LessonID
                                        inner join[OM].[Classes]
                                                c on c.ClassID = cl.ClassID
                                         ", condition.CustomerID);
            var result = Query(prp, select, from, condition, orderByBuilder);
            return result;

        }







    }
}