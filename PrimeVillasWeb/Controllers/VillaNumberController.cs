using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrimeVillas.Application.Common.Interfaces;
using PrimeVillas.Application.Utility;
using PrimeVillas.Domain.Entities;
using PrimeVillas.Infrastructure.DATA;
using PrimeVillasWeb.ViewModels;

namespace PrimeVillasWeb.Controllers
{
    [Authorize(Roles =StaticDetails.AdminEndUser)]
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaNumberController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<VillaNumber> villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties:"Villa");
            return View(villaNumbers);
        }

       
        public IActionResult Create()
        {  
            VillaNumberVM villaNumberVM = new()
            {
              VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
              {
                Text = u.Name,
                Value = u.Id.ToString()
              })
            };
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM villaNumberObj)
        {
            Boolean roomNumberExists = _unitOfWork.VillaNumber.Any(u => u.Villa_Number == villaNumberObj.VillaNumber.Villa_Number);           

            if (ModelState.IsValid && !roomNumberExists)
            {

                _unitOfWork.VillaNumber.Add(villaNumberObj.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "Villa Number added successfully";

                return RedirectToAction("index");
            }

            if (roomNumberExists)
            {
              TempData["error"] = "Villa Number already exists";
            }

            villaNumberObj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(villaNumberObj);
        }

        public IActionResult Edit(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {

                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)

            };

            if(villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Edit(VillaNumberVM villaNumberVM)
        {

            if (ModelState.IsValid )
            {

                _unitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "Villa Number Updated successfully";

                return RedirectToAction("index");
            }

            villaNumberVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(villaNumberVM);
        }


        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
            };

            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? obj = _unitOfWork.VillaNumber
                .Get(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");

            }
            _unitOfWork.VillaNumber.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Villa Number removed successfully.";
            return RedirectToAction("index");
        }
    }
}
