using System.ComponentModel.DataAnnotations;

namespace ManageProduct.DTOs
{
    public class ProductGenerateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } 

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockAvailable { get; set; }
    }
}
