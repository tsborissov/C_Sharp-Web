using WebServer.Server.Http;
using WebServer.Server.Responses;

namespace WebServer.Server.Controllers
{
    public abstract class Controller
    {
        public Controller(HttpRequest request)
            => this.Request = request;
        
        protected HttpRequest Request { get; private set; }

        protected HttpResponse Text(string text)
            => new TextResponse(text);

        protected HttpResponse Html(string html)
            => new HtmlResponse(html);
    }
}
