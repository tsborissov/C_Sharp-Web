namespace Git.Models.Repositories
{
    public class AllRepositoriesListingModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CreatedOn { get; set; }

        public string Owner { get; set; }

        public int CommitsCount { get; set; }
    }
}
