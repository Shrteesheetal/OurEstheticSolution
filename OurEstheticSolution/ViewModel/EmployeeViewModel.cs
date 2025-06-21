using System.ComponentModel.DataAnnotations;

namespace OurEstheticSolution.ViewModel
{
   
    public class EmployeeViewModel
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        public string? Designation { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
    public class EmployeePageViewModel
    {
        public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();
        public IEnumerable<EmployeeViewModel> EmployeeList { get; set; } = new List<EmployeeViewModel>();
    }

}
