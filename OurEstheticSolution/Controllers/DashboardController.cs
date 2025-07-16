using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OurEstheticSolution.Models;
using System.Security.Claims;

namespace OurEstheticSolution.Controllers
{

    public class DashboardController : Controller
    {
        private readonly UserManager<User> _userManager;

        public DashboardController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            string ? userName = HttpContext?.Session.GetString("UserName");
            string? userId = HttpContext?.Session.GetString("UserId");
            ViewBag.Username = userName;
            return View();
        }
    }
}
