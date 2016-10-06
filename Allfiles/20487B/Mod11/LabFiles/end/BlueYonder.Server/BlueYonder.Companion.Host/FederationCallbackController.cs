using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Web;
using Microsoft.IdentityModel;

namespace BlueYonder.Companion.Host
{
    public class FederationCallbackController : ApiController
    {
        public HttpResponseMessage Post()
        {
            var response = this.Request.CreateResponse(HttpStatusCode.Redirect);
            response.Headers.Add("Location", @"FederationCallback/end?acsToken=" + HttpContext.Current.User.BootstrapToken());

            return response;
        }     
    }
}
