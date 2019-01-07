using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using WebApiWithCRUD.Controllers;
using Microsoft.ServiceBus.Messaging;
using System.Runtime.Serialization;

namespace WebApiWithCRUD.Helper
{
    public class MessageToServiceBus 
    {
        
        private string ServiceBusConnectionString =ConfigurationManager.ConnectionStrings["ServiceBusConnectionString"].ConnectionString;
        private string queueName = ConfigurationManager.AppSettings["QueueName"];
        public  string CorrelationId { get; set; }
        private string topicName = "di-demotopic";
        private string subName = "di-demosub";

        public MessageToServiceBus()
        {
            CorrelationId = null;
        }

        public void Send(IEnumerable<string> payload)
        {

            try
            {

                QueueClient qc = QueueClient.CreateFromConnectionString(ServiceBusConnectionString, queueName);
                TopicClient tc = TopicClient.CreateFromConnectionString(ServiceBusConnectionString, topicName);
                var msg = new BrokeredMessage(payload.FirstOrDefault<string>());

                tc.Send(msg);

                SubscriptionClient sc = SubscriptionClient.CreateFromConnectionString(ServiceBusConnectionString, topicName, subName);
                sc.OnMessage(m => { qc.Send(new BrokeredMessage(m.GetBody<string>())); });

            }
            catch (SerializationException e)
            {
                ServerDataController.logs.TrackException(e);
            }
            catch (Exception e)
            {
                ServerDataController.logs.TrackException(e);
            }
            
        }

       
    }
}