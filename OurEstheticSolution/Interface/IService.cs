using OurEstheticSolution.Models.ViewModels;
using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Interface
{
    public interface IService
    {
        IEnumerable<ServiceViewModel> GetAllService();
        ServiceViewModel GetById(Guid id);
        void InsertService(ServiceViewModel model);

        void UpdateService(ServiceViewModel model);

        void DeleteService(Guid id);


    }
}
