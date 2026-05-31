using System.ComponentModel.DataAnnotations;

namespace MFui.Data
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateOnly dateValue)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

                if (dateValue > today)
                {
                    return new ValidationResult(ErrorMessage ?? "The date must be today or in the past.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
