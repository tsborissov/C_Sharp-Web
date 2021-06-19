using WebServer.Server.Http;

namespace WebServer.Server.Controllers
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute() 
            : base(HttpMethod.Get)
        {
        }
    }
}
