using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace Student_API
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,"User Not Authenticate");
               
            }
            else
            {
                String AuthentictionToken = actionContext.Request.Headers.Authorization.Parameter;
                String DecodingAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(AuthentictionToken));
                String[] userNamePasswordArray = DecodingAuthenticationToken.Split(':');
                String UserName = userNamePasswordArray[0];
                String PassWord = userNamePasswordArray[1];
                if(StudentSecurity.login(UserName,PassWord))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(UserName),null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}