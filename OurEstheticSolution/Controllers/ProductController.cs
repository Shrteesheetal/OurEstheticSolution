﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.Repository;

using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _prodRepo;
        private readonly IService _serviceRepo;

        public  ProductController(IProduct prodRepo, IService serviceRepo)
        {
            _prodRepo = prodRepo;
            _serviceRepo = serviceRepo;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<ServiceViewModel> prodRepo = _serviceRepo.GetAllServices();
            ViewBag.ServiceDropDown = prodRepo;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            try
            {
                _prodRepo.InsertProduct(model);
                return Json(new { success = true, message = "Product inserted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


        public JsonResult AllProduct()
        {
            var data = _prodRepo.GetAllProducts();
            return Json(data);

        }

        [HttpPost]
        public JsonResult UpdateProduct(ProductViewModel model)
        {
            try
            {

                _prodRepo.UpdateProduct(model);
                return Json(new { success = true, message = "product Updatedded successfully!" });



            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteProducts(Guid id)
        {
            _prodRepo.DeleteProduct(id);
            return Json(new { success = true, message = "Product Deleted successfully!" });
        }
        public IActionResult DetailProduct(Guid id)
        {
            var product = _prodRepo.GetById(id);
            return Json(product);
        }



    }
}
