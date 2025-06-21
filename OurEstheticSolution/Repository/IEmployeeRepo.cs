using Dapper;
using OurEstheticSolution.Data;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.ViewModel;
using System.Data;

namespace OurEstheticSolution.Repository
{
    public class IEmployeeRepo(AppDBContext _context) : IEmployee
    {


    

        public IEnumerable<EmployeeViewModel> GetAllEmployee()
        {
            return _context.Employees
                .Select(emp => new EmployeeViewModel
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Address = emp.Address,
                    Email = emp.Email,
                    Designation = emp.Designation,
                    //DateOfStart = emp.DateOfStart,
                    CreatedAt = emp.CreatedAt,
                    UpdatedAt = emp.UpdatedAt
                })
                .ToList();
        }

        public EmployeeViewModel GetById(Guid id)
        {
            var employee = _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Address = e.Address,
                    Designation = e.Designation
                })
                .FirstOrDefault();

            if (employee == null)
            {
                throw new Exception($"Employee with ID {id} not found.");
            }

            return employee;
        }

        public void InsertEmployee(EmployeeViewModel model)
        {

            // Map EmployeeViewModel to Employee entity  
            var employeeEntity = new Employee
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                Designation = model.Designation,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };

            _context.Employees.Add(employeeEntity); // Add the mapped entity to the DbSet  
            _context.SaveChanges(); // Save changes to the database  
        }
        public void DeleteEmployee(Guid Emp)
        {
            var employee = _context.Employees.Find(Emp); // Emp is assumed to be the ID

            if (employee != null)
            {
                _context.Employees.Remove(employee);   // Remove the entity
                _context.SaveChanges();                // Commit changes
            }
            else
            {
                throw new Exception("Employee not found.");
            }

        }
        public void UpdateEmployee(EmployeeViewModel Emp)
        {

            var employee = _context.Employees.FirstOrDefault(e => e.Id == Emp.Id);

            if (employee != null)
            {
                // Map fields from ViewModel to Entity
                employee.Name = Emp.Name;
                employee.Address = Emp.Address;
                employee.Email = Emp.Email;
                employee.Designation = Emp.Designation;
                _context.SaveChanges(); // Commit changes
            }
            else
            {
                throw new Exception("Employee not found.");
            }
        }


    }
}

       
        

        

    
