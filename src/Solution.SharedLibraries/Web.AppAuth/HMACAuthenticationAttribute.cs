using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Security.Principal;

namespace Web.AppAuth
{
    public class HMACAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private bool ignoreHashBody = false;
        public System.Threading.Tasks.Task AuthenticateAsync(HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
        {
            var msg = string.Empty;
            if (IsValidRequest())//(out msg))
            {
                var currentPrincipal = new GenericPrincipal(new GenericIdentity("Coreapp"), null);
                context.Principal = currentPrincipal;
            }
            else
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            }

            return System.Threading.Tasks.Task.FromResult(0);
        }

        private bool IsValidRequest()//(out string msg)
        {
            return true;
        }
        public System.Threading.Tasks.Task ChallengeAsync(HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            return System.Threading.Tasks.Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return false; }
        }

        public bool IgnoreHashBody
        {
            get
            {
                return this.ignoreHashBody
;
            }
            set { this.ignoreHashBody = value; }
        }
    }

    public class ResultWithChallenge : IHttpActionResult
    {
        private readonly string authenticationScheme = "amx";
        private readonly IHttpActionResult next;

        public ResultWithChallenge(IHttpActionResult next)
        {
            this.next = next;
        }

        public async System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
           var response = await next.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(authenticationScheme));
            }

            response.Headers.Add("error-code", response.ReasonPhrase);
            return response;
        }
    }
}
