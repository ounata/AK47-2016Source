using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Products.Entities;
using System;
using System.Data;

namespace PPTS.Data.Products.Adapters
{
    public class DiscountPermissionAdapter : ProductAdapterBase<DiscountPermission, DiscountPermissionCollection>
    {
        public static DiscountPermissionAdapter Instance = new DiscountPermissionAdapter();
        
    }
}
