using MCS.Library.OGUPermission;
using MCS.Library.Validation;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class EditCustomerTransferResourcesModel
    {
        [ObjectValidator]
        public IList<CustomerTransferResource> CustomerTransferResources { set; get; }
        /// <summary>
        /// 邮件或者短信
        /// </summary>
        public int[] MessageType { set; get; }
        public static string GenericContextMessage(RemainType remainType, string teacherName)
        {
            string content = string.Empty;
            switch (remainType)
            {
                case RemainType.Email:
                    content = string.Format(@"尊敬的{0}老师: \n\n您好，您所在的校区有新分配的潜在客户，请尽快分配咨询师并进行跟进工作！  ", teacherName);
                    break;
                case RemainType.Message:
                    content = string.Format(@"{0}老师 您所在校区有新分配的潜在客户，请尽快分配咨询师并进行跟进工作！", teacherName);
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
    public class CTUser
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Mobile { set; get; }
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            CTUser ctUser = obj as CTUser;
            return this.Name.Equals(ctUser.Name);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}