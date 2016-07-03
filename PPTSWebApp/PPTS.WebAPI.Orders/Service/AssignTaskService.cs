using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Service
{
    public class AssignTaskService
    {

        public static void UpdateCustomerSearchInfo(string customerID)
        {
            if (string.IsNullOrEmpty(customerID))
                return;
            PPTS.Contracts.Search.Models.CustomerSearchUpdateModel csum = new Contracts.Search.Models.CustomerSearchUpdateModel()
            {
                CustomerID = customerID,
                ObjectID = customerID,
                Type = Contracts.Search.Models.CustomerSearchUpdateType.Assign
            };
            try
            {
                PPTS.Data.Orders.UpdateCustomerSearchByCustomerTask.Instance.UpdateByCustomerInfoByTask(csum);
            }
            catch
            { }
        }

        public static void UpdateCustomerSearchInfo(IList<string> customerID)
        {
            if (customerID == null || customerID.Count() == 0)
                return;
            List<PPTS.Contracts.Search.Models.CustomerSearchUpdateModel> models = new List<PPTS.Contracts.Search.Models.CustomerSearchUpdateModel>();
            foreach (var v in customerID)
            {
                models.Add(new Contracts.Search.Models.CustomerSearchUpdateModel()
                {
                    CustomerID = v,
                    ObjectID = v,
                    Type = Contracts.Search.Models.CustomerSearchUpdateType.Assign
                });
            }
            try
            {
                PPTS.Data.Orders.UpdateCustomerSearchByCustomerTask.Instance.UpdateByCustomerCollectionInfoByTask(models);
            }
            catch
            { }
        }


    }
}