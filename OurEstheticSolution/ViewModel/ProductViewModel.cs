
using System;
using System.ComponentModel.DataAnnotations;


namespace OurEstheticSolution.ViewModel
{
  
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        public string ? Name { get; set; } = string.Empty;

        public string ? Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Expiry date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Manufacturer is required.")]
        public string ? ManufacturedBy { get; set; } 

        [Required(ErrorMessage = "Service ID is required.")]
        public Guid ? ServiceId { get; set; } 

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }

}
