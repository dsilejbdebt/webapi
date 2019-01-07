using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiWithCRUD.Persistance;
using ServerInfoDataAccess;
using WebApiWithCRUD.CustomLog;
using Microsoft.ApplicationInsights;
using WebApiWithCRUD.Helper;

namespace WebApiWithCRUD.Controllers
{
    public class ServerDataController : ApiController
    {
        static readonly IServerDataRepository serverDataRepository = new ServerDataRepository();
        public static WebApiLogging weblog=new WebApiLogging();
        public static TelemetryClient logs;
        public MessageToServiceBus msg;


        public ServerDataController()
        {
            logs = weblog.client;
            msg= new MessageToServiceBus();
            
        }


        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            logs.TrackEvent("Request received at Http DELETE Verb with INT parameter id as: "+id);
            logs.Flush();
           
            serverDataRepository.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK,id+" id delete done");
            
        }

      
        public  HttpResponseMessage Post(ServerInfo s)
        {
            logs.TrackEvent("Request received at Http POST Verb with SeverInfo parameter id as: "+s.server_id);
            logs.Flush();
            msg.Send(Request.Headers.GetValues("refid"));
            serverDataRepository.Add(s);
            return Request.CreateResponse(HttpStatusCode.Created,"entry created in database");
           
        }

      
        public HttpResponseMessage Put(ServerInfo s)
        {
            logs.TrackEvent("Request received at Http PUT Verb with ServerInfo parameter id as: "+s.server_id );
            logs.Flush();
            msg.Send(Request.Headers.GetValues("refid"));
            if (serverDataRepository.Update(s))
               return Request.CreateResponse(HttpStatusCode.OK,"updated");
            else
               return Request.CreateResponse(HttpStatusCode.ExpectationFailed,"update fail");
        }

        public IQueryable<ServerInfo> Get()
        {   
            logs.TrackEvent("Request received at Http GET Verb");
            logs.Flush();
            msg.Send(Request.Headers.GetValues("refid"));
            return serverDataRepository.GetAll();
        }

        public ServerInfo Get(int id)
        {
            logs.TrackEvent("Request received at Http GET Verb with INT parameter");
            logs.Flush();
            msg.Send(Request.Headers.GetValues("refid"));
            return serverDataRepository.Get(id);
        }

        
    }
}
