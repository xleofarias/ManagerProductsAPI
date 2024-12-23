using ManagerProductsAPI.DTOs;
using ManagerProductsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagerProductsAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsModel>> GetAllProductsAsync();
        Task<ProductsModel> GetProductByIdAsync(int id);
        Task<ProductsModel> CreateProductAsync(ProductCreateDto productsModel);
        Task<ProductsModel> UpdateProductAsync(int id, ProductUpdateDto productsModel);
        Task DeleteProductAsync(int id);
        Task DeleteAllProductsAsync();
    }
}
