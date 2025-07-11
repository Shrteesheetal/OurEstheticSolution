using Microsoft.AspNetCore.Mvc;

namespace OurEstheticSolution.Controllers
{
    public class AppointmentDashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
