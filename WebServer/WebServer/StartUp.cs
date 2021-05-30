using System.Threading.Tasks;
using WebServer.Server;

namespace WebServer
{
    public class StartUp
    {
        static async Task Main(string[] args)
        {
            var server = new HttpServer("127.0.0.1", 9999);

            await server.Start();
        }
    }
}
