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
    /// 客户关系授权对象
    /// </summary>
    [Serializable]
    [ORTableMapping("MT.CustomerRelationAuthorizations")]
    [DataContract]
    public class CustomerRelationAuthorization: RelationAuthorizationBase
    {
       
    }

    public class CustomerRelationAuthorizationCollection : EditableDataObjectCollectionBase<CustomerRelationAuthorization>
    { }
}
