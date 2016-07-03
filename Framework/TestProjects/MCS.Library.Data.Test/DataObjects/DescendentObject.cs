using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    public class DescendentObject : BaseObject
    {
        [ORFieldMapping("DescendentName")]
        public string DescendentName
        {
            get;
            set;
        }
    }
}
