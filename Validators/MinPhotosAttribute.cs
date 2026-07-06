using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Eventify.Validators
{
    public class MinPhotosAttribute : ValidationAttribute
    {
        private readonly int _min;

        public MinPhotosAttribute(int min)
        {
            _min = min;
            ErrorMessage = $"You must upload at least {_min} photo(s).";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var files = value as List<IFormFile>;

            if (files == null || files.Count < _min)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
