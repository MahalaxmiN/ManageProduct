using ManageProduct.DTOs;
using ManageProduct.Entities;
using ManageProduct.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ManageProduct.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _service;
		private readonly ILogger<ProductController> _logger;

		public ProductController(IProductService service, ILogger<ProductController> logger)
		{
			_service = service;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromBody] ProductGenerateDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.Name))
				return BadRequest("Product name is missing and its required.");
			if (dto.Price < 0)
				return BadRequest("Product price must be valid number.");
			if (dto.StockAvailable < 0)
				return BadRequest("Stock available must be valid number.");

			var product = new Product
			{
				Name = dto.Name,
				Description = dto.Description,
				Price = dto.Price,
				StockAvailable = dto.StockAvailable
			};
			var created = await _service.CreateProductAsync(product);
			var result = new ProductDto
			{
				Id = created.Id,
				Name = created.Name,
				Description = created.Description,
				Price = created.Price,
				StockAvailable = created.StockAvailable,
				CreatedDate = created.CreatedDate
			};
			return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			var products = await _service.GetAllProductsAsync();
			if (!products.Any())
				return NotFound("No products found.");
			var result = products.Select(p => new ProductDto
			{
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Price = p.Price,
				StockAvailable = p.StockAvailable,
				CreatedDate = p.CreatedDate
			});
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var product = await _service.GetProductByIdAsync(id);
			if (product == null) return NotFound($"Product with id {id} is not present.");
			var result = new ProductDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				StockAvailable = product.StockAvailable,
				CreatedDate = product.CreatedDate
			};
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductGenerateDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.Name))
				return BadRequest("Product name is missing and is required field.");
			if (dto.Price < 0)
				return BadRequest("Product price must be valid.");
			if (dto.StockAvailable < 0)
				return BadRequest("Stock available must be valid.");

			var product = new Product
			{
				Name = dto.Name,
				Description = dto.Description,
				Price = dto.Price,
				StockAvailable = dto.StockAvailable
			};
			var updated = await _service.UpdateProductAsync(id, product);
			if (updated == null) return NotFound($"Product with id {id} not found.");
			var result = new ProductDto
			{
				Id = updated.Id,
				Name = updated.Name,
				Description = updated.Description,
				Price = updated.Price,
				StockAvailable = updated.StockAvailable,
				CreatedDate = updated.CreatedDate
			};
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var deleted = await _service.DeleteProductAsync(id);
			if (!deleted) return NotFound($"Product with id {id} not found.");
			return NoContent();
		}

		[HttpPut("decrement-stock/{id}/{quantity}")]
		public async Task<IActionResult> DecrementStock(int id, int quantity)
		{
			if (quantity <= 0) return BadRequest("Quantity value must be valid.");
			var result = await _service.DecrementStockAsync(id, quantity);
			if (!result) return BadRequest($"Product with id {id} not found.");

			var product = await _service.GetProductByIdAsync(id);
			if (product == null) return NotFound($"Product with id {id} not found.");

			var dto = new ProductDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				StockAvailable = product.StockAvailable,
				CreatedDate = product.CreatedDate
			};
			return Ok(dto);
		}

		[HttpPut("add-to-stock/{id}/{quantity}")]
		public async Task<IActionResult> AddToStock(int id, int quantity)
		{
			if (quantity <= 0) return BadRequest("Quantity value must be valid.");
			var result = await _service.AddToStockAsync(id, quantity);
			if (!result) return NotFound($"Product with id {id} not found.");

			var product = await _service.GetProductByIdAsync(id);
			if (product == null) return NotFound($"Product with id {id} not found.");

			var dto = new ProductDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				StockAvailable = product.StockAvailable,
				CreatedDate = product.CreatedDate
			};
			return Ok(dto);
		}
	}
}
