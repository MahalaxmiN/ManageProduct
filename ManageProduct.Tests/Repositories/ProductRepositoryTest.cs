using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManageProduct.Data;
using ManageProduct.Entities;
using ManageProduct.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManageProduct.Tests.Repositories
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public async Task Add_Get_Product_Success()
        {
            using var context = GetInMemoryDbContext();
            var repo = new ProductRepository(context);
            var product = new Product { Name = "Test", Price = 10, StockAvailable = 5 };
            await repo.AddAsync(product);
            var result = await repo.GetByIdAsync(product.Id);
		     Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Name);
        }

        [TestMethod]
        public async Task Update_Product_Success()
        {
            using var context = GetInMemoryDbContext();
            var repo = new ProductRepository(context);
            var product = new Product { Name = "Test", Price = 10, StockAvailable = 5 };
            await repo.AddAsync(product);
            product.Name = "Updated";
            await repo.UpdateAsync(product);
            var result = await repo.GetByIdAsync(product.Id);
            Assert.AreEqual("Updated", result.Name);
        }

        [TestMethod]
        public async Task Delete_Product_Success()
        {
            using var context = GetInMemoryDbContext();
            var repo = new ProductRepository(context);
            var product = new Product { Name = "Test", Price = 10, StockAvailable = 5 };
            await repo.AddAsync(product);
            await repo.DeleteAsync(product.Id);
            var result = await repo.GetByIdAsync(product.Id);
            Assert.IsNull(result);
        }

		private ProductDbContext GetInMemoryDbContext()
		{
			var options = new DbContextOptionsBuilder<ProductDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb")
				.Options;
			return new ProductDbContext(options);
		}
	}
}
