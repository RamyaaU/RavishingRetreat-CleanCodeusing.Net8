using Microsoft.AspNetCore.Mvc;
using RavishingVilla.Domain.Entities;
using RavishingVilla.Infrastructure.Data;

namespace RavishingVilla.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var villas = _context.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if(villa.Name == villa.Description)
            {
                //name is teh field and it is case insensitive 
                ModelState.AddModelError("name", "The description cannot exactly match the Name field");
            }
            if (ModelState.IsValid)
            {
                _context.Villas.Add(villa);
                _context.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            return View(villa);
        }
    }
}
