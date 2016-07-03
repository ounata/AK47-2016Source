using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    

    ///学员视图，排课条件列表，查询结果数据模型
    public class AssignConditionQCR
    {
        public PagedQueryResult<AssignCondition, AssignConditionCollection> QueryResult { get; set; }
    }

    public class AssignQCR
    {
        public PagedQueryResult<Assign, AssignCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }

}