using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Models.Cars;
using CarShop.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly IValidator validator;
        private readonly IUserService userService;
        private readonly CarShopDbContext data;

        public CarsController(IValidator validator, IUserService userService, CarShopDbContext data)
        {
            this.validator = validator;
            this.userService = userService;
            this.data = data;
        }

        [HttpGet]
        [Authorize]
        public HttpResponse Add()
        {
            if (this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }
            
            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddCarFormModel model)
        {
            if (this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var modelErrors = this.validator.ValidateCarCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id
            };

            this.data.Cars.Add(car);

            this.data.SaveChanges();

            return Redirect("/Cars/All");
        }

        [HttpGet]
        [Authorize]
        public HttpResponse All()
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (this.userService.IsMechanic(this.User.Id))
            {
                carsQuery = this.data.Cars.Where(c => c.Issues.Any(i => !i.IsFixed));
            }
            else
            {
                carsQuery = this.data.Cars.Where(c => c.OwnerId == this.User.Id);
            }

            var cars = carsQuery
                .Select(c => new AllCarsModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    PlateNumber = c.PlateNumber,
                    Image = c.PictureUrl,
                    RemainingIssues = c.Issues.Where(issue => !issue.IsFixed).Count(),
                    FixedIssues = c.Issues.Where(issue => issue.IsFixed).Count()
                })
                    .ToList();

            return View(cars);
        }
    }
}
