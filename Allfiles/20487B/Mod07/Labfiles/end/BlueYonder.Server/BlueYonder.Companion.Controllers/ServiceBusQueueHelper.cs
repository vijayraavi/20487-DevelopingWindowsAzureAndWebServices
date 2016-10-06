using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.Azure;


namespace BlueYonder.Companion.Controllers
{
    
    
    class ServiceBusQueueHelper
    {
        static readonly string QueueName = "FlightUpdatesQueue";

        
        public static QueueClient ConnectToQueue()
        {
            //TODO: Lab07, Exercise 2, Task 1.4 : Implement the ConnectToQueue Method.
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            return QueueClient.CreateFromConnectionString(connectionString, QueueName);
        }

    }
}
