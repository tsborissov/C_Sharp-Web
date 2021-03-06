using WebServer.Models.Animals;
using WebServer.Server.Controllers;
using WebServer.Server.Http;

namespace WebServer.Controllers
{
    public class DogsController : Controller
    {
        [HttpGet]
        public HttpResponse Create() => View();

        [HttpPost]
        public HttpResponse Create(DogFormModel model)
            => Text($"Dog: {model.Name} - {model.Age} - {model.Breed}");

    }
}
