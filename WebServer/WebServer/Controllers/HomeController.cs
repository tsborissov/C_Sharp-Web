using WebServer.Server;
using WebServer.Server.Http;
using WebServer.Server.Controllers;
using WebServer.Server.Results;
using System;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index() => Text("Hello from this web server.");

        public HttpResponse LocalRedirect() => Redirect("/Animals/Cats");

        public HttpResponse ToSoftUni() => Redirect("https://softuni.bg");

        public HttpResponse StaticFiles() => View();

        public HttpResponse Error() => throw new InvalidOperationException("Invalid action!");
    }
}
