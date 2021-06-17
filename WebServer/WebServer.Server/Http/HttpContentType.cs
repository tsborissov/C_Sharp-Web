using System;

namespace WebServer.Server.Http
{
    public class HttpContentType
    {
        public const string PlainText = "text/plain; charset=utf-8";
        public const string Html = "text/html; charset=utf-8";
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        public static string GetByFileExtension(string fileExtension)
            => fileExtension switch
            {
                "css" => "text/css",
                "js" => "application/javascript",
                "jpg" or "jpeg" => "image/jpeg",
                "png" => "image/png",
                _ => PlainText
            };
    }
}
