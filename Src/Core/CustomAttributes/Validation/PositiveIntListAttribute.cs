using System.ComponentModel.DataAnnotations;

namespace MovieAppApi.Src.Core.CustomAttributes.Validation;

public class PositiveIntListAttribute : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    if (value is not List<int> list)
    {
      return new ValidationResult("The field must be a list of integers.");
    }

    foreach (var number in list)
    {
      if (number <= 0)
      {
        return new ValidationResult("All elements in the list must be strictly positive integers.");
      }
    }

    return ValidationResult.Success;
  }
}