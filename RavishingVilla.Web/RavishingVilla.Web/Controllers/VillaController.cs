using Microsoft.AspNetCore.Mvc;
using RavishingVilla.Application.Common.Interfaces;
using RavishingVilla.Domain.Entities;
using RavishingVilla.Infrastructure.Data;

namespace RavishingVilla.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaRepository _villaRepository;

        public VillaController(IVillaRepository villaRepository)
        {
            _villaRepository = villaRepository;
        }

        public IActionResult Index()
        {
            var villas = _villaRepository.GetAllVillas();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if (villa.Name == villa.Description)
            {
                //name is teh field and it is case insensitive 
                ModelState.AddModelError("name", "The description cannot exactly match the Name field");
            }
            if (ModelState.IsValid)
            {
                _villaRepository.Add(villa);
                _villaRepository.Save();
                TempData["success"] = "The villa has been created successfully";
                return RedirectToAction("Index", "Villa");
            }
            return View(villa);
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _villaRepository.GetVilla(x => x.Id == villaId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _villaRepository.Update(obj);
                _villaRepository.Save();
                TempData["success"] = "The villa has been updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _villaRepository.GetVilla(x => x.Id == villaId);
            if (obj is null)
            { 
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _villaRepository.GetVilla(u => u.Id == obj.Id);
            if (objFromDb is not null)
            {
                _villaRepository.Delete(objFromDb);
                _villaRepository.Save();
                TempData["success"] = "The villa has been deleted successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be deleted";
            return View();
        }
    }
}
