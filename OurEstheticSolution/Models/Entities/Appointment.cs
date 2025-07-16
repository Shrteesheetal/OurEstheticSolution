using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurEstheticSolution.Models.Entities
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }

        public Guid? ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service? Service { get; set; }


        [Required(ErrorMessage = "Appointment time is required")]
        [DataType(DataType.Time)]
        public DateTime AppointmentTime { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? CreatedBy { get; set; }  // from session
        public string? Remarks { get; set; }



    }
}
