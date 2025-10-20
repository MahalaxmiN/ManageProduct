using ManageProduct.Entities;
using ManageProduct.Interfaces;
using ManageProduct.Repositories;
using System.Threading.Tasks;

namespace ManageProduct.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _repository.AddAsync(product);
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var recordFromDb = await _repository.GetByIdAsync(id);
            if (recordFromDb == null) return null;
			recordFromDb.Name = product.Name;
            recordFromDb.Description = product.Description;
            recordFromDb.Price = product.Price;
			recordFromDb.StockAvailable = product.StockAvailable;
            await _repository.UpdateAsync(recordFromDb);
            return recordFromDb;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var recordFromDb = await _repository.GetByIdAsync(id);
            if (recordFromDb == null) return false;
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> DecrementStockAsync(int id, int quantity)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null || product.StockAvailable < quantity) return false;
            product.StockAvailable -= quantity;
            await _repository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> AddToStockAsync(int id, int quantity)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return false;
            product.StockAvailable += quantity;
            await _repository.UpdateAsync(product);
            return true;
        }
    }
}
