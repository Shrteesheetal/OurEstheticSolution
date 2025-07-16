using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Repository;
using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Controllers
{
    public class AppointmentDashBoardController : Controller

    {
        private readonly IAppointment _appoRepo;
        // Constructor to inject any required services
        private readonly IService _serviceRepo;
        private readonly IEmployee _employeeRepo;
        public AppointmentDashBoardController(IAppointment appoRepo, IService serviceRepo , IEmployee empRepo)
        {
            _appoRepo = appoRepo;
            _serviceRepo = serviceRepo;
            _employeeRepo = empRepo;
        }
      
        public IActionResult Index()
        {
            IEnumerable<ServiceViewModel> appoRepo = _serviceRepo.GetAllServices();
            ViewBag.ServiceDropDown = appoRepo;

             IEnumerable<EmployeeViewModel> empRepo = _employeeRepo.GetAllEmployee();
            ViewBag.EmployeeDropDown = empRepo;


            string? userName = HttpContext?.Session.GetString("UserName");
            string? userId = HttpContext?.Session.GetString("UserId");
            ViewBag.Username = userName;
            return View();
        }
        [HttpGet]
        public ActionResult AppointmentDetail(Guid id)
        {

            var appointment = _appoRepo.GetById(id);
            return View(appointment);



        }



        [HttpGet]
        public JsonResult AllAppointments()
        {
            var data = _appoRepo.GetAllAppointments();
            return Json(data);
        }


        [HttpPost]
        public IActionResult Create(AppointmentViewModel model)
        {
            try
            {
                model.CreatedBy = HttpContext?.Session.GetString("UserName");
                _appoRepo.InsertAppointment(model);
                return Json(new { success = true, message = "Appointment inserted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateAppointment(AppointmentViewModel model)
        {
            try
            {
                _appoRepo.UpdateAppointment(model);
                return Json(new { success = true, message = "Appointment updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        public IActionResult DetailAppointment(Guid id)
        {
            var appointment = _appoRepo.GetById(id);
            return Json(appointment);
        }

        public JsonResult DeleteAppointments(Guid id)
        {
            _appoRepo.DeleteAppointment(id);
            return Json(new { success = true, message = "Appointment deleted successfully!" });
        }


    }
}

    
