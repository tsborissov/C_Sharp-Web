using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models.Trips;
using SharedTrip.Services;
using System;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext data;

        public TripsController(
            IValidator validator,
            ApplicationDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse All()
        {
            var trips = this.data
                .Trips
                .OrderByDescending(t => t.DepartureTime)
                .Select(t => new TripsListingViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats
                })
                .ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripFormModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return Redirect("/Trips/Add");
            }

            var trip = new Trip
            {
                DepartureTime = DateTime.Parse(model.DepartureTime),
                Seats = model.Seats,
                StartPoint = model.StartPoint,
                Description = model.Description,
                EndPoint = model.EndPoint,
                ImagePath = model.ImagePath,
            };

            data.Trips.Add(trip);

            data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = this.data
                .Trips
                .Where(t => t.Id == tripId)
                .Select(t => new TripDetailsViewModel
                {
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Description = t.Description,
                    EndPoint = t.EndPoint,
                    Seats = t.Seats,
                    StartPoint = t.StartPoint,
                    Id = tripId,
                    ImagePath = t.ImagePath
                })
                .FirstOrDefault();

            return View(trip);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var targetTrip = this.data.Trips.Find(tripId);

            var isAlreadyJoined = this.data
                .UserTrips
                .Any(t => t.TripId == tripId && t.UserId == this.User.Id);

            var anyFreeSeats = this.data
                .Trips
                .Any(t => t.Id == tripId && t.Seats > 0);

            var canJoin = anyFreeSeats && !isAlreadyJoined;


            if (!canJoin)
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            targetTrip.Seats--;

            targetTrip.UserTrips.Add(new UserTrip { UserId = this.User.Id });

            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }
    }
}