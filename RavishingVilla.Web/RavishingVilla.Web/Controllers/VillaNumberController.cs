using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RavishingVilla.Domain.Entities;
using RavishingVilla.Infrastructure.Data;
using RavishingVilla.Web.ViewModels;

namespace RavishingVilla.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaNumberController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var villaNumbers = _context.VillaNumbers.Include(u => u.Villa).ToList();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {

            
            //list of villas
            //IEnumerable<SelectListItem> list = _context.Villas.ToList().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString(),

            //});

            VillaNumberVM villaNumber = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                })
            };

            ////view data code
            ////ViewData["VillaList"] = list;

            ////view bag code 
            //ViewBag.VillaList = list;
            return View(villaNumber);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM villa)
        {

            bool existingVillaNumber = _context.VillaNumbers.Any(u => u.Villa_Number == villa.VillaNumber.Villa_Number);


            //ModelState.Remove("Villa");
            if (ModelState.IsValid && !existingVillaNumber)
            {
                _context.VillaNumbers.Add(villa.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "The villa number has been created successfully";
                return RedirectToAction("Index", "Villa");
            }

            if(existingVillaNumber)
            {
                TempData["error"] = "The villa number already exists";
            }

            villa.VillaList = _context.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });

            return View(villa);


        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
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
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            bool existingVillaNumber = _context.VillaNumbers.Any(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);

            if (ModelState.IsValid)
            {
                _context.VillaNumbers.Update(villaNumberVM.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "The villa number has been updated successfully";
                return RedirectToAction("Index", "Villa");
            }

            villaNumberVM.VillaList = _context.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });

            return View(villaNumberVM);
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _context.Villas.FirstOrDefault(x => x.Id == villaId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _context.Villas.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb is not null)
            {
                _context.Villas.Remove(objFromDb);
                _context.SaveChanges();
                TempData["success"] = "The villa has been deleted successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be deleted";
            return View();
        }
    }
}
