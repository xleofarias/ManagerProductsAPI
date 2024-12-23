using Microsoft.AspNetCore.Mvc;
using ManagerProductsAPI.Models;
using ManagerProductsAPI.DTOs;
using ManagerProductsAPI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ManagerProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpGet("produtos")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (products == null)
                    return NoContent();

                var productsDto = products.Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Preco = (decimal)p.Preco,
                    EstoqueQtd = p.EstoqueQtd,
                    DataDeEntrada = p.DataDeEntrada
                });

                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Status interno no Servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id) 
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var product = await _productService.GetProductByIdAsync(id);

                if (product == null)
                    return NotFound("Produto não encontrado");

                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            { 
                Console.Write(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductCreateDto newProduct) 
        {
            try
            {
                if (!ModelState.IsValid) 
                    return BadRequest(ModelState);

                var createdProduct = await _productService.CreateProductAsync(newProduct);

                Console.WriteLine($"Rota: api/Products/{createdProduct.Id}");
            
                return Created($"api/Products/{createdProduct.Id}", createdProduct);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Erro Interno do Servidor");
            }
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult>  UpdateProductAsync(int id, [FromBody] ProductUpdateDto productUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState); // 400 Bad Request

                if (productUpdate == null)
                    return NotFound("Produto não encontrado");

                var updateProduct = await _productService.UpdateProductAsync(id, productUpdate);
                return Ok($"Produto com o ID: {updateProduct.Id} atualizado com sucesso");
            }
            catch (KeyNotFoundException ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
                return NoContent();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
               return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteProductAsync(int id) 
        {
            try 
            {
                if(id == 0) 
                    return NotFound();

                await _productService.DeleteProductAsync(id);
                return Ok($"Produto com o ID: {id} deletado com sucesso");
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound("Produto não encontrado"); // 404 Not Found
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro interno do Servidor");
            }
        }

        [HttpDelete("deleteAll")]
        public async Task<IActionResult> DeleteAllProductsAsync() 
        {
            try 
            {
                await _productService.DeleteAllProductsAsync();
                return Ok("Todos os produtos foram excluídos");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
