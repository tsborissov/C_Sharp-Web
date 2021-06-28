using CarShop.Models.Cars;
using CarShop.Models.Issues;
using CarShop.Models.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using static CarShop.Data.DataConstants;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCarCreation(AddCarFormModel model)
        {
            var errors = new List<string>();

            if (model.Model.Length < CarModelMinLength || model.Model.Length > DefaultMaxLength)
            {
                errors.Add($"Model '{model.Model}' is not valid.");
            }

            if (model.Year < CarYearMinValue || model.Year > CarYearMaxValue)
            {
                errors.Add($"Year '{model.Year}' is not valid.");
            }

            
            if (!Regex.IsMatch(model.PlateNumber, CarPlateNumberRegularExpression))
            {   
                errors.Add($"Plate number '{model.PlateNumber}' is not valid.");
            }

            return errors;
        }

        public ICollection<string> ValidateIssueRegistration(AddIssueFormModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < IssueDescriptionMinLength)
            {
                errors.Add($"Issue description '{model.Description}' is not valid.");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(RegisterUserFormModel model)
        {
            var errors = new List<string>();
            
            if (model.Username.Length < UserMinUsername || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{model.Email}' is not valid.");
            }

            if (model.Password.Length < UserMinPassword || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"Password is not valid.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Passwords do not match.");
            }

            if (model.UserType != UserTypeClient && model.UserType != UserTypeMechanic)
            {
                errors.Add($"User type '{model.UserType}' is not valid.");
            }

            return errors;
        }
    }
}
