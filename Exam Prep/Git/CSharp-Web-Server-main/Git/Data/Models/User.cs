using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class User
    {
        [Required]
        [MaxLength(40)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<Repository> Repositories { get; set; } = new HashSet<Repository>();

        public IEnumerable<Commit> Commits { get; set; } = new HashSet<Commit>();
    }
}

/*
 •	Has an Id – a string, Primary Key
•	Has a Username – a string with min length 5 and max length 20 (required)
•	Has an Email - a string (required)
•	Has a Password – a string with min length 6 and max length 20  - hashed in the database (required)
•	Has Repositories collection – a Repository type
•	Has Commits collection – a Commit type
 */
