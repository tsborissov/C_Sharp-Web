using System;
using WebServer.Server.Controllers;
using WebServer.Server.Http;

namespace WebServer.Controllers
{
    public class AccountController : Controller
    {
        public HttpResponse Login()
        {
            var someUserId = "MyUserId";

            this.SignIn(someUserId);

            return Text("User authenticated.");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Text("User signed out.");
        }

        public HttpResponse AuthenticationCheck()
        {
            if (this.User.IsAuthenticated)
            {
                return Text($"Authenticated user: {this.User.Id}");
            }

            return Text("User is not authenticated!");
        }

        [Authorize]
        public HttpResponse AuthorizationCheck()
        {
            return Text($"Current user: {this.User.Id}");
        }

        public HttpResponse CookiesCheck()
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

        public HttpResponse SessionCheck()
        {
            const string CurrentDateKey = "CurrentDate";

            if (this.Request.Session.ContainsKey(CurrentDateKey))
            {
                var currentDate = this.Request.Session[CurrentDateKey];

                return Text($"Stored date: {currentDate}");
            }

            this.Request.Session[CurrentDateKey] = DateTime.UtcNow.ToString();

            return Text("Current date stored.");
        }
    }
}
