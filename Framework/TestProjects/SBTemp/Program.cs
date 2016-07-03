using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTemp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];

            NamespaceManager namespaceManager =
                    NamespaceManager.CreateFromConnectionString(connectionString);

            if (namespaceManager.TopicExists("TestTopic") == false)
            {
                namespaceManager.CreateTopic("TestTopic");
            }

            if (namespaceManager.SubscriptionExists("TestTopic", AppDomain.CurrentDomain.FriendlyName) == false)
            {
                SubscriptionDescription sd = new SubscriptionDescription("TestTopic", AppDomain.CurrentDomain.FriendlyName);

                sd.TopicPath = "TestTopic";
                sd.EnableBatchedOperations = true;
                sd.AutoDeleteOnIdle = TimeSpan.FromMinutes(5);
                sd.Name = AppDomain.CurrentDomain.FriendlyName;

                namespaceManager.CreateSubscription(sd);
            }

            TopicClient topicClient = TopicClient.CreateFromConnectionString(connectionString, "TestTopic");

            BrokeredMessage message1 = new BrokeredMessage("Hello " + DateTime.Now);

            message1.Properties["CacheQueue"] = "Test Cache Queue";

            topicClient.Send(message1);

            BrokeredMessage message2 = new BrokeredMessage("Hello 2" + DateTime.Now);

            message2.Properties["CacheQueue"] = "Test Cache Queue";

            topicClient.Send(message2);

            SubscriptionClient client = SubscriptionClient.CreateFromConnectionString(connectionString, "TestTopic",
                AppDomain.CurrentDomain.FriendlyName, ReceiveMode.ReceiveAndDelete);

            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = true;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            IEnumerable<BrokeredMessage> messages = client.ReceiveBatch(20);

            foreach (BrokeredMessage message in messages)
            {
                Console.WriteLine(message.GetBody<string>());
            }
        }
    }
}
