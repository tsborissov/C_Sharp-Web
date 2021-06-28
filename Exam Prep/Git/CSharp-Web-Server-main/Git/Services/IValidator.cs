using Git.Models.Commits;
using Git.Models.Repositories;
using Git.Models.Users;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(UserRegisterModel model);

        ICollection<string> ValidateRepositoryCreation(RepositoryCreateModel model);

        ICollection<string> ValidateCommitCreation(CommitCreateModel model);
    }
}
