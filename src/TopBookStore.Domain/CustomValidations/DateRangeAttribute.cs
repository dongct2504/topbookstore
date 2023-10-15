using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Domain.CustomValidations;

public class DateRangeAttribute : ValidationAttribute
{
    private readonly DateTime _minDate;
    private readonly DateTime _maxDate;

    public DateRangeAttribute()
    {
        _minDate = new DateTime(1000, 1, 1);
        _maxDate = DateTime.Now;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue)
        {
            if (dateValue < _minDate || dateValue > _maxDate)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}