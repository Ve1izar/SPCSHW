using ASP_Shop.Data;
using ASP_Shop.Models;
using ASP_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace ASP_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? category, string? sortOrder)
        {
            var categories = await _context.Categories.ToListAsync();
            IQueryable<ProductModel> products = _context.Products.Include(p => p.Category);

            if (!string.IsNullOrWhiteSpace(category))
            {
                var queryCategory = categories.FirstOrDefault(c => c.Name.ToLower() == category.ToLower());
                if (queryCategory != null) products = products.Where(p => p.CategoryId == queryCategory.Id);
            }

            products = sortOrder switch
            {
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "name" => products.OrderBy(p => p.Name),
                "rating_desc" => products.OrderByDescending(p => p.Rating),
                _ => products.OrderByDescending(p => p.CreateDate) // За замовчуванням
            };

            var viewModel = new HomeVM
            {
                Products = await products.ToListAsync(),
                Categories = categories
            };

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentCategory"] = category;

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult Privacy() => View();
        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}