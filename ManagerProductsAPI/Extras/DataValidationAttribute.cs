using ManagerProductsAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace ManagerProductsAPI.Extras
{
    public class DataValidationAttribute : ValidationAttribute 
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var contextDb = validationContext.GetService(typeof(ProductsDBContext)) as ProductsDBContext;

            var validationDate = contextDb.Products.Any(p => p.DataDeEntrada <= DateTime.Parse(value.ToString()));

            return validationDate ? new ValidationResult("A data de Entrada é anterior a data já cadastrada, não será possível atualizar") : ValidationResult.Success;

        }
    }
}
