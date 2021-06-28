
using System.Collections.Generic;

namespace CarShop.Models.Issues
{
    public class CarIssuesViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public IEnumerable<IssueListingViewModel> Issues { get; set; }
    }
}
