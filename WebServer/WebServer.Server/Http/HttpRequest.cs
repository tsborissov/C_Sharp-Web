using System;
using System.Collections.Generic;
using System.Linq;

namespace WebServer.Server.Http
{
    public class HttpRequest
    {
        private const string NewLine = "\r\n";

        public HttpMethod Method { get; private set; }

        public string Url { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(NewLine);

            var startLine = lines.First().Split(" ");

            var method = ParseHttpMethod(startLine[0]);
            var url = startLine[1];

            var headers = ParseHttpHeaderCollection(lines.Skip(1));

            var bodyLines = lines.Skip(headers.Count + 2);

            var body = string.Join(NewLine, bodyLines);

            return new HttpRequest
            {
                Method = method,
                Url = url,
                Headers = headers,
                Body = body
            };
        }

        private static HttpHeaderCollection ParseHttpHeaderCollection(IEnumerable<string> headerLines)
        {
            var headerCollection = new HttpHeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty) break;

                var headerParts = headerLine.Split(":", 2);

                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid.");
                }

                var header = new HttpHeader
                {
                    Name = headerParts[0],
                    Value = headerParts[1].Trim()
                };

                headerCollection.Add(header);
            }

            return headerCollection;
        }

        private static HttpMethod ParseHttpMethod(string method)
            => method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException($"Method {method} is not supported.")
            };

        //public static string[] GetStartLine(string request)
        //{

        //}
    }
}
