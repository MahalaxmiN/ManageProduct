using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageProduct.Entities
{
    public class Product
    {
        /// <summary>
        /// Unique product Id, 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } 

        /// <summary>
        /// Description of the product.
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Stock available for the product.
        /// </summary>
        public int StockAvailable { get; set; }

        /// <summary>
        /// Date the product was created.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
