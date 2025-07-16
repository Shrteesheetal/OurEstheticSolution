using Microsoft.AspNetCore.Mvc;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.Repository;
using OurEstheticSolution.ViewModel;

using System.Web;


namespace OurEstheticSolution.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IService _serviceRepo;
    
        public ServiceController(IService ServiceRepo )
        {
            _serviceRepo = ServiceRepo;
          
        }
        public IActionResult Index()
        {
            string? userName = HttpContext?.Session.GetString("UserName");
            ViewBag.Username = userName;
            return View();

           
        }

        [HttpGet]
        public ActionResult ServiceDetail(Guid id)
        {

            var services = _serviceRepo.GetById(id);
            return View(services);

        }
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(IFormFile PostImage, [FromForm] ServiceViewModel tb)
        {
            tb.CreatedBy = HttpContext?.Session.GetString("UserName");

            if (PostImage == null || PostImage.Length == 0)
                return Json(new { success = false, message = "Image file is missing." });

            string fileName = Path.GetFileNameWithoutExtension(PostImage.FileName);
            string extension = Path.GetExtension(PostImage.FileName);
            fileName = $"{fileName}_{Guid.NewGuid()}{extension}"; // Avoid name collisions

            string saveDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "serviceimg");
            if (!Directory.Exists(saveDir))
                Directory.CreateDirectory(saveDir);

            string savePath = Path.Combine(saveDir, fileName);
            tb.Imagepath = "/serviceimg/" + fileName; // Save image path to match frontend folder

            try
            
            
            
            
            {
                _serviceRepo.InsertService(tb);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await PostImage.CopyToAsync(stream);
                }

                return Json(new { success = true, message = "Service created successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Server error: " + ex.Message });
            }
        }


        //[HttpPost, ValidateInput(false)]

        //public IActionResult Create(HttpPostedFileBase PostImage, ServiceViewModel model)
        //{
        //    try
        //    {
        //        _serviceRepo.InsertService(model);
        //        return Json(new { success = true, message = "Service inserted successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "Error: " + ex.Message });
        //    }
        //}


        public JsonResult AllService()
        
        
        
        {
            var data = _serviceRepo.GetAllServices();
            return Json(data);
        }

        //[HttpPost]
        //public JsonResult UpdateService(ServiceViewModel model)
        //{
        //    try
        //    {
        //        _serviceRepo.UpdateService(model);
        //        return Json(new { success = true, message = "Service updated successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "Error: " + ex.Message });
        //    }
        //}
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<JsonResult> UpdateService(IFormFile PostImage, [FromForm] ServiceViewModel model)
        {

            if (PostImage == null || PostImage.Length == 0)
                return Json(new { success = false, message = "Image file is missing." });

            string fileName = Path.GetFileNameWithoutExtension(PostImage.FileName);
            string extension = Path.GetExtension(PostImage.FileName);
            fileName = $"{fileName}_{Guid.NewGuid()}{extension}"; // Avoid name collisions

            string saveDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "serviceimg");
            if (!Directory.Exists(saveDir))
                Directory.CreateDirectory(saveDir);

            string savePath = Path.Combine(saveDir, fileName);
            model.Imagepath = "/serviceimg/" + fileName; // Save image path to match frontend folder

            try

            {
                _serviceRepo.UpdateService(model);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await PostImage.CopyToAsync(stream);
                }

                return Json(new { success = true, message = "Service Updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Server error: " + ex.Message });
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
