using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Eventify.Models.Entities;
using Eventify.ViewModels.EventVM;
using Microsoft.EntityFrameworkCore.Internal;

namespace Eventify.Validators
{
    public class EnsureAtLeastOnePhotoAttribute : ValidationAttribute
    {
        private readonly string _eventPhotosProperty;

        public EnsureAtLeastOnePhotoAttribute(string eventPhotosProperty)
        {
            _eventPhotosProperty = eventPhotosProperty;
            ErrorMessage = "The Event Must Have at Least ONE Photo";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var DeletedPhotos = (List<string>)value!;
            var eventPhotosPropertyInfo = validationContext.ObjectType.GetProperty(_eventPhotosProperty);
            var eventPhotos = eventPhotosPropertyInfo!.GetValue(validationContext.ObjectInstance) as List<IFormFile>;
            if (DeletedPhotos.Count >= eventPhotos!.Count)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;

        }
    }
}
