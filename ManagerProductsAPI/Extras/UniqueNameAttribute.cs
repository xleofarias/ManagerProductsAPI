using ManagerProductsAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace ManagerProductsAPI.Extras
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dbContext = (ProductsDBContext) validationContext.GetService(typeof(ProductsDBContext));
            var exists = dbContext.Products.Any(p => p.Nome == value.ToString());

            return exists ? new ValidationResult("O nome já existe.") : ValidationResult.Success;
        }
    }
}
