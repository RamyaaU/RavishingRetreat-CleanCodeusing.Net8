using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RavishingVilla.Application.Common.Interfaces;
using RavishingVilla.Domain.Entities;
using RavishingVilla.Web.ViewModels;

namespace RavishingVilla.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var villaNumbers = _unitOfWork.VillaNumber.GetAllVillas(includeProperties: "Villa");
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

            VillaNumberVM villaNumber = new()
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
        public IActionResult Create(VillaNumberVM villa)
        {

            bool existingVillaNumber = _unitOfWork.VillaNumber.Any(u => u.Villa_Number == villa.VillaNumber.Villa_Number);

            //ModelState.Remove("Villa");
            if (ModelState.IsValid && !existingVillaNumber)
            {
                _unitOfWork.VillaNumber.Add(villa.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa number has been created successfully";
                return RedirectToAction("Index", "Villa");
            }

            if (existingVillaNumber)
            {
                TempData["error"] = "The villa number already exists";
            }

            villa.VillaList = _unitOfWork.Villa.GetAllVillas().Select(u => new SelectListItem
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
                VillaList = _unitOfWork.Villa.GetAllVillas().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),

                VillaNumber = _unitOfWork.VillaNumber.GetVilla(u => u.Villa_Number == villaNumberId)
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
            bool existingVillaNumber = _unitOfWork.VillaNumber.Any(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);

            if (ModelState.IsValid)
            {
                _unitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa number has been updated successfully";
                return RedirectToAction("Index", "Villa");
            }

            villaNumberVM.VillaList = _unitOfWork.Villa.GetAllVillas().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });

            return View(villaNumberVM);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAllVillas().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),

                VillaNumber = _unitOfWork.VillaNumber.GetVilla(u => u.Villa_Number == villaNumberId)
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
            VillaNumber? objFromDb = _unitOfWork.VillaNumber
                .GetVilla(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);

            if (objFromDb is not null)
            {
                _unitOfWork.VillaNumber.Delete(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been deleted successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be deleted";
            return View();
        }
    }
}
