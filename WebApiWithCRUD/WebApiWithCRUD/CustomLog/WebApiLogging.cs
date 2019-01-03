using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace WebApiWithCRUD.CustomLog
{
    public class  WebApiLogging
    {
        public TelemetryClient client;

        public WebApiLogging()
        {
            client = new TelemetryClient();
        }
       
    }
}