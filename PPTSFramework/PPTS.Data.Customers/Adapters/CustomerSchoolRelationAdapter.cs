﻿using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerSchoolRelationAdapter : CustomerAdapterBase<CustomerSchoolRelation, CustomerSchoolRelationCollection>
    {
        public new static CustomerSchoolRelationAdapter Instance = new CustomerSchoolRelationAdapter();

        private CustomerSchoolRelationAdapter()
        {
        }
    }
}