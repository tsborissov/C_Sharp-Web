using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class Repository
    {
        [Required]
        [MaxLength(40)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsPublic { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public IEnumerable<Commit> Commits { get; set; } = new HashSet<Commit>();
    }
}

/*
•	Has an Id – a string, Primary Key
•	Has a Name – a string with min length 3 and max length 10 (required)
•	Has a CreatedOn – a datetime (required)
•	Has a IsPublic – bool (required)
•	Has a OwnerId – a string
•	Has a Owner – a User object
•	Has Commits collection – a Commit type

 */