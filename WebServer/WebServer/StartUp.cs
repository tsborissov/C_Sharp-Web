﻿using System.Threading.Tasks;
using WebServer.Controllers;
using WebServer.Server;
using WebServer.Server.Controllers;

namespace WebServer
{
    public class StartUp
    {
        static async Task Main(string[] args)
            => await new HttpServer(routes => routes
                .MapGet<HomeController>("/", c => c.Index())
                .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
                .MapGet<HomeController>("/ToCats", c => c.LocalRedirect())
                .MapGet<AnimalsController>("/Cats", c => c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs()))
            .Start();
    }
}