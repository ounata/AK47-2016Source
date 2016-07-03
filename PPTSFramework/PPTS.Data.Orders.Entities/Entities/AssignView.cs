using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Entities
{
    [Serializable]
    [ORTableMapping("OM.v_Assigns4Teacher")]
    [DataContract]
    public class AssignView : Assign
    {

    }


    [Serializable]
    [DataContract]
    public class AssignViewCollection : EditableDataObjectCollectionBase<AssignView>
    {

    }
}
