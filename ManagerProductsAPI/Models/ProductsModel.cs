using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ManagerProductsAPI.Models
{
    public class ProductsModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "O tamanho máximo para o nome é de 50 caracteres")]
        public string Nome { get; set; }

        [StringLength(150, ErrorMessage = "O tamanho máximo para a descrição é de 150 caracteres")]
        public string Descricao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O Estoque precisa estar acima de 1")]
        public float Preco { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O estoque precisar estar em 1")]
        public int EstoqueQtd { get; set; }
        public DateTime DataDeEntrada { get; set; }
    }
}
