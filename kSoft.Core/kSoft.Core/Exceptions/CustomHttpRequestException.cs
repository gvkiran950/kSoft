using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace kSoft.Core.Exceptions
{
    public class CustomHttpRequestException : HttpRequestException
    {
        public HttpStatusCode HttpCode { get;}
        public CustomHttpRequestException(HttpStatusCode code) : this(code, null, null)
        {
        }

        public CustomHttpRequestException(HttpStatusCode code, string message) : this(code, message, null)
        {
        }

        public CustomHttpRequestException(HttpStatusCode code, string message, Exception inner) : base(message, inner)
        {
            HttpCode = code;
        }

    }
}
