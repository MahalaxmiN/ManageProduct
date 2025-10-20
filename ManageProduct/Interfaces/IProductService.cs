using ManageProduct.Entities;

namespace ManageProduct.Interfaces
{
	public interface IProductService
	{
		Task<Product?> GetProductByIdAsync(int id);
		Task<List<Product>> GetAllProductsAsync();
		Task<Product> CreateProductAsync(Product product);
		Task<Product?> UpdateProductAsync(int id, Product product);
		Task<bool> DeleteProductAsync(int id);
		Task<bool> DecrementStockAsync(int id, int quantity);
		Task<bool> AddToStockAsync(int id, int quantity);
	}
}
