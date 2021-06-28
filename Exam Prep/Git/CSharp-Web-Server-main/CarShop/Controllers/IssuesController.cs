using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Models.Issues;
using CarShop.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IValidator validator;
        private readonly IUserService userService;
        private readonly ApplicationDbContext data;

        public IssuesController(
            IValidator validator,
            IUserService userService,
            ApplicationDbContext data)
        {
            this.validator = validator;
            this.userService = userService;
            this.data = data;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            var userIsMechanic = this.userService.UserIsMechanic(this.User.Id);

            if (!userIsMechanic)
            {
                if (!this.userService.CarIsOwnedByUser(carId, this.User.Id))
                {
                    return Error($"Invalid car");
                }
            }
            
            var carWithIssues = this.data
                .Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarIssuesViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    UserIsMechanic = userIsMechanic,
                    Issues = c.Issues
                        .Select(i => new IssueListingViewModel
                        {
                            Id = i.Id,
                            Description = i.Description,
                            IsFixedInformation = i.IsFixed ? "Yes" : "Not yet"
                        })
                })
                .FirstOrDefault();

            if (carWithIssues == null)
            {
                return NotFound();
            }
            
            return View(carWithIssues);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddIssueFormModel model)
        {
            if (!this.userService.UserIsMechanic(this.User.Id))
            {
                if (!this.userService.CarIsOwnedByUser(model.CarId, this.User.Id))
                {
                    return Error($"Invalid car");
                }
            }

            var modelErrors = this.validator.ValidateIssue(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var issue = new Issue
            {
                Description = model.Description,
                CarId = model.CarId
            };

            this.data.Issues.Add(issue);

            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={model.CarId}");
        }
    }
}
