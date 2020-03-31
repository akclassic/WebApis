using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SampleApi.Filters
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"Unauthorized Request");
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.ToString();
                if(authToken == "X12345678")
                {
                    //Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(authToken), null);
                    
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized Request");
                }
            }
        }
    }
}