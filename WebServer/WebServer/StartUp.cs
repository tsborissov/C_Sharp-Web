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
                .MapGet<HomeController>("/", c => c.Index())
                .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
                .MapGet<HomeController>("/ToCats", c => c.LocalRedirect())
                .MapGet<HomeController>("/Error", c => c.Error())
                .MapGet<HomeController>("/StaticFiles", c => c.StaticFiles())
                .MapGet<AnimalsController>("/Cats", c => c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
                .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
                .MapGet<AccountController>("/Login", c => c.Login())
                .MapGet<AccountController>("/Logout", c => c.Logout())
                .MapGet<AccountController>("/Cookies", c => c.CookiesCheck())
                .MapGet<AccountController>("/Session", c => c.SessionCheck())
                .MapGet<AccountController>("/Authentication", c => c.AuthenticationCheck())
                .MapGet<CatsController>("/Cats/Create", c => c.Create())
                .MapPost<CatsController>("/Cats/Save", c => c.Save()))
            .Start();
    }
}