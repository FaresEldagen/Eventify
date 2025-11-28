using Eventify.Data;
using Eventify.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Eventify.Validators
{
    public class CheckEmailUniqueAttribute:ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                var db = validationContext.GetService<AppDbContext>();

                var isExsit = db!.Users.Any(u => u.Email == value!.ToString());
                if (isExsit)
                {
                    return new ValidationResult("this email is already exists");
                }
                return ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
