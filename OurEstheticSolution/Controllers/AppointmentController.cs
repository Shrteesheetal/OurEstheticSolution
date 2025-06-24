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

    }
}
