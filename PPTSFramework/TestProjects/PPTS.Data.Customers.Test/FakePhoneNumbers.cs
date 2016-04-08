using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Test
{
    public class FakePhoneNumbers : IContactPhoneNumbers
    {
        public string PrimaryPhone
        {
            get;
            set;
        }

        public string SecondaryPhone
        {
            get;
            set;
        }
    }
}
