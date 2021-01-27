using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Lib.Domain.ModelApi;

namespace Web.AppAuth
{
    public class AuthenticationHandler : DelegatingHandler
    {
        public const string HEADER_VERSION = "X-Core-Version";
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                string configVersion = ConfigurationManager.AppSettings["APP_VERSION"];
                Version currentVersion = new Version(configVersion);
                var headers = HttpContext.Current.Request.Headers;
                if (headers != null)
                {
                    string headerVersion = headers[HEADER_VERSION];
                    if (!string.IsNullOrEmpty(headerVersion))
                    { 
                        Version appVersion = new Version(headerVersion);
                        if (appVersion < currentVersion)
                        {
                            return Task<HttpResponseMessage>.Factory.StartNew(() =>
                            {
                                return request.CreateResponse(HttpStatusCode.OK, new ResponseData()
                                {
                                    Result = HttpStatusCode.HttpVersionNotSupported
                                });
                            });
                        }
                    }
                }
            }
            catch
            {

            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
