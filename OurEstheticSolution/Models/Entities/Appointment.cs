namespace OurEstheticSolution.Models.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? CustomerId { get; set; }

        public string? EmployeeId { get; set; }
        public string? ServiceId { get; set; }

        public DateTime AppointmwntTime { get; set; }
        public DateTime AppointmwntDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;

        public DateTime LastUpdatedBy { get; set; } = DateTime.Now;
    }
   


    
}
