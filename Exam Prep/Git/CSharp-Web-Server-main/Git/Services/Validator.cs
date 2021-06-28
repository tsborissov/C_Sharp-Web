
using Git.Models.Commits;
using Git.Models.Repositories;
using Git.Models.Users;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Git.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCommitCreation(CommitCreateModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < 5)
            {
                errors.Add("Invalid commit description.");
            }

            return errors;
        }

        public ICollection<string> ValidateRepositoryCreation(RepositoryCreateModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < 3 || model.Name.Length > 40)
            {
                errors.Add("Invalid repository name.");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(UserRegisterModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < 5 || model.Username.Length > 20)
            {
                errors.Add($"Invalid username length.");
            }

            if (!Regex.IsMatch(model.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                errors.Add($"Invalid email.");
            }

            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                errors.Add($"Invalid password.");
            }

            if (model.ConfirmPassword.Length < 6  || model.ConfirmPassword.Length > 20 || model.ConfirmPassword != model.Password)
            {
                errors.Add($"Invalid confirm password.");
            }

            return errors;
        }
    }
}