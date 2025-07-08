using System.ComponentModel.DataAnnotations;

namespace OurEstheticSolution.ViewModel
{
    public class AppointmentViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        public string? CustomerId { get; set; }

        [Required(ErrorMessage = "Employee is required")]
        public string? EmployeeId { get; set; }

        [Required(ErrorMessage = "Service is required")]
        public string? ServiceId { get; set; }

        [Required(ErrorMessage = "Appointment time is required")]
        [DataType(DataType.Time)]
        public DateTime AppointmentTime { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "CreatedBy is required.")]
        [StringLength(100, ErrorMessage = "CreatedBy cannot be longer than 100 characters.")]
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

    }
}
