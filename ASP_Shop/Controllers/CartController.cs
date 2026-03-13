using ASP_Shop.Data;
using ASP_Shop.Services;
using ASP_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sessionItems = CartService.GetItems(HttpContext.Session);
            var cartVM = new List<CartItemVM>(); // Створимо цю ViewModel нижче

            foreach (var item in sessionItems)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product != null)
                {
                    cartVM.Add(new CartItemVM { Product = product, Count = item.Count });
                }
            }

            return View(cartVM);
        }

        public IActionResult Add(int id)
        {
            CartService.AddToCart(HttpContext.Session, id);
            return RedirectToAction("Index");
        }

        public IActionResult Decrease(int id)
        {
            CartService.Decrease(HttpContext.Session, id);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            CartService.RemoveFromCart(HttpContext.Session, id);
            return RedirectToAction("Index");
        }
    }
}
