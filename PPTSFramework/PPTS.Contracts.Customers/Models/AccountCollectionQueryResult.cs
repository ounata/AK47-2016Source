using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Models
{
    [DataContract]
    public class AccountCollectionQueryResult
    {
        [DataMember]
        public List<Account> AccountCollection { get; set; }
    }
}
