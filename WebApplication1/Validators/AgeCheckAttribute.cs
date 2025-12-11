using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Validators
{
    public class AgeCheckAttribute:ValidationAttribute
    {
       protected override ValidationResult ? IsValid(dynamic ? value, ValidationContext validationContext)
        {
            if(value == null || (value < 18) || (value >61))
            {
                return new ValidationResult("Employee Age Should Be more Than 18 and less tha 61....");
            }
            return ValidationResult.Success;
        }
    }
}
