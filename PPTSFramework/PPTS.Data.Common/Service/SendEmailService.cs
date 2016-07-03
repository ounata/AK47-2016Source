using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.SOA.DataObjects;

namespace PPTS.Data.Common.Service
{
    public class SendEmailService
    {
        public static SendEmailService Instance = new SendEmailService();

        public void SendEmail(string address, string title, string body)
        {
            EmailMessage message = new EmailMessage(address, title, body);

            EmailMessageAdapter.Instance.Insert(message);

        }

    }
}
