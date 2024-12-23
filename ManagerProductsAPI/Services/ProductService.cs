using ManagerProductsAPI.Data;
using ManagerProductsAPI.DTOs;
using ManagerProductsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerProductsAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsDBContext _productsDBContext;

        public ProductService(ProductsDBContext productService) 
        {
            _productsDBContext = productService;
        }

        public async Task<IEnumerable<ProductsModel>> GetAllProductsAsync() => await _productsDBContext.Products.ToListAsync();

        public async Task<ProductsModel> GetProductByIdAsync(int id) => await _productsDBContext.Products.FindAsync(id);

        public async Task<ProductsModel> CreateProductAsync(ProductCreateDto createProduct) 
        {
            var newProduct = new ProductsModel
            {   
                Nome = createProduct.Nome,
                Descricao = createProduct.Descricao,
                Preco = createProduct.Preco,
                EstoqueQtd = createProduct.EstoqueQtd,
                DataDeEntrada = DateTime.UtcNow,   
            };

            await _productsDBContext.Products.AddAsync(newProduct);
            await _productsDBContext.SaveChangesAsync();

            return newProduct;
        }

        public async Task<ProductsModel> UpdateProductAsync(int id, ProductUpdateDto updateProduct) 
        {
            var existingproduct = await _productsDBContext.Products.FindAsync(id);

            if (existingproduct == null) 
            {
                throw new KeyNotFoundException("Produto não encontrado");
            }

            existingproduct.Descricao = updateProduct.Descricao;
            existingproduct.Preco = updateProduct.Preco;
            existingproduct.DataDeEntrada = updateProduct.DataDeEntrada;
            existingproduct.EstoqueQtd = updateProduct.EstoqueQtd;

            await _productsDBContext.SaveChangesAsync();

            return existingproduct;
        }

        public async Task DeleteProductAsync(int id) 
        {
            var existingProduct = await _productsDBContext.Products.FindAsync(id);
            if(existingProduct == null) 
            {
                throw new KeyNotFoundException("Produto não encontrado");
            }
            
            _productsDBContext.Remove(existingProduct);
             
            await _productsDBContext.SaveChangesAsync();
        }

        public async Task DeleteAllProductsAsync()
        {
            await _productsDBContext.Products.ExecuteDeleteAsync();
            await _productsDBContext.SaveChangesAsync();
        }
    }
}
