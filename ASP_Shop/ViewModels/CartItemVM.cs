using ASP_Shop.Models;

namespace ASP_Shop.ViewModels
{
    public class CartItemVM
    {
        public ProductModel Product { get; set; } = null!;
        public int Count { get; set; }
        public decimal TotalPrice => Product.Price * Count;
    }
}
