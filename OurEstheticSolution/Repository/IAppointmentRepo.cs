using OurEstheticSolution.Data;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Repository
{
    public class IAppointmentRepo: IAppointment
    {
        private readonly AppDBContext _context;

        public IAppointmentRepo(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<AppointmentViewModel> GetAllAppointments()
        {
            return _context.Appointments
                .Select(appt => new AppointmentViewModel
                {
                    Id = appt.Id,
                    Title = appt.Title,
                    CustomerId = appt.CustomerId,
                    EmployeeId = appt.EmployeeId,
                    ServiceId = appt.ServiceId,
                    AppointmentTime = appt.AppointmentTime,
                    AppointmentDate = appt.AppointmentDate,
                    CreatedDate = appt.CreatedDate,
                    CreatedBy = appt.Createdby
               

                })
                .ToList();
        }

        public void InsertAppointment(AppointmentViewModel model)
        {
            // Map AppointmentViewModel to Appointment entity
            var appointmentEntity = new Appointment
            {
                Id = model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
                Title = model.Title,
                CustomerId = model.CustomerId,
                EmployeeId = model.EmployeeId,
                ServiceId = model.ServiceId,
                AppointmentDate = model.AppointmentDate,
                AppointmentTime = model.AppointmentTime,
                CreatedDate = model.CreatedDate == default ? DateTime.UtcNow : model.CreatedDate,
                Createdby = model.CreatedBy,
               
            };

            _context.Appointments.Add(appointmentEntity); // Add to DbSet
            _context.SaveChanges(); // Persist to DB
        }

    }
}
