﻿using System.Collections.Generic;

namespace WebServer.Server.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; private set; }

        public string Url { get; private set; }

        public HttpHeaderCollection Headers { get; }  = new HttpHeaderCollection();

        public string Body { get; private set; }
    }
}
