using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RavishingVilla.Application.Common.Interfaces;
using RavishingVilla.Domain.Entities;
using RavishingVilla.Web.ViewModels;

namespace RavishingVilla.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var villaNumbers = _unitOfWork.Amenity.GetAllVillas(includeProperties: "Villa");
            //var villaNumbers = _context.VillaNumbers.Include(u => u.Villa).ToList();
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

            AmenityVM villaNumber = new()
            {
                VillaList = _unitOfWork.Villa.GetAllVillas().Select(u => new SelectListItem
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
        public IActionResult Create(AmenityVM amenity)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Add(amenity.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenity has been created successfully.";
                return RedirectToAction(nameof(Index));
            }

            amenity.VillaList = _unitOfWork.Amenity.GetAllVillas().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(amenity);
        }

        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAllVillas().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),

                Amenity = _unitOfWork.Amenity.GetVilla(u => u.Id == amenityId)
            };

            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenity has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            amenityVM.VillaList = _unitOfWork.Villa.GetAllVillas().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(amenityVM);
        }

        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAllVillas().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.GetVilla(u => u.Id == amenityId)
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
            Amenity? objFromDb = _unitOfWork.Amenity
                .GetVilla(u => u.Id == amenityVM.Amenity.Id);
            if (objFromDb is not null)
            {
                _unitOfWork.Amenity.Delete(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The amenity has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The amenity could not be deleted.";
            return View();
        }
    }
}
