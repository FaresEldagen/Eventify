using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Eventify.Models.Entities;

namespace Eventify.Validators
{
    public class EnsureAtLeastOnePhotoAttribute : ValidationAttribute
    {
        private readonly string _deletedPhotosProperty;
        private readonly string _originalCountProperty;

        public EnsureAtLeastOnePhotoAttribute(string originalCountProperty, string deletedPhotosProperty)
        {
            _deletedPhotosProperty = deletedPhotosProperty;
            _originalCountProperty = originalCountProperty;
            ErrorMessage = "You must keep at least one existing photo or upload a new one.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var formFiles = value as List<IFormFile>;

            var deletedProp = validationContext.ObjectType.GetProperty(_deletedPhotosProperty);
            var originalCountProp = validationContext.ObjectType.GetProperty(_originalCountProperty);

            int originalCount = (int)originalCountProp.GetValue(validationContext.ObjectInstance);
            var deletedPhotos = deletedProp.GetValue(validationContext.ObjectInstance) as List<string>;

            int deletedCount = deletedPhotos?.Count ?? 0;
            int newUploadedCount = formFiles?.Count ?? 0;

            // If all original photos deleted AND no new photos uploaded
            if (deletedCount == originalCount && newUploadedCount == 0)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
