using WebServer.Server.Controllers;
using WebServer.Server.Http;

namespace WebServer.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse ActionWithCookies()
        {
            const string CookieName = "My-First-Cookie";

            if (this.Request.Cookies.ContainsKey(CookieName))
            {
                var cookie = this.Request.Cookies[CookieName];
                
                return Text($"Cookie already exists - {cookie}.");
            }
            
            this.Response.AddCookie(CookieName, "My-First-Value");
            this.Response.AddCookie("My-Second-Cookie", "My-Second-Value");

            return Text("Cookies set.");
        }
    }
}
