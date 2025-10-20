using Microsoft.EntityFrameworkCore;
using ManageProduct.Entities;

namespace ManageProduct.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }
}
