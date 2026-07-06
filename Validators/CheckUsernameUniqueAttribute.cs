using Eventify.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Eventify.Validators
{
    public class CheckUsernameUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var context = validationContext.GetService<AppDbContext>();
                var isExist = context!.Users.Any(u => u.UserName == value.ToString());
                if (isExist)
                {
                    return new ValidationResult("This Username already Exists");
                }
                return ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
