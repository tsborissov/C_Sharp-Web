using System.Threading.Tasks;
using WebServer.Server;
using WebServer.Server.Responses;

namespace WebServer
{
    public class StartUp
    {
        static async Task Main(string[] args)
            => await new HttpServer(routes => routes
                .MapGet("/", new TextResponse("Hello from this web server."))
                .MapGet("/Cats", new TextResponse("<h1>Hello from cats.</h1>", "text/html; charset=utf-8"))
                .MapGet("/Dogs", new TextResponse("Hello from dogs.")))
            .Start();
    }
}