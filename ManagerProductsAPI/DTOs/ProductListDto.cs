using System.ComponentModel.DataAnnotations;

namespace ManagerProductsAPI.DTOs
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int EstoqueQtd { get; set; }
        public DateTime DataDeEntrada { get; set; }
    }
}
