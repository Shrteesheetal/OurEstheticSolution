using Microsoft.AspNetCore.Mvc;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Controllers
{
    public class EmployeeController : Controller

    {
        private readonly IEmployee _employeeRepo;
        public EmployeeController(IEmployee empRepo)
        {
            _employeeRepo = empRepo;
        }

        public virtual IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(EmployeeViewModel model)
        {


            try
            {
                    _employeeRepo.InsertEmployee(model);
                    return Json(new { success = true, message = "Employee inserted successfully!" });
             
            }
             
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


        [HttpPost]
        public JsonResult UpdateEmployee(EmployeeViewModel model)
        {
            try
            {
                
                    _employeeRepo.UpdateEmployee(model);
                    return Json(new { success = true, message = "Employee Updatedded successfully!" });
               

               
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        public JsonResult AllEmployee()
        {
            var data = _employeeRepo.GetAllEmployee();
            return Json(data);

        }
        [HttpPost]
        public JsonResult DeleteEmployees(Guid id)
        {
            _employeeRepo.DeleteEmployee(id);
            return Json(new { success = true, message = "Employee Deleted successfully!" });
        }
        public IActionResult DetailEmployee(Guid id)
        {
            var employee = _employeeRepo.GetById(id);
            return Json(employee);
        }

      



    }


}
