using ManagerProductsAPI.Extras;
using System.ComponentModel.DataAnnotations;

namespace ManagerProductsAPI.DTOs
{
    public class ProductUpdateDto
    {
        [StringLength(100)]
        public string Descricao { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "O Preço precisa se acima ou igual a 1")]
        public float Preco { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O Estoque precisa estar acima de 1")]
        public int EstoqueQtd { get; set; }

        [DataValidation]
        [Required]
        public DateTime DataDeEntrada { get; set; }
    }
}
