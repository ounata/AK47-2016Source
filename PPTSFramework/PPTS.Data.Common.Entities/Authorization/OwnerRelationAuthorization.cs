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
    /// 所有者关系授权记录
    /// </summary>
    [Serializable]
    [ORTableMapping("MT.OwnerRelationAuthorizations")]
    [DataContract]
    public class OwnerRelationAuthorization : RelationAuthorizationBase
    {

    }

    public class OwnerRelationAuthorizationCollection : EditableDataObjectCollectionBase<OwnerRelationAuthorization>
    { }
}
