using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Models
{
    [DataContract]
    public class AssetStatisticQueryResult
    {
        [DataMember]
        public decimal AssetsValue
        { get; set; }
    }
}
