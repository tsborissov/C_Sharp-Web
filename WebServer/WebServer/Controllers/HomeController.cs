using WebServer.Server;
using WebServer.Server.Http;
using WebServer.Server.Controllers;
using WebServer.Server.Responses;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Index()
            => Text("Hello from this web server.");
    }
}
