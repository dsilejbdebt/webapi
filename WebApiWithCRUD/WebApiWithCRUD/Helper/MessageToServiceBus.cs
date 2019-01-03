using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace WebApiWithCRUD.Helper
{
    public class MessageToServiceBus 
    {
        
        private const string ServiceBusConnectionString = " Some Endpoint";
        public string CorrelationId { get; set; }

        public MessageToServiceBus()
            {
                
            }
        public async Task Send(IEnumerable<string> payload)
            {
               
                try
                {
                    //string payload="example";
                    Message message = new Message(Encoding.UTF8.GetBytes(payload.FirstOrDefault<string>()));
                            message.CorrelationId = payload.FirstOrDefault<string>();
                

                    var Client = new QueueClient(ServiceBusConnectionString, "QueueName");
                    await Client.SendAsync(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        
    }
}