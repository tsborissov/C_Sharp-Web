namespace SharedTrip.Models.Trips
{
    public class TripsListingViewModel
    {

        public string Id { get; init; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public string DepartureTime { get; set; }

        public int Seats { get; set; }
    }
}
