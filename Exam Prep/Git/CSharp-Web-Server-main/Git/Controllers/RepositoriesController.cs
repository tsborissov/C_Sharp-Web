using Git.Data;
using Git.Data.Models;
using Git.Models.Repositories;
using Git.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Linq;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext data;

        public RepositoriesController(IValidator validator, GitDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [HttpGet]
        public HttpResponse All()
        {
            var repositoriesQuery = this.data.Repositories.AsQueryable();

            if (!this.User.IsAuthenticated)
            {
                repositoriesQuery = repositoriesQuery.Where(r => r.IsPublic);
            }
            else
            {
                repositoriesQuery = repositoriesQuery.Where(r => r.IsPublic || r.OwnerId == this.User.Id);
            }

            var repositories = repositoriesQuery
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new AllRepositoriesListingModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Owner = r.Owner.Username,
                    CreatedOn = r.CreatedOn.ToLocalTime().ToString("G"),
                    CommitsCount = r.Commits.Count()
                })
                .ToList();
            
            return View(repositories);
        }

        [HttpGet]
        [Authorize]
        public HttpResponse Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(RepositoryCreateModel model)
        {
            var modelErrors = this.validator.ValidateRepositoryCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var repository = new Repository 
            {
                Name = model.Name,
                IsPublic = model.RepositoryType == "Public",
                OwnerId = this.User.Id,
                //CreatedOn = DateTime.Now,
            };

            this.data.Repositories.Add(repository);

            this.data.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
}
