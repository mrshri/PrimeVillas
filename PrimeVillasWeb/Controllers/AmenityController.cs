using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrimeVillas.Application.Common.Interfaces;
using PrimeVillas.Application.Utility;
using PrimeVillas.Domain.Entities;
using PrimeVillasWeb.ViewModels;

namespace PrimeVillasWeb.Controllers
{
    
    [Authorize(Roles =StaticDetails.AdminEndUser)]
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Amenity> amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM amenityVMObj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Add(amenityVMObj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Amenity  added successfully";

                return RedirectToAction(nameof(Index));
            }

            amenityVMObj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(amenityVMObj);
        }

        public IActionResult Edit(int AmenityId)
        {
            AmenityVM  amenityVm = new()
            {

                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == AmenityId)

            };

            if (amenityVm.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(amenityVm);
        }

        [HttpPost]
        public IActionResult Edit(AmenityVM amenityVM)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.Amenity.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Amenity Updated successfully";

                return RedirectToAction(nameof(Index));
            }

            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(amenityVM);
        }


        public IActionResult Delete(int AmenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == AmenityId)
            };

            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            Amenity? obj = _unitOfWork.Amenity
                .Get(u => u.Id == amenityVM.Amenity.Id);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            _unitOfWork.Amenity.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Amenity removed successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
