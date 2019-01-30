using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiWithCRUD.Persistance;
using ServerInfoDataAccess;
using Microsoft.ApplicationInsights;
using WebApiWithCRUD.Helper;


namespace WebApiWithCRUD.Controllers
{
    //[BasicAuthorization]
    public class ServerDataController : ApiController
    {
        static readonly IServerDataRepository serverDataRepository = new ServerDataRepository();
        public static TelemetryClient logs;
        //public MessageToServiceBus msg;
        public string refId;


        public ServerDataController()
        {
            logs = new TelemetryClient();
            //msg = new MessageToServiceBus();
        }


        
        public IQueryable<ServerInfo> GET()
        {
            refId = Request.Headers.GetValues("refid").FirstOrDefault<string>();
            logs.TrackEvent("Request received at Http GET Verb" + " refid:" + refId);
            
            return serverDataRepository.GetAll();
        }

        public HttpResponseMessage GET(int id)
        {
            refId = Request.Headers.GetValues("refid").FirstOrDefault<string>();
            logs.TrackEvent("Request received at Http GET Verb with INT parameter" + " refid:" + refId);
            
            return Request.CreateResponse(HttpStatusCode.OK,serverDataRepository.Get(id));
        }

        public  HttpResponseMessage POST(ServerInfo s)
        {
            refId = Request.Headers.GetValues("refid").FirstOrDefault<string>();
            logs.TrackEvent("Request received at Http POST Verb with SeverInfo parameter id as: "+s.server_id+" refid:"+refId);

            //msg.Send(s.ToString());

            if (serverDataRepository.Add(s))
                return Request.CreateResponse(HttpStatusCode.Created, "entry created in database");
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
           
        }

        public HttpResponseMessage PUT(ServerInfo s)
        {
            refId = Request.Headers.GetValues("refid").FirstOrDefault<string>();
            logs.TrackEvent("Request received at Http PUT Verb with ServerInfo parameter id as: "+s.server_id+" refid:"+refId);
            
            //msg.Send(s.ToString());

            if (serverDataRepository.Update(s))
               return Request.CreateResponse(HttpStatusCode.OK,"updated");
            else
               return Request.CreateResponse(HttpStatusCode.ExpectationFailed,"update fail");
        }

        [HttpDelete]
        public HttpResponseMessage DELETE(int id)
        {
            refId = Request.Headers.GetValues("refid").FirstOrDefault<string>();
            logs.TrackEvent("Request received at Http DELETE Verb with INT parameter id as: " + id + " refid:" + refId);

            if (serverDataRepository.Delete(id))
                return Request.CreateResponse(HttpStatusCode.OK, id + " id delete done");
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, id + " id not found");

        }

        

    }

}
