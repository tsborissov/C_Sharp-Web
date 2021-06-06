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
                .MapGet("/Cats", new HtmlResponse("<h1>Hello from cats.</h1>"))
                .MapGet("/Dogs", new HtmlResponse("<h1>Hello from dogs.</h1>")))
            .Start();
    }
}