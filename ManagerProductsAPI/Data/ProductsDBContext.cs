using ManagerProductsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagerProductsAPI.Data
{
    public class ProductsDBContext : DbContext
    {
        public ProductsDBContext(DbContextOptions<ProductsDBContext> options) : base(options) { }
        public DbSet<ProductsModel> Products { get; set; }
    }
}
