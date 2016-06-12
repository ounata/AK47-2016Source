using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// 课时关系授权对象
    /// </summary>
    [Serializable]
    [ORTableMapping("MT.CourseRelationAuthorizations")]
    [DataContract]
    public class CourseRelationAuthorization : RelationAuthorizationBase
    {
    }

    public class CourseRelationAuthorizationCollection : EditableDataObjectCollectionBase<CourseRelationAuthorization>
    { }
}
