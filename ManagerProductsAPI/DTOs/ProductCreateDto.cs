using ManagerProductsAPI.Extras;
using System.ComponentModel.DataAnnotations;

namespace ManagerProductsAPI.DTOs
{
    public class ProductCreateDto
    {
        [UniqueName]
        [Required(ErrorMessage ="Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O tamanho máximo para o nome é de 50 caracteres")]
        [NoSpecialCharacters]
        public string Nome { get; set; }

        [StringLength(150,ErrorMessage= "O tamanho máximo para a descrição é de 150 caracteres")]
        public string Descricao { get; set; }

        [Required]
        [Range(1,float.MaxValue, ErrorMessage = "O preço precisa estar em 1")]
        public float Preco { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O estoque precisar estar em 1")]
        public int EstoqueQtd { get; set; }
    }
}
