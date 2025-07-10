using Microsoft.AspNetCore.Mvc;

namespace OurEstheticSolution.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
