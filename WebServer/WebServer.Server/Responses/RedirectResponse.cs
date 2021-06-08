using WebServer.Server.Http;

namespace WebServer.Server.Responses
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
        }
    }
}
