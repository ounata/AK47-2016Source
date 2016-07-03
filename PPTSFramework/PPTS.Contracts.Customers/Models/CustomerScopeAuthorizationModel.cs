﻿using PPTS.Data.Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Models
{
    [Serializable]
    [DataContract]
    public class CustomerScopeAuthorizationModel
    {
        [DataMember]
        public string OwnerID
        { get; set; }

        [DataMember]
        public RecordType OwnerType
        { get; set; }

        [DataMember]
        public RelationType RelationType
        { get; set; }
    }
}