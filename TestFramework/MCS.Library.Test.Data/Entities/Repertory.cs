using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Test.Data.Entities
{
    [Serializable]
    [ORTableMapping("Repertories")]
    public class Repertory
    {
        [ORFieldMapping("ProductID", PrimaryKey = true)]
        public string ProductID
        {
            get;
            set;
        }

        [ORFieldMapping("ProductName")]
        public string ProductName
        {
            get;
            set;
        }

        [ORFieldMapping("TotalQuantity")]
        public int TotalQuantity
        {
            get;
            set;
        }

        [ORFieldMapping("UsedQuantity")]
        public int UsedQuantity
        {
            get;
            set;
        }

        [ORFieldMapping("CreateTime")]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()")]
        public DateTime CreateTime
        {
            get;
            set;
        }
    }

    [Serializable]
    public class RepertoryCollection : EditableDataObjectCollectionBase<Repertory>
    {
    }
}
