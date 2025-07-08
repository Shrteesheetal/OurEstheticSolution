using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Models;
using OurEstheticSolution.Repository;

namespace OurEstheticSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService _serviceRepo;

        public HomeController(ILogger<HomeController> logger, IService serviceRepo  )
        {
            _logger = logger;
            _serviceRepo = serviceRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]

        public IActionResult Admin()
        {
            return View();
        }


        [Authorize(Roles = "User")]

        public IActionResult User()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
