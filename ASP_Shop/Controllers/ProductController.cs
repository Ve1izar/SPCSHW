using ASP_Shop.Data;
using ASP_Shop.Models;
using ASP_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ASP_Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> products = _context.Products.Include(p => p.Category).AsEnumerable();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();
            var viewModel = new CreateProductVM
            {
                SelectItems = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateProductVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories.ToListAsync();
                viewModel.SelectItems = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                return View(viewModel);
            }

            var model = new ProductModel
            {
                CategoryId = viewModel.CategoryId <= 0 ? null : viewModel.CategoryId,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Amount = viewModel.Amount,
                CreateDate = viewModel.CreateDate
            };

            if (viewModel.Image != null)
            {
                model.Image = await SaveImageAsync(viewModel.Image);
            }
            else if (!string.IsNullOrWhiteSpace(viewModel.ImageUrl))
            {
                model.Image = viewModel.ImageUrl;
            }

            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var categories = await _context.Categories.ToListAsync();

            var viewModel = new EditProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Amount = product.Amount,
                CategoryId = product.CategoryId,
                ExistingImage = product.Image,
                SelectItems = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories.ToListAsync();
                viewModel.SelectItems = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                return View(viewModel);
            }

            var product = await _context.Products.FindAsync(viewModel.Id);
            if (product == null) return NotFound();

            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.Amount = viewModel.Amount;
            product.CategoryId = viewModel.CategoryId <= 0 ? null : viewModel.CategoryId;

            if (viewModel.NewImage != null)
            {
                if (!string.IsNullOrWhiteSpace(product.Image) && !product.Image.StartsWith("http"))
                {
                    string oldImagePath = Path.Combine(_environment.WebRootPath, "images", "products", product.Image);
                    if (System.IO.File.Exists(oldImagePath)) System.IO.File.Delete(oldImagePath);
                }
                product.Image = await SaveImageAsync(viewModel.NewImage);
            }
            else if (!string.IsNullOrWhiteSpace(viewModel.ImageUrl))
            {
                if (!string.IsNullOrWhiteSpace(product.Image) && !product.Image.StartsWith("http"))
                {
                    string oldImagePath = Path.Combine(_environment.WebRootPath, "images", "products", product.Image);
                    if (System.IO.File.Exists(oldImagePath)) System.IO.File.Delete(oldImagePath);
                }
                product.Image = viewModel.ImageUrl;
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                if (!string.IsNullOrWhiteSpace(product.Image))
                {
                    string filePath = Path.Combine(_environment.WebRootPath, "images", "products", product.Image);
                    if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private async Task<string?> SaveImageAsync(IFormFile file)
        {
            try
            {
                var types = file.ContentType.Split("/");
                if (types.Length != 2 || types[0] != "image") return null;

                string imagesPath = Path.Combine(_environment.WebRootPath, "images", "products");
                Directory.CreateDirectory(imagesPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(imagesPath, fileName);

                using var fileStream = System.IO.File.Create(filePath);
                using var imageStream = file.OpenReadStream();
                await imageStream.CopyToAsync(fileStream);

                return fileName;
            }
            catch { return null; }
        }
    }
}