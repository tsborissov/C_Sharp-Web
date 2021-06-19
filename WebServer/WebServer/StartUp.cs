using System.Threading.Tasks;
using WebServer.Controllers;
using WebServer.Server;
using WebServer.Server.Controllers;

namespace WebServer
{
    public class StartUp
    {
        static async Task Main(string[] args)
            => await new HttpServer(routes => routes
                .MapStaticFiles()
                .MapControllers()
                .MapGet<HomeController>("/ToCats", c => c.LocalRedirect()))
            .Start();
    }
}