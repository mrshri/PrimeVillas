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
    }
}
