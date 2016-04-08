using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Test.Validators
{
    public class CompositeObject
    {
        [ObjectValidator]
        public Customer Customer
        {
            get;
            set;
        }

        [ObjectValidator]
        public Order Order
        {
            get;
            set;
        }
    }
}
