using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Interface
{
    public interface IEmployee
    {

        void DeleteEmployee(Guid Emp);
        IEnumerable<EmployeeViewModel> GetAllEmployee();
        EmployeeViewModel GetById(Guid id);
        void InsertEmployee(EmployeeViewModel model);
        void UpdateEmployee(EmployeeViewModel Emp);
  
    }
}
