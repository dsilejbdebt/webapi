using System;
using System.Configuration;
using Microsoft.ServiceBus.Messaging;
using System.Runtime.Serialization;
using Microsoft.ApplicationInsights;

namespace WebApiWithCRUD.Helper
{
    public class MessageToServiceBus 
    {
        
        private string ServiceBusConnectionString =ConfigurationManager.ConnectionStrings["ServiceBusConnectionString"].ConnectionString;
        private string queueName = ConfigurationManager.AppSettings["QueueName"];
        private string topicName = ConfigurationManager.AppSettings["TopicName"];
        private TelemetryClient logs;
       
        public MessageToServiceBus()
        {
            logs = new TelemetryClient();
        }

        public void Send(string payload)
        {

            try
            {
                TopicClient tc = TopicClient.CreateFromConnectionString(ServiceBusConnectionString, topicName);
                var msg = new BrokeredMessage(payload);

                tc.Send(msg);

                //SubscriptionClient sc = SubscriptionClient.CreateFromConnectionString(ServiceBusConnectionString, topicName, subName);
                //sc.OnMessage(m => { qc.Send(new BrokeredMessage(m.GetBody<string>())); });

            }
            catch (SerializationException e)
            {
                logs.TrackException(e);
            }
            catch (Exception e)
            {
                logs.TrackException(e);
            }
            
        }

       
    }
}