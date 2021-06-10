using WebServer.Server.Http;

namespace WebServer.Server.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html) 
            : base(html, HttpContentType.Html)
        {
        }
    }
}
