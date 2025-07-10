using Microsoft.AspNetCore.Mvc;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Repository;
using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Controllers
{
    public class AppointmentController : Controller

    {
        private readonly IAppointment _appoRepo;
        // Constructor to inject any required services
        public AppointmentController(IAppointment appoRepo)
        {
            _appoRepo = appoRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AppointmentDetail(Guid id)
        {

            var appointment= _appoRepo.GetById(id);
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
