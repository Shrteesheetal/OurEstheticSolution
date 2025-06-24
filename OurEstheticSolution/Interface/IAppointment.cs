using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Interface
{
    public interface IAppointment
    {
        IEnumerable<AppointmentViewModel> GetAllAppointments();

        void InsertAppointment(AppointmentViewModel model);
    }
}
