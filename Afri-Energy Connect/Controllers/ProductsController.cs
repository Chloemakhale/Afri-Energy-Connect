using Afri_Energy_Connect.Models;
using Afri_Energy_Connect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Afri_Energy_Connect.Controllers
{
    [Authorize]

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment) 
        { 
        this._context = context;
        }
        public IActionResult Index(DateTime? filterDate, string filterCategory)
        {
            IQueryable<Product> productsQuery = _context.Products;

            if (filterDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate.Date == filterDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(filterCategory))
            {
                productsQuery = productsQuery.Where(p => p.ProductCategory == filterCategory);
            }

            var products = productsQuery.ToList();
            return View(products);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            Product product = new Product
            {
                ProductName = productDto.ProductName,
                ProductCategory = productDto.ProductCategory,
                ProductionDate = productDto.ProductionDate,
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}