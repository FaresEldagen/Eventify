using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Eventify.Validators
{
    public class EndDateAfterStartAttribute : ValidationAttribute
    {
        private readonly string _startDateProperty;

        public EndDateAfterStartAttribute(string startDateProperty)
        {
            _startDateProperty = startDateProperty;
            ErrorMessage = "End Date must be at least 1 hour after the Start Date.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            DateTime endDate = (DateTime)value;

            // Get Start Date property
            PropertyInfo? startDateProp = validationContext.ObjectType.GetProperty(_startDateProperty);

            if (startDateProp == null)
                return new ValidationResult($"Unknown property: {_startDateProperty}");

            DateTime startDate = (DateTime)startDateProp.GetValue(validationContext.ObjectInstance);

            // Compare (End must be >= Start + 1 hour)
            if (endDate < startDate.AddHours(1))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
