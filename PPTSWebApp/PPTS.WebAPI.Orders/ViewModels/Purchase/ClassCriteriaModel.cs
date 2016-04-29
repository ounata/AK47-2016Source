using MCS.Library.Data;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{
    public class ClassCriteriaModel
    {
        [ConditionMapping("ProductID")]
        public string ProductID { set; get; }


        [ConditionMapping("ClassName")]
        public string ClassName { set; get; }
        

        //----------------------------



        [NoMapping]
        public PageRequestParams PageParams
        {
            get; set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get; set;
        }

    }

    public class ClassQueryResult
    {
        public PagedQueryResult<Class, ClassCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }

    public class ClassModel : Class {

        [NoMapping]
        [DataMember]
        public string OrderAmount { set; get; }
    }

    public class ClassCollectionModel: EditableDataObjectCollectionBase<ClassModel> {

        public void FillOrderAmount()
        {
            var classIds = this.Select(m => m.ClassID).ToArray();
            
            var openAccountDate = DateTime.Parse( DateTime.Now.ToString("yyyy-" + (DateTime.Now.Month + 1) + "-1"));
            ClassLessonsAdapter.Instance.GetNoOpenAccountLessonCount(classIds, openAccountDate).ToList().ForEach(kv => { this.Single(s => s.ClassID == kv.Key).OrderAmount = kv.Value; });
        }
    }

}