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
                .MapGet("/Cats", request => 
                {
                    const string nameKey = "Name";

                    var query = request.Query;

                    var catName = query.ContainsKey(nameKey)
                    ? query[nameKey]
                    : "the cats";

                    var result = $"<h1>Hello from {catName}.</h1>";

                    return new HtmlResponse(result);
                })
                .MapGet("/Dogs", new HtmlResponse("<h1>Hello from dogs.</h1>")))
            .Start();
    }
}