using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    [ORTableMapping("BaeTable")]
    public class BaseObject
    {
        [ORFieldMapping("ID", PrimaryKey = true)]
        public virtual string ID
        {
            get;
            set;
        }

        [ORFieldMapping("Name")]
        public virtual string Name
        {
            get;
            set;
        }
    }
}
