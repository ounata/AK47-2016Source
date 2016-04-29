using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    /// <summary>
    /// 班组-班级 查询  返回  模型
    /// </summary>
    public class ClassesQueryResultModel
    {
        public PagedQueryResult<ClassSearchModel, ClassSearchModelCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}