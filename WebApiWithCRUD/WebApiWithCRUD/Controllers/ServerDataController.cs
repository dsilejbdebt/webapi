using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiWithCRUD.Persistance;
using System.Data.Entity;
using ServerInfoDataAccess;
using WebApiWithCRUD.CustomLog;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using WebApiWithCRUD.Helper;
using System.Threading.Tasks;


namespace WebApiWithCRUD.Controllers
{
    public class ServerDataController : ApiController
    {
        static readonly IServerDataRepository serverDataRepository = new ServerDataRepository();
        public static WebApiLogging weblog=new WebApiLogging();
        public static TelemetryClient logs;
        HttpRequestMessage req;
        //public static TelemetryClient logs= weblog.reglogs();

        public ServerDataController()
        {
            logs = weblog.client;
            req = new HttpRequestMessage();
        }


        [HttpDelete]
        public void Delete(int id)
        {
            logs.TrackEvent("Request received at Http DELETE Verb with INT parameter"+id);
            logs.Flush();
            
            serverDataRepository.Delete(id);
            req.CreateResponse(HttpStatusCode.OK);
            
        }

      
        public void Post(ServerInfo s)
        {
            
            logs.TrackEvent("Request received at Http POST Verb with SeverInfo parameter");
            logs.Flush();
            serverDataRepository.Add(s);
            req.CreateResponse(HttpStatusCode.Created);
           
        }

      
        public void Put(ServerInfo s)
        {
            logs.TrackEvent("Request received at Http PUT Verb with ServerInfo parameter");
            logs.Flush();
            if (serverDataRepository.Update(s))
                req.CreateResponse(HttpStatusCode.OK);
            else
                req.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        public IQueryable<ServerInfo> Get()
        {
            logs.TrackEvent("Request received at Http GET Verb");
            logs.Flush();
            return serverDataRepository.GetAll();
        }

        public ServerInfo Get(int id)
        {
            logs.TrackEvent("Request received at Http GET Verb with INT parameter");
            logs.Flush();
            return serverDataRepository.Get(id);
        }

        
    }
}
