using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManageProduct.Data;
using ManageProduct.Entities;
using ManageProduct.Repositories;
using ManageProduct.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ManageProduct.Tests.Services
{
    [TestClass]
    public class ProductServiceTest
    {
       
        [TestMethod]
        public async Task CreateProductAsync_Add_Success()
        {
            using var context = GetInMemoryDbContext();
            var service = GetService(context);
            var product = new Product { Name = "Test", Price = 10, StockAvailable = 5 };
            var result = await service.CreateProductAsync(product);
			Assert.IsTrue(result.Id > 0);
			Assert.AreEqual("Test", result.Name);
            Assert.AreEqual(1, context.Products.Count());
        }

        [TestMethod]
        public async Task DecrementStockAsync_Delete_Success()
        {
            using var context = GetInMemoryDbContext();
            var service = GetService(context);
            var product = new Product { Name = "Test", Price = 10, StockAvailable = 5 };
            var result = await service.CreateProductAsync(product);
            var resultWithDecrement = await service.DecrementStockAsync(result.Id, 2);
            Assert.IsTrue(resultWithDecrement);
            var getRecord = await service.GetProductByIdAsync(result.Id);
            Assert.AreEqual(3, getRecord.StockAvailable);
        }

        [TestMethod]
        public async Task DecrementStockAsync_Delete_InsufficientStock_Failure()
        {
            using var context = GetInMemoryDbContext();
            var service = GetService(context);
            var product = new Product { Name = "Test", Price = 10, StockAvailable = 1 };
            var result = await service.CreateProductAsync(product);
            var resultWithDecrement = await service.DecrementStockAsync(result.Id, 2);
            Assert.IsFalse(resultWithDecrement);
        }

		private ProductService GetService(ProductDbContext context)
		{
			var repo = new ProductRepository(context);
			return new ProductService(repo);
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
