using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// 课时机构授权对象
    /// </summary>
    [Serializable]
    [ORTableMapping("MT.CourseOrgAuthorizations")]
    [DataContract]
    public class CourseOrgAuthorization : OrgAuthorizationBase
    {

    }
    public class CourseOrgAuthorizationCollection : EditableDataObjectCollectionBase<CourseOrgAuthorization>
    { }
}
