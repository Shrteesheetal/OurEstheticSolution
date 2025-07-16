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
                    Title = appt.Remarks,
                  
                    EmployeeId = appt.EmployeeId,
                    ServiceId = appt.ServiceId,
                    AppointmentTime = appt.AppointmentTime,
                    AppointmentDate = appt.AppointmentDate,
                    CreatedDate = appt.CreatedDate,
                    CreatedBy = appt.CreatedBy
               

                })
                .ToList();
        }
        public AppointmentViewModel GetById(Guid id)
        {
            var appointment = _context.Appointments
                .Where(appt => appt.Id == id)
                .Select(appt => new AppointmentViewModel
                {
                    Id = appt.Id,
                    Title = appt.Remarks,
                    EmployeeId = appt.EmployeeId,
                    ServiceId = appt.ServiceId,
                    AppointmentTime = appt.AppointmentTime,
                    AppointmentDate = appt.AppointmentDate,
                    CreatedDate = appt.CreatedDate,
                    CreatedBy = appt.CreatedBy

                })
                .SingleOrDefault(); // SingleOrDefault since ID is unique

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID '{id}' was not found.");
            }

            return appointment;
        }


        public void InsertAppointment(AppointmentViewModel model)
        {
            // Map AppointmentViewModel to Appointment entity
            var appointmentEntity = new Appointment
            {
                Id = model.Id,
                Remarks = model.Title,
              
                EmployeeId = model.EmployeeId,
                ServiceId = model.ServiceId,
                AppointmentDate = model.AppointmentDate,
                AppointmentTime = model.AppointmentTime,
                CreatedDate = model.CreatedDate == default ? DateTime.UtcNow : model.CreatedDate,
                CreatedBy = model.CreatedBy ?? string.Empty, // Fix for CS8601: Provide a default value if null
            };

            _context.Appointments.Add(appointmentEntity); // Add to DbSet
            _context.SaveChanges(); // Persist to DB
        }
        public void UpdateAppointment(AppointmentViewModel model)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == model.Id);

            if (appointment != null)
            {
                // Map fields from ViewModel to Entity
                appointment.Remarks = model.Title;
                appointment.EmployeeId = model.EmployeeId;
                appointment.ServiceId = model.ServiceId;
                appointment.AppointmentDate = model.AppointmentDate;
                appointment.AppointmentTime = model.AppointmentTime;
                appointment.CreatedDate = model.CreatedDate;
                appointment.CreatedBy = model.CreatedBy ?? string.Empty; // Fix for CS8601: Provide a default value if null

                _context.SaveChanges(); // Commit changes
            }
            else
            {
                throw new Exception("Appointment not found.");
            }
        }

        public void DeleteAppointment(Guid id)
        {
            var appointment = _context.Appointments.Find(id); // 'id' is assumed to be the Appointment ID

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment); // Remove the Appointment entity
                _context.SaveChanges();                    // Commit the deletion to the database
            }
            else
            {
                throw new Exception("Appointment not found.");
            }
        }


    }
}
