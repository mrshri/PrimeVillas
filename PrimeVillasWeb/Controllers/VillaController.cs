using Microsoft.AspNetCore.Mvc;
using PrimeVillas.Domain.Entities;
using PrimeVillas.Infrastructure.DATA;

namespace PrimeVillasWeb.Controllers
{
    
    public class VillaController : Controller
    {
        private readonly VillaContext _context;
        public VillaController(VillaContext villaContext)
        {
            _context = villaContext;
        }
        public IActionResult Index()
        {

            IEnumerable<Villa> villas = _context.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villaObj)
        {
            if(!ModelState.IsValid)
            {
                _context.Villas.Add(villaObj);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(villaObj);
        }

        public IActionResult Edit(int villaId)
        {
            var obj = _context.Villas.FirstOrDefault(u => u.Id == villaId);

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

                _context.Villas.Update(villaObj);
                _context.SaveChanges();

                return RedirectToAction("Index");
            
        }

        public IActionResult Delete(int villaId)
        {
            var obj = _context.Villas.FirstOrDefault(u => u.Id == villaId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }


            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa villaObj)
        {
            var obj = _context.Villas.FirstOrDefault(u => u.Id == villaObj.Id);

            if (obj == null)
            {
                return NotFound();
            }

            _context.Villas.Remove(obj);
            _context.SaveChanges();

            return RedirectToAction("Error","Home");

        }

    }
}
