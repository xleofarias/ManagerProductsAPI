using System.ComponentModel.DataAnnotations;

namespace ManagerProductsAPI.Extras
{
    public class NoSpecialCharactersAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string str && str.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return new ValidationResult("O campo não deve conter caracteres especiais.");
            }
            return ValidationResult.Success;
        }
    }

}
