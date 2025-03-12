using Microsoft.AspNetCore.Mvc;
using PrimeVillas.Application.Common.Interfaces;
using PrimeVillas.Domain.Entities;
using PrimeVillas.Infrastructure.DATA;

namespace PrimeVillasWeb.Controllers
{
    
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public VillaController(IUnitOfWork unitOfWork , IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {

            IEnumerable<Villa> villas = _unitOfWork.Villa.GetAll();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villaObj)
        {
            if(ModelState.IsValid)
            {
                if (villaObj.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString()+Path.GetExtension(villaObj.Image.FileName);
                    string imagePath = Path.Combine(_hostingEnvironment.WebRootPath, @"images\VillaImage");

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    villaObj.Image.CopyTo(fileStream);
                    
                    villaObj.ImageUrl = @"\images\VillaImage\" + fileName;
                }
                else
                {
                    villaObj.ImageUrl = "https://placehold.co/600x400";
                }

                _unitOfWork.Villa.Add(villaObj);
                _unitOfWork.Save();
                TempData["success"] = "Villa added successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa will  not be added";
            return View(villaObj);
        }

        public IActionResult Edit(int villaId)
        {
            var obj = _unitOfWork.Villa.Get(u => u.Id == villaId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Villa villaObj)
        {

            if (ModelState.IsValid && villaObj.Id> 0)
            {
                if (villaObj.Image  != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villaObj.Image.FileName);
                    string imagePath = Path.Combine(_hostingEnvironment.WebRootPath, @"images\VillaImage");
                    
                    if(!string.IsNullOrEmpty(villaObj.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, villaObj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    villaObj.Image.CopyTo(fileStream);

                    villaObj.ImageUrl = @"\images\VillaImage\" + fileName;
                }
                _unitOfWork.Villa.Update(villaObj);
                _unitOfWork.Save();
                TempData["success"] = "Villa updated successfully";
                return RedirectToAction("Index");

            }
            return View();

        }

        public IActionResult Delete(int villaId)
        {
            var obj = _unitOfWork.Villa.Get(u => u.Id == villaId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa villaObj)
        {
            var obj = _unitOfWork.Villa.Get(u => u.Id == villaObj.Id);

            if (villaObj is not null)
            {
                if (!string.IsNullOrEmpty(obj.ImageUrl))
                {
                    string oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.Villa.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
