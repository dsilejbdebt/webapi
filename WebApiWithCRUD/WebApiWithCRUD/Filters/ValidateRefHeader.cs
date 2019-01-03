using System;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net;

namespace WebApiWithCRUD.Filters
{
    public class ValidateRefHeader : ActionFilterAttribute
    {
      
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var header = actionContext.Request.Headers;
            if (!header.Contains("refid"))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Refid headrer not present");

            }
        }
      

    }
}