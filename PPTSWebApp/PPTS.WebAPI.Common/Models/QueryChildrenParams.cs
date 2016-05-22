using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Common.Models
{
    public class QueryChildrenParams
    {
        public QueryChildrenParams()
        {
        }

        public QueryChildrenParams(string parentKey, DepartmentType deptType)
        {
            this.ParentKey = parentKey;
            this.DepartmentType = deptType;
        }

        public string ParentKey
        {
            get;
            set;
        }

        public DepartmentType DepartmentType
        {
            get;
            set;
        }
    }
}