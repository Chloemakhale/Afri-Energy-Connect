using Afri_Energy_Connect.Models;
using Afri_Energy_Connect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afri_Energy_Connect.Controllers
{
    [Authorize(Policy = "MustBeChloe")]
    public class FarmersController : Controller
    {
        private readonly FarmerDbContext _context;
        public FarmersController(FarmerDbContext context)
        {
            _context = context;
        }

        public IActionResult FarmerIndex()
        {
            var farmers = _context.Farmers.ToList();
            return View(farmers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FarmersDto farmersdto)
        {
            if (!ModelState.IsValid)
            {
                return View(farmersdto);
            }

            Farmer farmer = new Farmer
            {
                Name = farmersdto.Name,
                Email = farmersdto.Email,
                Password = farmersdto.Password,
                Occupation = farmersdto.Occupation
            };

            _context.Farmers.Add(farmer);
            _context.SaveChanges();

            if (farmer.ID > 0) // Check if the data is successfully saved
            {
                return RedirectToAction("FarmerIndex");
            }
            else
            {
                // Data was not saved, return to the create view
                ModelState.AddModelError(string.Empty, "Failed to create farmer. Please try again.");
                return View(farmersdto);
            }
        }
    }
}
