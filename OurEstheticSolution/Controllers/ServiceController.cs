using Microsoft.AspNetCore.Mvc;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.Repository;
using OurEstheticSolution.ViewModel;


namespace OurEstheticSolution.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IService _serviceRepo;
        private readonly IProduct _prodRepo;
        public ServiceController(IService ServiceRepo, IProduct prodRepo )
        {
            _serviceRepo = ServiceRepo;
            _prodRepo = prodRepo;
        }
        public IActionResult Index()
        {
           
            IEnumerable<ProductViewModel> ServiceRepo= _prodRepo.GetAllProducts();
            ViewBag.ProductDropDown = ServiceRepo;
            return View();
        }


        [HttpPost]
        public IActionResult Create(ServiceViewModel model)
        {
            try
            {
                _serviceRepo.InsertService(model);
                return Json(new { success = true, message = "Service inserted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


        public JsonResult AllService()
        {
            var data = _serviceRepo.GetAllServices();
            return Json(data);
        }

        [HttpPost]
        public JsonResult UpdateService(ServiceViewModel model)
        {
            try
            {
                _serviceRepo.UpdateService(model);
                return Json(new { success = true, message = "Service updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        public IActionResult DetailService(Guid id)
        {
            var service = _serviceRepo.GetById(id);
            return Json(service);
        }

        public JsonResult DeleteServices(Guid id)
        {
            _serviceRepo.DeleteService(id);
            return Json(new { success = true, message = "Service Deleted successfully!" });
        }



    }
}
