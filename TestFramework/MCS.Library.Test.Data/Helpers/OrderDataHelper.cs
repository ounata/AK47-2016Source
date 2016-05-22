using MCS.Library.Core;
using MCS.Library.Net.SNTP;
using MCS.Library.Test.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Test.Data.Helpers
{
    public static class OrderDataHelper
    {
        public static Repertory PrepareRepertory()
        {
            Repertory result = new Repertory();

            result.ProductID = UuidHelper.NewUuidString();
            result.ProductName = string.Format("Surface Pro 4-{0: yyyy-MM-dd HH:mm:ss}", SNTPClient.AdjustedLocalTime);
            result.TotalQuantity = 100;

            return result;
        }

        public static Order PrepareOrder(string productID)
        {
            Order result = new Order();

            result.OrderID = UuidHelper.NewUuidString();
            result.OrderName = string.Format("采购Surface Pro合同-{0: yyyy-MM-dd HH:mm:ss}", SNTPClient.AdjustedLocalTime);
            result.ProductID = productID;
            result.Quantity = 45;

            return result;
        }
    }
}
