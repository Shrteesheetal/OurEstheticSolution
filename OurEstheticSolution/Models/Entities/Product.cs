using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurEstheticSolution.Models.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters")]
        public string? Name { get; set; }

        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Manufacturer name is required")]
        [StringLength(100, ErrorMessage = "Manufacturer name cannot be longer than 100 characters")]
        public string? ManufacturedBy { get; set; }

        [Required(ErrorMessage = "Service ID is required")]
        [StringLength(50, ErrorMessage = "Service ID cannot be longer than 50 characters")]
        public string? ServiceId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
