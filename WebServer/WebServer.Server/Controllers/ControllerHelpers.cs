using System;

namespace WebServer.Server.Controllers
{
    public static class ControllerHelpers
    {
        public static string GetControllerName(this Type type)
            => type.Name.Replace(nameof(Controller), string.Empty);
    }
}
