using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Interface
{
    public interface IService
    {
        IEnumerable<ServiceViewModel> GetAllServices();
        ServiceViewModel GetById(Guid id);
        void InsertService(ServiceViewModel model);

        void UpdateService(ServiceViewModel model);

        void DeleteService(Guid id);
     


    }
}
