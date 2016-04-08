﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MCS.Web.MVC.Library.Filters
{
    internal class PassportAuthenticationFailureResult : IHttpActionResult
    {
        public PassportAuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase
        {
            get;
            private set;
        }

        public HttpRequestMessage Request
        {
            get;
            private set;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;

            return response;
        }
    }
}
