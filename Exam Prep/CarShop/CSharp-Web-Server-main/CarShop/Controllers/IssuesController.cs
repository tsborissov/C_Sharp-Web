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
        private readonly CarShopDbContext data;

        public IssuesController(IValidator validator, IUserService userService, CarShopDbContext data)
        {
            this.validator = validator;
            this.userService = userService;
            this.data = data;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            if (!this.userService.IsMechanic(this.User.Id))
            {
                var userOwnsCar = this.data.Cars
                    .Any(c => c.Id == carId && c.OwnerId == this.User.Id);

                if (!userOwnsCar)
                {
                    return Error("Car is not existing or not owned by current user!");
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
                    Issues = c.Issues
                        .Select(i => new IssueListingViewModel 
                        {
                            Id = i.Id,
                            Description = i.Description,
                            IsFixed = i.IsFixed
                        })
                })
                .FirstOrDefault();

            if (carWithIssues == null)
            {
                return Error($"Car with ID '{carId}' does not exist.");
            }

            return View(carWithIssues);
        }

        [HttpGet]
        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddIssueFormModel model)
        {
            if (this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var modelErrors = this.validator.ValidateIssueRegistration(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var issue = new Issue 
            {
                Description = model.Description,
                CarId = model.CarId,
            };

            this.data.Issues.Add(issue);

            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={model.CarId}");
        }

        [Authorize]
        public HttpResponse Delete(string carId, string issueId)
        {
            if (this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var targetIssue = this.data
                .Issues
                .Where(i => i.Id == issueId)
                .FirstOrDefault();

            this.data.Issues.Remove(targetIssue);

            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Fix(string carId, string issueId)
        {
            if (!this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var targetIssue = this.data
                .Issues
                .Where(i => i.Id == issueId)
                .FirstOrDefault();

            targetIssue.IsFixed = true;

            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
