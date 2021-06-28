namespace CarShop.Models.Cars
{
    public class AllCarsModel
    {
        public string Id { get; set; }

        public string Model { get; init; }

        public int Year { get; init; }

        public string PlateNumber { get; set; }

        public string Image { get; set; }

        public int FixedIssues { get; init; }

        public int RemainingIssues { get; init; }
    }
}
