using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Interface
{
    public interface IAppointment
    {
       

        IEnumerable<AppointmentViewModel> GetAllAppointments();

        void InsertAppointment(AppointmentViewModel model);

        AppointmentViewModel GetById(Guid id);

        void DeleteAppointment(Guid id);
        void UpdateAppointment(AppointmentViewModel model);
    }
}
