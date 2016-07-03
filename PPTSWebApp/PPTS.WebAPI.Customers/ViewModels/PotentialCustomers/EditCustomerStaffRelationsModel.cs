using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.Validation;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class EditCustomerStaffRelationsModel
    {
        [ObjectValidator]
        public IList<CustomerStaffRelation> CustomerStaffRelations { set; get; }

        public void InitCustomerStaffRelation()
        {
            this.CustomerStaffRelations.ToList().ForEach(action => {
                var csr = CustomerStaffRelationAdapter.Instance.GetCustomerStaffRelation(action.CustomerID, ((int)action.RelationType).ToString());
                csr.IsNotNull(a => { action.VersionStartTime = a.VersionStartTime;action.VersionEndTime = a.VersionEndTime; });
            });
        }
        public int[] MessageType { set; get; }
        public static string GenericContextMessage(RemainType remainType, string customerName)
        {
            string content = string.Empty;
            switch (remainType)
            {
                case RemainType.Email:
                    content = string.Format(@"尊敬的XX老师: \n\n您好，您名下有新分配的潜在客户，请尽快跟进！  ");
                    break;
                case RemainType.Message:
                    content = string.Format(@"您名下有新分配的潜在客户，请尽快跟进！");
                    break;
            }

            return content;
        }
        public static CTUser GetUser(string staffId)
        {
            IUser user = Data.Common.OGUExtensions.GetUserByID(staffId);

            if (user == null)
            {
                throw new ApplicationException("该学员无学管师！");
                //Name = user.Name;

                //Email = user.Email;

                //Mobile = user.GetUserMobile();
            }
            return new CTUser() { Name = user.Name, Email = user.Email, Mobile = user.GetUserMobile() };
        }
    }
   
}