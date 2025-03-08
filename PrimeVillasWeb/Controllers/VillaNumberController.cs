using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrimeVillas.Domain.Entities;
using PrimeVillas.Infrastructure.DATA;

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
            IEnumerable<VillaNumber> villaNumbers = _context.VillaNumbers.ToList();
            return View(villaNumbers);
        }

       
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> list = _context.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            ViewData["villaList"] = list;
            return View();
        }

        [HttpPost]
        public IActionResult Create(VillaNumber villaNumberObj)
        {
            if (ModelState.IsValid)
            {
                _context.VillaNumbers.Add(villaNumberObj);
                _context.SaveChanges();
                TempData["success"] = "Villa Number added successfully";

                return RedirectToAction("index");
            }
            return View(villaNumberObj);
        }


    }
}
