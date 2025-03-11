using Microsoft.AspNetCore.Mvc;
using PrimeVillas.Application.Common.Interfaces;
using PrimeVillas.Domain.Entities;
using PrimeVillas.Infrastructure.DATA;

namespace PrimeVillasWeb.Controllers
{
    
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            if (villaObj == null)
            {
                return RedirectToAction("Error", "Home");
            }

            _unitOfWork.Villa.Update(villaObj);
            _unitOfWork.Save();
            TempData["success"] = "Villa updated successfully";
            return RedirectToAction("Index");
            
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

            if (obj == null)
            {
                TempData["error"] = "Villa not found";
                return RedirectToAction("Error", "Home");

            }

            _unitOfWork.Villa.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Villa deleted successfully";
            return RedirectToAction("Index");

        }

    }
}
