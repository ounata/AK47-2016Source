using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Adapters
{
    public class PresentPermissionAdapter : ProductAdapterBase<PresentPermission, PresentPermissionCollection>
    {
        public static PresentPermissionAdapter Instance = new PresentPermissionAdapter();
    }
}
