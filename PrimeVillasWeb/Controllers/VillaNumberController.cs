using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrimeVillas.Domain.Entities;
using PrimeVillas.Infrastructure.DATA;
using PrimeVillasWeb.ViewModels;

namespace PrimeVillasWeb.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly VillaContext _context;
        public VillaNumberController(VillaContext villaContext)
        {
                _context=villaContext;
        }


        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<VillaNumber> villaNumbers = _context.VillaNumbers.Include(u =>u.Villa).ToList();
            return View(villaNumbers);
        }

       
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {

                VillaList = _context.Villas.Select(u => new SelectListItem
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
            Boolean roomNumberExists = _context.VillaNumbers.Any(u => u.Villa_Number == villaNumberObj.VillaNumber.Villa_Number);           

            if (ModelState.IsValid && !roomNumberExists)
            {

                _context.VillaNumbers.Add(villaNumberObj.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "Villa Number added successfully";

                return RedirectToAction("index");
            }

            if (roomNumberExists)
            {
              TempData["error"] = "Villa Number already exists";
            }

            villaNumberObj.VillaList = _context.Villas.Select(u => new SelectListItem
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

                VillaList = _context.Villas.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)

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

                _context.VillaNumbers.Update(villaNumberVM.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "Villa Number Updated successfully";

                return RedirectToAction("index");
            }

            villaNumberVM.VillaList = _context.Villas.Select(u => new SelectListItem
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

                VillaList = _context.Villas.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)

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
            VillaNumber? obj = _context.VillaNumbers
                .FirstOrDefault(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");

            }
            _context.VillaNumbers.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Villa Number removed successfully.";
            return RedirectToAction("index");
        }
    }
}
