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
    [ORTableMapping("Orders")]
    public class Order
    {
        [ORFieldMapping("OrderID", PrimaryKey = true)]
        public string OrderID
        {
            get;
            set;
        }

        [ORFieldMapping("OrderName")]
        public string OrderName
        {
            get;
            set;
        }

        [ORFieldMapping("ProductID")]
        public string ProductID
        {
            get;
            set;
        }

        [ORFieldMapping("Quantity")]
        public int Quantity
        {
            get;
            set;
        }

        [ORFieldMapping("Status")]
        public OrderStatus Status
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
    public class OrderCollection : EditableDataObjectCollectionBase<Order>
    {
    }
}
