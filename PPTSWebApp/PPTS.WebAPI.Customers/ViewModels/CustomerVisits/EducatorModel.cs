using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Common.Security;
using MCS.Library.OGUPermission;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVisits
{
    public class EducatorModel
    {
        string Name { get; set; }
        string Email { get; set; }
        string Mobile { get; set; }

        public EducatorModel(string customerID)
        {
            CustomerStaffRelation csRelation = new CustomerStaffRelation();

            csRelation = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(customerID, CustomerRelationType.Educator);

            if (csRelation != null)
            {
                IUser user = Data.Common.OGUExtensions.GetUserByID(csRelation.StaffID);

                if (user != null)
                {
                    Name = user.Name;

                    Email = user.Email;

                    Mobile = user.GetUserMobile();
                }
            }
        }

        public static string GenericContextMessage(EducatorModel educatorModel,string studentName,DateTime visitTime, RemainType remainType)
        {
            string str = "";
            if (remainType == RemainType.Email)
            {
                str = educatorModel.Name + "老师：\n\n";
                str = str + "\t您好，您将于" + visitTime.ToString() + "对" + studentName + "同学进行回访，请勿忘记。（本邮件为系统邮件，请勿回复。）";
            }
            else if (remainType == RemainType.Message)
            {
                str = educatorModel.Name + "老师,";
                str = str + "您好。您将于" + visitTime.ToString() + "对" + studentName + "同学进行回访，请勿忘记。（本邮件为系统邮件，请勿回复。）";
            }

            return str;
        }
    }
}